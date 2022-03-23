## Alterações
# 23/03/2022
- Implementação do banco de dados no projeto
- Cadastros e Exibição feitos usando banco de dados
- Correção da navegação entre submenus
- Correção de CPFs e CNPJs que começam com zero
- Exibir compras funciona 
- Controle dos cliente e fornecedores inadimplentes ou de risco

## Correções e melhorias a serem feitas
- Navegação entre os registros mostra opções incorretas
- Localizar produção não exibe corretamente os dados
- Edição dos campos de Cliente, Fornecedores, MateriaPrima e Produto

# Como executar o projeto?
1. Execute o script de criação das tabelas do banco de dados, encontra-se na raiz do projeto com o nome ScripDataBaseBiltiful.sql
2. Altere as credenbciais da string de conexão com o banco de dados, que fica em BILTIFUL.Application no arquivo Configuration.cs


## BILTIFUL
BILTIFUL é um projeto de controle de estoque, produção e vendas para uma empresa de cosméticos, o projeto foi desenvolvido utilizando ConsoleApp

# Como funciona?
O programa possui um Menu Principal dividido em 4 partes 


![C__Users_José_source_repos_BILTIFUL_BILTIFUL_bin_Debug_net5 0_BILTIFUL exe-2022-03-08-15-43-29_Trim](https://user-images.githubusercontent.com/61014145/157305221-1312f812-6c50-4f4e-abf6-b7e3feaec36a.gif)


# Como usar?
O número 0 é usado para sair ou voltar ao menu. Os números devem ser adicionados com às duas casas decimais Ex: 1,00 como é informado na mensagem para o usuário. 

<hr>

<h1>Vendas, como usar!</h1>

![image](https://user-images.githubusercontent.com/89309834/157436499-f100690f-f776-4c1c-a16c-5e7dd5ab7f38.png)

  1) Cadastro <br>
Verificamos se o CPF do cliente está cadastrado no sistema, e se o mesmo está ativo ou está inadimplente. Se o CPF não existir no sistema o usuário tem a proposta de cadastrar o cliente.<br>
Depois do CPF ser validado, o sistema pergunta se os dados do usuário estão corretos, se estiverem começa o processo de venda.<br>
O sistema pede o código de barra do produto e verifica se o mesmo existe, depois solicita a quantidade que cliente deseja comprar, se as verificações de quantidade (quantidade > 0 || quantidade < 999) forem corretas o item é cadastrado na venda.<br>
Após adicionar os itens o sistema pergunta se deseja mesmo realizar a venda, se a opção escolhida for SIM a venda é realizada se a opção for NÃO a venda é cancelada.<br>
  
  2) Localizar Venda <br>
  Localiza uma venda específica pela Data de realização da venda.
 
  2) Exibir vendas cadastradas <br>
  Exibir em forma de registro as vendas cadastradas com as opções de próximo registro, registro anterior, último registro e primeiro registro


