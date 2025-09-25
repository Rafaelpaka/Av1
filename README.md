# Sistema Ludoteca .NET

## Equipe de Desenvolvimento
- **Rafael de Alcântara Peçanha Fernandes**: 06010477
- **Vinicius Schonfelder**: 06010595
- **Lucas Soares Francisco**: 06011048
- **Lohana Delgado**: 06009900

## Diagrama do Sistema
[Link para o diagrama](https://drive.google.com/file/d/1MUU0Vlw6h7DLvbSXrZoR-zlAHMT5Icm9/view?usp=sharing)

##Link do Video no Youtube:

[Link para o video](https://youtu.be/CXJSIICWTqQ)

---

## 1. Classe Jogo
Responsável por representar cada jogo da biblioteca.

### Propriedades:
- `Id` (int)
- `Nome` (string, private set)
- `Genero` (string, private set)
- `Ano` (int, private set)
- `Estado` (bool)
- `EmprestadoHa` (string)
- `DataEmprestimo` (DateTime?)
- `DataDevolucao` (DateTime?)
- `FormaPagamento` (string)

### Características:
- **Validações**: não permite nomes ou anos inválidos
- **Construtor**: encapsulamento aplicado

---

## 2. Classe Membros
Representa cada membro da ludoteca.

### Propriedades:
- `IdMem` (int)
- `NomeMem` (string, private set)
- `emprestimo` (bool)
- `jogoPego` (string)

---

## 3. Classe Emprestar
Controle de empréstimos e devoluções.

### Métodos Genéricos de Persistência:
- `Carregar(string caminho)` // [AV1-3]
- `Salvar(string caminho, List lista)` // [AV1-3]

### Funcionalidades:
- Tratamento de exceções com comentários // [AV1-5] para try/catch
- Lógica de limite de 2 jogos por membro
- Cálculo de valor de devolução e forma de pagamento

---

## 4. Classe GerarRelatorio
Geração de relatórios de empréstimos em arquivo `relatorio.txt` // [AV2-1]

### Características:
- Persistência com `System.Text.Json`
- Tratamento de exceções
- Debug log de erros com marcação // [AV2-5]

---

## 5. Classes CadastroJogo / CadastroMembros
Permitem adicionar e remover jogos e membros.

### Funcionalidades:
- Salvamento e carregamento via JSON com comentários // [AV1-3]
- Validações de entrada
- Encapsulamento de propriedades com `private set`

---

## 6. Classe ListaJogos
Listagem completa de jogos cadastrados.

### Características:
- Indica estado (disponível/indisponível)
- Mostra a quem está emprestado

---

## Persistência de Dados
Todos os dados são armazenados em JSON na pasta `data` do projeto:

- **biblioteca.json** → jogos
- **Membros.json** → membros  
- **relatorio.txt** → relatório de empréstimos

### Implementação:
- Métodos `Salvar()` e `Carregar()` usam `System.Text.Json` com serialização indentada
- Comentários `// [AV1-3]` indicam pontos de serialização
- Comentários `// [AV1-5]` indicam blocos try/catch

[AV1-3] linhas = 
emprestar.cs - linha: 35
CadastroMembros.cs - linha: 29, 46
CadastroJogo.cs - linha: 160

[AV1-5] linhas =
GerarRelatorio.cs - linha: 27
CadastroMembros.cs - linha: 41, 27

---

## Menu Console
O menu principal permite executar as funções mínimas:

```
=== LUDOTECA .NET ===
1 - Cadastrar jogo          // [AV1-4-Cadastrar]
2 - Excluir Jogo           // [AV1-4-Excluir]
3 - Cadastrar membro       // [AV1-4-Cadastrar]
4 - Excluir membro         // [AV1-4-Excluir]
5 - Listar jogos           // [AV1-4-Listar]
6 - Emprestar jogo         // [AV1-4-Emprestar]
7 - Devolver jogo          // [AV1-4-Devolver]
8 - Gerar relatório        // [AV1-4-Relatorio]
0 - Sair
```

---

## Regras e Validações

### Validações de Entrada:
- Nome de jogo e membro não podem estar vazios
- Oferece validação em caso de nomes duplicados
- Ano do jogo ≥ 1950 || ano do jogo > ano atual


### Regras de Negócio:
- Limite de 2 jogos por membro
- Data de devolução não pode ser anterior à data de empréstimo

### Debug e Logs:
- Debug log registra pelo menos duas exceções // [AV2-5]





