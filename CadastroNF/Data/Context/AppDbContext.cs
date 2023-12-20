using CadastroNF.Entities;
using Microsoft.EntityFrameworkCore;

namespace CadastroNF.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Fornecedor> Fornecedores { get; set; } = null!;
    public DbSet<Produto> Produtos { get; set; } = null!;
    public DbSet<NotaFiscal> NotasFiscais { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(cliente =>
        {
            cliente
                .HasKey(x => x.Id)
                .HasName("cliente_pkey");

            cliente.ToTable("cliente");

            cliente
                .Property(x => x.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");

            cliente.Property(x => x.Nome).HasColumnName("nome");
        });

        modelBuilder.Entity<Fornecedor>(fornecedor =>
        {
            fornecedor.HasKey(x => x.Id).HasName("fornecedor_pkey");

            fornecedor.ToTable("fornecedor");

            fornecedor
                .Property(x => x.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            fornecedor.Property(x => x.Nome).HasColumnName("nome");
        });

        modelBuilder.Entity<NotaFiscal>(notaFiscal =>
        {
            notaFiscal.HasKey(x => x.NumeroNota).HasName("nota_fiscal_pkey");

            notaFiscal.ToTable("nota_fiscal");

            notaFiscal.Ignore(x => x.ValorTotalEmCentavos);

            notaFiscal
                .Property(x => x.NumeroNota)
                .UseIdentityAlwaysColumn()
                .HasColumnName("numero_nota");
            notaFiscal.Property(x => x.ClienteId).HasColumnName("cliente_id");
            notaFiscal.Property(x => x.FornecedorId).HasColumnName("fornecedor_id");

            notaFiscal
                .HasOne(x => x.Cliente)
                .WithMany(cliente => cliente.NotasFiscais)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cliente");

            notaFiscal
                .HasOne(x => x.Fornecedor)
                .WithMany(fornecedor => fornecedor.NotasFiscais)
                .HasForeignKey(x => x.FornecedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_fornecedor");

            notaFiscal
                .HasMany(x => x.Produtos)
                .WithMany(produto => produto.NotasFiscais)
                .UsingEntity<Dictionary<string, object>>(
                    "NotaFiscalProduto",
                    r => r.HasOne<Produto>().WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_produto"),
                    l => l.HasOne<NotaFiscal>().WithMany()
                        .HasForeignKey("NumeroNotaFiscal")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_nota_fiscal"),
                    j =>
                    {
                        j
                            .HasKey("NumeroNotaFiscal", "ProdutoId")
                            .HasName("nota_fiscal_produto_pkey");
                        j.ToTable("nota_fiscal_produto");
                        j.IndexerProperty<int>("NumeroNotaFiscal").HasColumnName("numero_nota_fiscal");
                        j.IndexerProperty<int>("ProdutoId").HasColumnName("produto_id");
                    });
        });

        modelBuilder.Entity<Produto>(produto =>
        {
            produto.HasKey(x => x.Id).HasName("produto_pkey");

            produto.ToTable("produto");

            produto.Property(x => x.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            produto.Property(x => x.Nome).HasColumnName("nome");
            produto.Property(x => x.ValorEmCentavos).HasColumnName("valor_em_centavos");
        });

        base.OnModelCreating(modelBuilder);
    }
}