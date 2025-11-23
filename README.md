# BalanceMe Academy 

> **Global Solution 2025 | FIAP**
>
> DevOps Tools & Cloud Computing

O projeto BalanceMe Academy é o módulo corporativo web de educação e bem-estar da solução "BalanceMe". O aplicativo mobile diagnostica o problema com os dados do usuário (ex: usuário cansado, estressado, ou trabalhando demais). E a plataforma web entra como solução.

A plataforma desenvolvida em ASP.NET Core MVC, é voltada para a gestão de conteúdo de reskilling e upskilling. É um portal onde o RH da empresa disponibiliza conteúdo para ajudar os colaboradores a desenvolverem as "competências humanas", ou dicas de saúde e bem-estar.

No contexto da disciplina de DevOps Tools & Cloud Computing, este projeto demonstra a implantação de uma arquitetura distribuída em nuvem (Microsoft Azure), segregando a camada de aplicação (Front-end) da camada de persistência (Banco de Dados) em servidores distintos, simulando um ambiente de produção real com interconectividade segura.

### Identificação do Grupo
* **Jhonatta Lima Sandes de Oliveira** - RM: 560277
* **Rangel Bernadi Jordão** - RM: 560547
* **Lucas José Lima** - RM: 561160

**Turma:** 2TDSPA

---

## Arquitetura de Infraestrutura (Azure)

A solução foi implantada utilizando a infraestrutura como serviço (IaaS) na Microsoft Azure, composta por dois ambientes virtualizados interligados por uma rede privada virtual (VNet).

### 1. Servidor de Aplicação (Front-end)
* **Função:** Hospedagem da aplicação web ASP.NET Core.
* **Sistema Operacional:** Linux Ubuntu Server 22.04 LTS.
* **Especificações:** Standard B2als v2 (2 vCPUs, 4 GiB RAM).
* **Runtime:** .NET 9.0 SDK.
* **Rede:**
    * IP Público exposto (Portas 80/HTTP e 22/SSH).
    * Comunicação interna liberada para o servidor de banco de dados.

### 2. Servidor de Banco de Dados (Back-end)
* **Função:** Persistência de dados relacionais.
* **Sistema Operacional:** Windows Server 2022 Datacenter (Azure Edition).
* **SGBD:** Oracle Database 21c Express Edition (XE).
* **Especificações:** Standard B2as v2 (2 vCPUs, 8 GiB RAM).
* **Segurança:**
    * Sem acesso direto via HTTP público.
    * Regras de NSG (Network Security Group) configuradas para aceitar conexões na porta 1521 (TCP) exclusivamente provenientes do IP Privado do Servidor de Aplicação.

---

## Arquitetura de Software

O projeto segue o padrão MVC (Model-View-Controller) e utiliza as seguintes tecnologias:

* **Framework:** .NET 9.0 (ASP.NET Core MVC).
* **ORM:** Entity Framework Core com provider Oracle.
* **Front-end:** Razor Views com Bootstrap 5.
* **Banco de Dados:** Oracle Database.

### Decisões de Design
* **Injeção de Dependência:** Utilizada para desacoplar o contexto de dados e serviços da camada de controle.
* **Segregação de Ambientes:** A aplicação foi desenhada para não depender de configurações locais, permitindo a troca dinâmica da string de conexão via variáveis de ambiente ou arquivo de configuração (appsettings.json).
* **Migrações Automáticas:** Implementação de rotina no startup da aplicação para aplicar migrações pendentes do Entity Framework automaticamente ao iniciar o serviço, facilitando o deploy contínuo.

---

## Endpoints Principais

A aplicação expõe as seguintes rotas principais:

| Método | Rota | Descrição | Acesso |
| :--- | :--- | :--- | :--- |
| GET | / | Redireciona para a listagem principal. | Público |
| GET | /academy | Listagem de conteúdos e filtros. | Público |
| GET | /academy/details/{id} | Visualização detalhada do conteúdo. | Público |
| GET | /Admin/Login | Acesso à área administrativa. | Público |
| POST | /academy/create | Cadastro de novos conteúdos. | Admin |
| POST | /academy/edit/{id} | Edição de conteúdos existentes. | Admin |
| POST | /academy/delete/{id} | Exclusão de conteúdos. | Admin |

---
