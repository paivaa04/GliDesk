# 📌 Glidesk — Sistema de Atendimento Inteligente (Ticket System)

![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-blue)
![Backend](https://img.shields.io/badge/Backend-.NET%208-blueviolet)
![Mobile](https://img.shields.io/badge/Mobile-Android%20(Java)-green)
![Database](https://img.shields.io/badge/Database-SQL%20Server-red)
![Auth](https://img.shields.io/badge/Auth-JWT-orange)
![Cloud](https://img.shields.io/badge/Cloud-Azure-lightblue)

---

## 📖 Índice
1. [Visão Geral](#-visão-geral)
2. [Motivação](#-motivação)
3. [Mockup da Interface](#-mockup-da-interface)
4. [Arquitetura Geral](#-arquitetura-geral)
5. [Tecnologias Utilizadas](#-tecnologias-utilizadas)
6. [Estrutura do Repositório](#-estrutura-do-repositório)
7. [Roadmap do Projeto](#-roadmap-do-projeto)
8. [Autor](#-autor)

---

## 🚀 Visão Geral

**Glidesk** é um sistema completo de abertura e gestão de chamados (Tickets), projetado para uso interno em empresas, equipes de TI, suporte técnico e help desks.

Ele é composto por:

- **API REST em .NET 8**, hospedada na Azure  
- **Aplicativo Mobile Android (Java)** para abertura e acompanhamento de chamados  
- **Painel administrativo (futuro)**  
- **Integração com IA (futuro)** para classificação automática e sugestão de soluções  

O objetivo do Glidesk é oferecer um sistema moderno, intuitivo e poderoso, com foco em **organização, rastreabilidade e agilidade** no atendimento.

---

## 🎯 Motivação

O Glidesk nasceu da minha necessidade de:

- Aplicar na prática os conhecimentos adquiridos na **faculdade**
- Dominar tecnologias reais utilizadas no **mercado corporativo**
- Criar um projeto sólido para **portfólio** e preparação para o mercado de trabalho  
- Desenvolver um sistema que simula demandas reais de empresas, como:
  - controle de chamados  
  - priorização  
  - histórico  
  - anexos  
  - autenticação  
  - categorização por setor  

Este projeto representa minha evolução como desenvolvedor e minha preparação contínua para atuar profissionalmente.

---

## 🎨 Mockup da Interface

Abaixo está uma prévia visual de como ficará o design final do Glidesk:

![Mockup](./ChatGPT%20Image%2030%20de%20nov.%20de%202025,%2022_34_51.png)

_(Versões dark e light do layout de login)_

---

## 🏗 Arquitetura Geral

+---------------------+
| Aplicativo |
| Mobile Android |
| (Java) |
+----------+----------+
|
| HTTPS / JSON
|
+----------v----------+
| API REST .NET 8 |
| Autenticação JWT |
| Azure App Service |
+----------+----------+
|
|
+----------v----------+
| SQL Server (Azure)|
+---------------------+


### Fluxo principal:
1. Usuário cria conta e faz login → JWT é gerado  
2. Abre chamados via aplicativo  
3. API registra chamados, histórico, anexos e status  
4. Técnicos podem alterar status, resolver ou fechar  
5. Mobile acompanha tudo em tempo real  

---

## 🧰 Tecnologias Utilizadas

### **Backend (.NET / Azure)**
- .NET 8 Web API  
- Entity Framework Core  
- SQL Server Azure  
- JWT Authentication  
- Azure App Service  
- Swagger / OpenAPI  

### **Mobile**
- Android nativo  
- Java  
- Retrofit (REST client)  
- Material Design  

### **Infraestrutura & Boas práticas**
- Soft Delete  
- Histórico de mudanças  
- Seeds de Status & Prioridades  
- DTOs organizados  
- Controllers RESTful  
- Arquitetura escalável  

---

## 📁 Estrutura do Repositório

/
|-- README.md
|-- GlideskAPI/
| |-- README.md # documentação completa da API
| |-- Controllers/
| |-- Models/
| |-- DTO/
| |-- Data/
|
|-- Mobile/ (futuro)
|-- Docs/ (futuro: DER, diagramas, IA)


---

## 🛣 Roadmap do Projeto

### ✔️ Concluído
- Modelagem do banco (EF Core)
- API base (Autenticação, CRUD de chamados)
- Soft Delete
- Histórico de chamados
- Seeds (Status, Prioridade)
- Login & Register
- Publicação no Azure

### 🔄 Em desenvolvimento
- App Mobile Android
- Sistema completo de categorias e setores 

### 🚀 Futuro
- Chatbot IA para classificação de chamados
- Sugestão automática de solução
- Notificações push no mobile
- SLA / métricas de atendimento

---

## 👤 Autor

**Matheus Emanuel de Paiva**  
Desenvolvedor Back-End & Mobile  || Estudante de Análise e Desenvolvimento de Sistemas na UNIP, atualmente no 4º semestre (previsão para conclusão em Junho/26).
  

---
