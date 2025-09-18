Rafael de Alcantara = 06010477
Vinicius Schonfelder = 06010595
Lucas Soares = 06011048
lohana: 06009900


Diagrama: https://drive.google.com/file/d/1MUU0Vlw6h7DLvbSXrZoR-zlAHMT5Icm9/view?usp=sharing

1. Jogo

Responsável por representar cada jogo da biblioteca.

Propriedades:

Id (int)

Nome (string, private set)

Genero (string, private set)

Ano (int, private set)

Estado (bool)

EmprestadoHa (string)

DataEmprestimo (DateTime?)

DataDevolucao (DateTime?)

FormaPagamento (string)

Validações: não permite nomes ou anos inválidos.

Construtor: encapsulamento aplicado.

2. Membros

Representa cada membro da ludoteca.

Propriedades:

IdMem (int)

NomeMem (string, private set)

emprestimo (bool)

jogoPego (string)

3. Emprestar

Controle de empréstimos e devoluções.

Métodos genéricos de persistência:

Carregar<T>(string caminho) // [AV1-3]

Salvar<T>(string caminho, List<T> lista) // [AV1-3]

Tratamento de exceções com comentários // [AV1-5] para try/catch.

Lógica de limite de 2 jogos por membro, cálculo de valor de devolução e forma de pagamento.

4. GerarRelatorio

Geração de relatórios de empréstimos em arquivo relatorio.txt. // [AV2-1]

Persistência com System.Text.Json e tratamento de exceções.

Debug log de erros com marcação // [AV2-5]

5. CadastroJogo / CadastroMembros

Permitem adicionar e remover jogos e membros.

Salvamento e carregamento via JSON com comentários // [AV1-3].

Validações de entrada e encapsulamento de propriedades com private set.

6. ListaJogos

Listagem completa de jogos cadastrados.

Indica estado (disponível / indisponível) e a quem está emprestado.

Persistência

Todos os dados são armazenados em JSON na pasta data do projeto:

biblioteca.json → jogos

Membros.json → membros

relatorio.txt → relatório de empréstimos

Métodos Salvar() e Carregar() usam System.Text.Json com serialização indentada.
Comentários // [AV1-3] indicam pontos de serialização.
Comentários // [AV1-5] indicam blocos try/catch.

Menu Console

O menu principal permite executar as funções mínimas:

=== LUDOTECA .NET ===
1 - Cadastrar jogo      // [AV1-4-Cadastrar]
2 - Excluir Jogo        // [AV1-4-Excluir]
3 - Cadastrar membro    // [AV1-4-Cadastrar]
4 - Excluir membro      // [AV1-4-Excluir]
5 - Listar jogos        // [AV1-4-Listar]
6 - Emprestar jogo      // [AV1-4-Emprestar]
7 - Devolver jogo       // [AV1-4-Devolver]
8 - Gerar relatório     // [AV1-4-Relatorio]
0 - Sair

Regras e Validações

Nome de jogo e membro não podem estar vazios.

Ano do jogo ≥ 1950.

Limite de 2 jogos por membro.

Data de devolução não pode ser anterior à data de empréstimo.

Debug log registra pelo menos duas exceções // [AV2-5].

