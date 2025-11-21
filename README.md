# BalanceMe - Global Solution 2025

## 📋 Visão Geral

O **BalanceMe Academy** é uma plataforma web desenvolvida para enfrentar os desafios do "Futuro do Trabalho", focando na organização e disponibilização de conteúdos educacionais para *upskilling* e *reskilling*. A aplicação permite o gerenciamento de trilhas de aprendizado, conteúdos multimídia e categorização por níveis de dificuldade, servindo como um hub para o desenvolvimento contínuo de competências.

### 👥 Integrantes do Grupo
* **Jhonatta Lima Sandes de Oliveira** - RM: 560277
* **Rangel Bernadi Jordão** - RM: 560547
* **Lucas José Lima** - RM: 561160

**Turma:** 2TDSPA

---

## 🏗️ Decisões Arquiteturais

O projeto foi construído seguindo a arquitetura **MVC (Model-View-Controller)** utilizando **ASP.NET Core**, garantindo a separação de responsabilidades entre a interface do usuário, a lógica de negócios e o acesso a dados.

* **Framework:** .NET 8 / ASP.NET Core MVC.
* **ORM:** Entity Framework Core (EF Core) para manipulação de dados.
* **Banco de Dados:** Oracle Database.
* **Design Patterns:**
    * **Dependency Injection:** Utilizada para injetar o contexto do banco (`AppDbContext`) e serviços (`ContentService`) nos controladores.
    * **Service Layer:** Lógica de negócios encapsulada em serviços (`ContentService.cs`) para evitar controladores "gordos".
* **Front-end:** Razor Views com Bootstrap para estilização responsiva e layout consistente.
* **Validações:** Data Annotations (`[Required]`, `[StringLength]`) nos Models para garantir a integridade dos dados antes da persistência.

---

## 🗺️ Rotas e Navegação (Endpoints)

A aplicação utiliza **Attribute Routing** para personalizar as URLs, tornando-as mais descritivas (ex: `/academy` em vez de `/Contents`), além de rotas padrão para a área administrativa.

| Funcionalidade | Método HTTP | Rota / Endpoint | Descrição | Acesso |
| :--- | :---: | :--- | :--- | :---: |
| **Home (Redirecionamento)** | `GET` | `/` | Redireciona automaticamente para a listagem principal (`/academy`). | Público |
| **Listar Conteúdos** | `GET` | `/academy` | Página principal. Exibe cards de conteúdos, filtros (busca, categoria, nível) e paginação. | Público |
| **Detalhes do Conteúdo** | `GET` | `/academy/details/{id}` | Exibe o artigo completo, resumo, vídeo e imagem de capa de um conteúdo específico. | Público |
| **Login Administrativo** | `GET` | `/Admin/Login` | Exibe o formulário de login para administradores. | Público |
| **Autenticar Admin** | `POST` | `/Admin/Login` | Processa as credenciais (Usuário: `Admin` / Senha: `Admin123@`). | Público |
| **Logout** | `GET` | `/Admin/Logout` | Encerra a sessão do administrador e redireciona para a Home. | Admin |
| **Criar Conteúdo** | `GET` | `/academy/create` | Exibe o formulário de cadastro de novo conteúdo. | **Admin** |
| **Salvar Conteúdo** | `POST` | `/academy/create` | Processa a inclusão do novo registro no banco de dados. | **Admin** |
| **Editar Conteúdo** | `GET` | `/academy/edit/{id}` | Exibe o formulário de edição carregado com os dados atuais do conteúdo. | **Admin** |
| **Atualizar Conteúdo** | `POST` | `/academy/edit/{id}` | Processa as alterações realizadas no conteúdo. | **Admin** |
| **Confirmar Exclusão** | `GET` | `/academy/delete/{id}` | Exibe os detalhes do conteúdo para confirmação antes de apagar. | **Admin** |
| **Excluir Conteúdo** | `POST` | `/academy/delete/{id}` | Remove definitivamente o registro do banco de dados. | **Admin** |

> **Nota:** As rotas marcadas com acesso **Admin** verificam a sessão do usuário (`IsAdmin`) e redirecionam para a tela de login caso não esteja autenticado.


## 📸 Exemplos de Uso (Fluxos Principais)

### 1. Área Pública - "Academy"
O acesso principal é feito pela rota `/academy`. Nesta área, qualquer utilizador pode visualizar os conteúdos disponíveis.

<img width="1893" height="914" alt="image" src="https://github.com/user-attachments/assets/e25ceb5a-8410-46f4-98b2-2d892b66fc48" />



* **Visualização de Detalhes:**
    * Ao clicar no botão **"Ler Artigo"** num card, o utilizador é direcionado para `/academy/details/{id}`.
    * Esta página exibe o artigo completo (`ArticleBody`), a imagem de capa e o vídeo do YouTube incorporado (se houver `MediaUrl`).
<img width="1897" height="913" alt="image" src="https://github.com/user-attachments/assets/7bd5386f-2cd1-48d0-a808-acab916b0bc9" />


---

### 2. Área Administrativa - Gestão de Conteúdo
Para adicionar, editar ou remover conteúdos, é necessário estar autenticado como Administrador.

* **Login de Administrador:**
    * Aceda a `/Admin/Login`.
    * **Credenciais Padrão:**
        * **User:** `Admin`
        * **Password:** `Admin123@`
    * Após o login com sucesso, a sessão `IsAdmin` é ativada e o menu superior exibe as opções de gestão.

<img width="1900" height="913" alt="image" src="https://github.com/user-attachments/assets/1e0a54ea-2266-4d3f-a6e1-9a9bf2adc409" />


* **Criar Novo Conteúdo:**
    * Clique em **"Novo Conteúdo"** (rota `/academy/create`).
    * **Campos Obrigatórios:** Título, Resumo, Conteúdo (Max 2000 caracteres) e Categoria.
    * **Conteúdo Rico:** O campo "Corpo do Artigo" aceita texto longo para posts de blog.
    * **Multimédia:** Insira URLs válidas para a Imagem de Capa e Vídeo do YouTube.
    * Ao salvar, o sistema valida os dados; se houver erro (ex: resumo muito longo), o formulário é recarregado com mensagens de alerta.

<img width="1894" height="913" alt="image" src="https://github.com/user-attachments/assets/6e124390-1adc-436e-867a-15f4826c35fb" />


* **Edição:**
<img width="1896" height="915" alt="image" src="https://github.com/user-attachments/assets/54d5fa53-bac6-4967-8bf0-726ff0df0f12" />

* **Exclusão:**
<img width="1917" height="917" alt="image" src="https://github.com/user-attachments/assets/a3816f59-99ba-4b74-b757-2c7462dc91b9" />


---

## 🚀 Como Rodar o Projeto

### Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) instalado.
* Acesso a um banco de dados **Oracle**.
* Visual Studio 2022 ou VS Code.

#### 1. Configuração do Banco de Dados (Connection String)
O projeto espera uma conexão com o Oracle. Você deve configurar a string de conexão.
Edite o arquivo appsettings.json na raiz do projeto e substitua os valores:
```bash
"ConnectionStrings": {
  "OracleConnection": "Data Source=seu_datasource_oracle;User Id=seu_usuario;Password=sua_senha;"
}
```

#### 2. Aplicando Migrations
Para criar as tabelas no banco de dados, execute o comando abaixo na raiz do projeto (onde está o arquivo .csproj):
```
dotnet ef database update
```

#### 3. Executando a Aplicação
Após configurar o banco, inicie o servidor apertando F5 ou abrindo o terminal e digitando:
```
dotnet run
```




