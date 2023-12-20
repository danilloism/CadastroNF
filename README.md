# Cadastro NF
CRUD simples feito em .NET Core 6 tentando cumprir os seguintes requisitos:
1. deverá haver no mínimo 4 classes representando o domínio: Nota Fiscal, Cliente, Fornecedor, Produtos;
2. cada nota fiscal deve haver apenas um cliente, e vários produtos;
3. cada classe deverá possuir sua respectiva tabe no banco e o programador deve garantir a consistência da inserção de uma nota, ou seja utilizar transação para que os relacionamentos sejam tratados como unicidade, no caso, se ao inserir uma nota ocorra um erro ao inserir seus produtos ou cliente, a operação deve ser revertida.

## Desenvolvimento
### Banco de dados
Quanto ao banco de dados, utilizei um ORM no código, o EF Core 7, pois tenho afinidade com ele. Resolvi adotar uma abordagem "database first", ou seja, criei o banco e as tabelas através de um script DDL que é rodado ao subir os containers através do docker compose e então utilizei as ferramentas do EF Core para criar automaticamente o contexto no código.

### Docker
Utilizei o docker compose para subir o container do PostgreSQL e realizar a configuração inicial. Também é iniciado um container rodando o pgadmin, caso seja necessário verificar os dados diretamente no banco. Devido ao tempo curto, não conseguir fazer o container da aplicação funcionar no docker compose.

### Código
Utilizei camadas de Controller, Service e Repository para separar as responsabilidades e desacoplar o código. Apliquei princípios de inversão de controle através da injeção de dependência e interfaces (processo que o próprio .NET Core facilita e incentiva). Ao realizar a operação de criar uma nota fiscal, é iniciada uma transaction e se algum erro ocorrer todas as modificações feitas no banco de dados até então são revertidas. 

### Testes
Infelizmente não houve tempo para a criação dos testes.

## Como rodar
Para rodar, basta iniciar o docker compose do projeto para iniciar o banco de dados e o pgadmin e então rodar o projeto através da linha de comando ou de sua IDE de preferência. Ao iniciar o servidor local, automaticamente é aberta a página do Swagger com as rotas/endpoints disponíveis.

## Pontos de melhoria
Criar testes e mais rotas, pois por enquanto há apenas rotas GET e POST.
Foi utlizado valor em centavos como valor monetário (pois lida com dinheiro de forma granular), mas eu queria ter implementado uma formatação desse valor. 
