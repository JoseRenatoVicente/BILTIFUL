

create database BILTIFUL;
GO

USE BILTIFUL;

create table Produto
(
	CodigoBarras Numeric(12) IDENTITY(789661700001, 1) not null,
	Nome varchar(20) not null,
	ValorVenda NUMERIC(4,2) not null,
	UltimaVenda DATE DEFAULT '0001-01-01',
	DataCadastro DATE not null DEFAULT GETDATE(),
	Situacao char not null DEFAULT 'A',
	QuantidadeEstoque NUMERIC(3,2) DEFAULT 0,
	CONSTRAINT ProdutoCodigoBarras PRIMARY KEY (CodigoBarras),
	CONSTRAINT CodigoBarrasProduto UNIQUE (CodigoBarras)
);


create table MPrima
(
	Id varchar(6) not null,
	Nome varchar(20) not null,
	UltimaCompra DATE DEFAULT '0001-01-01',
	DataCadastro DATE not null DEFAULT GETDATE(),
	Situacao char not null DEFAULT 'A',
	CONSTRAINT MPrimaID PRIMARY KEY (Id)
);

create table Cliente
(
	CPF varchar(11) not null,
	Nome varchar(50) not null,
	DataNascimento DATE not null,
	Sexo char not null DEFAULT '0',
	UltimaCompra DATE DEFAULT '0001-01-01',
	DataCadastro DATE not null DEFAULT GETDATE(),
	Situacao char not null DEFAULT 'A',
	CONSTRAINT CPFCliente PRIMARY KEY (CPF)
);

create table Fornecedor
(
	CNPJ varchar(14) not null,
	RazaoSocial varchar(50) not null,
	DataAbertura DATE not null,
	UltimaCompra DATE DEFAULT '0001-01-01',
	DataCadastro DATE not null DEFAULT GETDATE(),
	Situacao char not null DEFAULT 'A',
	CONSTRAINT CNPJFornecedor PRIMARY KEY (CNPJ)
);

create table Risco
(
	CPF varchar(11) not null,
	CONSTRAINT CPF PRIMARY KEY (CPF),
	CONSTRAINT CPFRisco FOREIGN KEY (CPF) REFERENCES Cliente (CPF)
);

create table Bloqueado
(
	CNPJ varchar(14) not null,
	CONSTRAINT CNPJ PRIMARY KEY (CNPJ),
	CONSTRAINT CNPJBloqueado FOREIGN KEY (CNPJ) REFERENCES Fornecedor (CNPJ)
);

create table Venda
(
	Id Numeric(5) IDENTITY not null,
	DataVenda DATE not null DEFAULT GETDATE(),
	Cliente varchar(11) not null,
	ValorTotal NUMERIC(5,2) not null,
	CONSTRAINT VendaId PRIMARY KEY (Id),
	CONSTRAINT ClienteVenda FOREIGN KEY (Cliente) REFERENCES Cliente (CPF)
);

create table ItemVenda
(
	Id Numeric(5) not null,
	Produto Numeric(12) not null,
	Quantidade NUMERIC(3),
	ValorUnitario NUMERIC(4,2) not null,
	TotalItem NUMERIC(4,2),
	CONSTRAINT ItemVenda_VendaId FOREIGN KEY (Id) REFERENCES Venda (Id),
	CONSTRAINT ItemVenda_ProdutoCodigoBarras FOREIGN KEY (Produto) REFERENCES Produto (CodigoBarras)
);

create table Compra
(
	Id Numeric(5) IDENTITY not null,
	DataCompra DATE not null DEFAULT GETDATE(),
	Fornecedor varchar(14) not null,
	ValorTotal NUMERIC(5,2) not null,
	CONSTRAINT CompraId PRIMARY KEY (Id),
	CONSTRAINT Compra_FornecedorCNPJ FOREIGN KEY (Fornecedor) REFERENCES Fornecedor (CNPJ)
);

create table ItemCompra
(
	Id Numeric(5) not null,
	DataCompra DATE not null DEFAULT GETDATE(),
	MateriaPrima varchar(6) not null,
	Quantidade NUMERIC(4,2),
	ValorUnitario NUMERIC(4,2) not null,
	TotalItem NUMERIC(4,2),
	CONSTRAINT ItemCompra_CompraId FOREIGN KEY (Id) REFERENCES Compra (Id),
	CONSTRAINT ItemCompra_MPrimaId FOREIGN KEY (MateriaPrima) REFERENCES MPrima (Id)
);

create table Producao
(
	Id Numeric(5) IDENTITY not null,
	DataProducao DATE not null DEFAULT GETDATE(),
	Produto Numeric(12) not null,
	Quantidade NUMERIC(4,2) not null,
	CONSTRAINT ProducaoId PRIMARY KEY (Id),
	CONSTRAINT Producao_ProdutoId FOREIGN KEY (Produto) REFERENCES Produto (CodigoBarras)
);

create table ItemProducao
(
	Id Numeric(5) not null,
	DataProducao DATE not null DEFAULT GETDATE(),
	MateriaPrima varchar(6) not null,
	QuantidadeMateriaPrima NUMERIC(4,2),
	CONSTRAINT ItemProducao_ProducaoId FOREIGN KEY (Id) REFERENCES Producao (Id),
	CONSTRAINT ItemProducao_MPrimaId FOREIGN KEY (MateriaPrima) REFERENCES MPrima (Id)
);
