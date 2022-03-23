create proc AdicionarCliente 
	@cpf varchar(11),
	@nome varchar(50),
	@dnascimento date,
	@sexo char
as
begin
	insert into Cliente (cpf,nome,dnascimento,sexo) values 
	(@cpf,@nome,@dnascimento,@sexo);
end;
--------
create proc AdicionarFornecedor
	@cnpj varchar(14),
	@rsocial varchar(50),
	@dabertura date
as
begin
	insert into Fornecedor(cnpj,rsocial,dabertura) values 
	(@cnpj,@rsocial,@dabertura);
end;
--------
create proc AdicionarMPrima
	@id varchar(6),
	@nome varchar(50)
as
begin
	insert into MPrima(id,nome) values 
	(@id,@nome);
end;
---------
create proc AdicionarProduto
	@cbarras varchar(12),
	@nome varchar(50),
	@vvenda decimal(5,2)
as
begin
	insert into Produto(cbarras,nome,vvenda) values 
	(@cbarras,@nome,@vvenda);
end;
-------
create proc AdicionarCompra
	@id varchar(5),
	@cnpj_fornecedor varchar(14)
as
begin
	
	insert into Compra (id,cnpj_fornecedor) values
	(@id,@cnpj_fornecedor);
end;
------
create proc CompraVtotal
	@id varchar(5)
	as
	begin
	declare
	@vtotal decimal(7,2) = 0
	
	select @vtotal = @vtotal + titem from Item_Compra where id_compra = @id
	update Compra set vtotal = @vtotal where id = @id
end;
-------
create proc AdicionarItemCompra
	@qtd decimal(5,2),
	@vunitario decimal(5,2),
	@id_compra varchar(5),
	@id_mprima varchar(6)
as
begin
	declare
	@titem decimal(6,2)

	select @titem = @qtd * @vunitario

	insert into Item_Compra (qtd,vunitario,titem,id_compra,id_mprima) values
	(@qtd,@vunitario,@titem,@id_compra,@id_mprima)
end;
--------------------
create proc AdicionarVenda
	@id varchar(5),
	@cpf_cliente varchar(11)
as
begin
	
	
	insert into Venda (id,cpf_cliente) values
	(@id,@cpf_cliente);
end;
--------
create proc VendaVtotal
	@id varchar(5)
	as
	begin
	declare
	@vtotal decimal(7,2) = 0

	select @vtotal = @vtotal + titem from Item_Venda where @id = id_venda
	update Venda set vtotal = @vtotal where id = @id
end;

--------
create proc AdicionarItemVenda
	@qtd decimal(5,2),
	@id_venda varchar(5),
	@cbarras_produto varchar(12)
as
begin
	declare
	@titem decimal(6,2),
	@vunitario decimal(5,2)

	select @vunitario = vvenda from Produto where cbarras = @cbarras_produto
	select @titem = @qtd * @vunitario

	insert into Item_Venda (qtd,vunitario,titem,id_venda,cbarras_produto) values
	(@qtd,@vunitario,@titem,@id_venda,@cbarras_produto)
end;
-----
create proc AdicionarProducao
	@id varchar(5),
	@qtd int,
	@cbarras_produto varchar(12)
as
begin
	
	insert into Producao(id,qtd,cbarras_produto) values
	(@id,@qtd,@cbarras_produto);
end;
--------
create proc AdicionarItemProducao
	@qtdmp decimal(5,2),
	@id_producao varchar(6),
	@id_mprima varchar(6)
as
begin

	insert into Item_Producao(qtdmp,id_producao,id_mprima) values
	(@qtdmp,@id_producao,@id_mprima)
end;
---------
create proc AdicionarInadimplente
	@cpf_cliente varchar(11)
as
begin
	insert into Risco(cpf_cliente) values
	(@cpf_cliente)
end;
--------
create proc RemoverInadimplente
	@cpf_inadimplente varchar(11)
as
begin
	delete from Risco where cpf_cliente = @cpf_inadimplente
end;
--------
create proc AdicionarBloqueado
	@cnpj_fornecedor varchar(14)
as
begin
	insert into Bloqueado(cnpj_fornecedor) values
	(@cnpj_fornecedor)
end;
--------
create proc RemoverBloqueado
	@cnpj_fornecedor varchar(14)
as
begin
	delete from Bloqueado where cnpj_fornecedor = @cnpj_fornecedor
end;


