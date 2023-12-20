\connect localhost_db

CREATE TABLE cliente
(
    id   INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    nome TEXT NOT NULL
);

CREATE TABLE fornecedor
(
    id   INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    nome TEXT NOT NULL
);

CREATE TABLE produto
(
    id                INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    nome              TEXT NOT NULL,
    valor_em_centavos INT  NOT NULL
);

CREATE TABLE nota_fiscal
(
    numero_nota   INT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    cliente_id    INT NOT NULL,
    fornecedor_id INT NOT NULL,

    CONSTRAINT fk_cliente FOREIGN KEY (cliente_id) REFERENCES cliente (id),
    CONSTRAINT fk_fornecedor FOREIGN KEY (fornecedor_id) REFERENCES fornecedor (id)
);

CREATE TABLE nota_fiscal_produto
(
    numero_nota_fiscal INT NOT NULL,
    produto_id         INT NOT NULL,

    PRIMARY KEY (numero_nota_fiscal, produto_id),
    CONSTRAINT fk_produto FOREIGN KEY (produto_id) REFERENCES produto (id),
    CONSTRAINT fk_nota_fiscal FOREIGN KEY (numero_nota_fiscal) REFERENCES nota_fiscal (numero_nota)
);