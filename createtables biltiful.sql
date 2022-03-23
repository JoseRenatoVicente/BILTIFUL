create database BILTIFUL;
use BILTIFUL;
drop database BILTIFUL;

create table Cliente(
cpf varchar(11) not null,
nome varchar(50) not null,
dnascimento date not null,
sexo char not null,
ucompra date not null default getdate(),
dcadastro date not null default getdate(),
situacao char not null default 'A'

constraint PK_Cliente primary key (cpf)
);

create table Fornecedor(
cnpj varchar(14) not null,
rsocial varchar(50) not null,
dabertura date not null,
ucompra date not null default getdate(),
dcadastro date not null default getdate(),
situacao char not null default 'A'

constraint PK_Fornecedor primary key (cnpj)
);

create table MPrima(
id varchar(6) not null,
nome varchar(50) not null,
ucompra date not null default getdate(),
dcadastro date not null default getdate(),
situacao char not null default 'A'

constraint PK_MPrima primary key (id)
);

create table Produto(
cbarras varchar(12) not null,
nome varchar(50) not null,
vvenda decimal(5,2) not null,
ucompra date not null default getdate(),
dcadastro date not null default getdate(),
situacao char not null default 'A'

constraint PK_Produto primary key (cbarras)
);

create table Venda(
id varchar(5) not null,
dvenda date not null default getdate(),
vtotal decimal(7,2) not null default 0,
cpf_cliente varchar(11) not null

constraint PK_Venda primary key (id),
constraint FK_Venda_Cliente foreign key (cpf_cliente) references Cliente(cpf)
);

create table Item_Venda(
qtd int not null,
vunitario decimal(5,2) not null,
titem decimal(6,2) not null,
id_venda varchar(5) not null,
cbarras_produto varchar(12) not null

constraint PK_Item_Venda primary key (id_venda,cbarras_produto),
constraint FK_Item_Venda_Venda foreign key (id_venda) references Venda(id),
constraint FK_Item_Venda_Produto foreign key (cbarras_produto) references Produto(cbarras)
);

create table Producao(
id varchar(5) not null,
dproducao date not null default getdate(),
qtd int not null,
cbarras_produto varchar(12) not null

constraint PK_Producao primary key (id),
constraint FK_Producao_Produto foreign key (cbarras_produto) references Produto(cbarras)
);

create table Item_Producao(
qtdmp decimal(5,2) not null,
dproducao date not null default getdate(),
id_producao varchar(5) not null,
id_mprima varchar(6) not null

constraint PK_Item_Producao primary key (id_producao,id_mprima),
constraint FK_Item_Producao_Producao foreign key (id_producao) references Producao(id),
constraint FK_Item_Producao_MPrima foreign key (id_mprima) references MPrima(id)
);

create table Compra(
id varchar(5) not null,
dcompra date not null default getdate(),
vtotal decimal(7,2) not null default 0,
cnpj_fornecedor varchar(14) not null

constraint PK_Compra primary key (id),
constraint FK_Compra_Fornecedor foreign key (cnpj_fornecedor) references Fornecedor(cnpj)
);

create table Item_Compra(
qtd decimal(5,2) not null,
vunitario decimal(5,2) not null,
titem decimal(6,2) not null,
id_compra varchar(5) not null,
id_mprima varchar(6) not null

constraint PK_Item_Compra primary key (id_compra,id_mprima),
constraint FK_Item_Compra_Compra foreign key (id_compra) references Compra(id),
constraint FK_Item_Compra_MPrima foreign key (id_mprima) references MPrima(id)
);

create table Risco(
	cpf_cliente varchar(11)

	constraint PK_Risco primary key (cpf_cliente)
	constraint FK_Risco_Cliente foreign key (cpf_cliente) references Cliente(cpf)
);

create table Bloqueado(
	cnpj_fornecedor varchar(14)

	constraint PK_Bloqueado primary key (cnpj_fornecedor)
	constraint FK_Risco_Fornecedor foreign key (cnpj_fornecedor) references Fornecedor(cnpj)
);