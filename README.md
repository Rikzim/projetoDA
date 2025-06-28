# Projeto de Desenvolvimento de Aplicações — iTasks

![Logo](logowhite.png)


**Curso Técnico Superior Profissional de Programação de Sistemas de Informação**  
**Unidade Curricular:** Desenvolvimento de Aplicações

## Elementos do Grupo
- Henrik Dambros — nº 2241589
- Martim Mendes — nº 2241559
- Afonso António — nº 2241602

---

## Pré-requisitos

### Instalação do SQL Server
1. Abrir **Visual Studio Installer**
2. Clicar **"Modify"** no Visual Studio
3. Ir a **"Individual components"**
4. Marcar **"SQL Server Express LocalDB"**
5. Marcar **"SQL Server Data Tools"**
6. Clicar **"Modify"** para instalar

### Verificar Instalação
- Abrir Visual Studio
- **View** → **SQL Server Object Explorer**
- Deve aparecer `(localdb)\MSSQLLocalDB`

---

## Instalação e Execução

### 1. Preparação
1. **Extrair o ficheiro .zip** para o local desejado
2. **Ir até à pasta:** `\Projeto\`

### 2. Compilação
1. **Executar o ficheiro:** `iTasks.sln` (duplo clique abre o Visual Studio)
2. **Compilar o projeto:**
   - No Visual Studio: `Build` → `Build Solution`
   - Ou usar o atalho: `Ctrl + Shift + B`

### 3. Execução
1. **Ir à pasta:** `bin\Debug\`
2. **Executar:** `iTasks.exe`
3. **Login padrão:**
   - **User:** `admin`
   - **Pass:** `admin`

---

## Informações da Aplicação

### Características
- Todos os dados são guardados numa base de dados local SQL Server
- A aplicação abre com a janela de **Login**
- As funcionalidades disponíveis variam conforme o tipo de utilizador:
  - **Gestor**
  - **Programador**
