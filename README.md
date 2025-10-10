# ExpForge.Documentation

Este repositório contém a documentação oficial do projeto ExpForge, desenvolvida como uma aplicação Blazor. O objetivo é fornecer um guia abrangente sobre a estrutura, funcionalidades e uso do ExpForge, facilitando o entendimento e a colaboração.

## Estrutura do Projeto

O projeto é organizado da seguinte forma:

```
ExpForge.Documentation/
├── ExpForge.Documentation.sln
├── README.md
├── packages-microsoft-prod.deb
└── src/
    └── ExpForge.Documentation.Presentation/
        ├── App.razor
        ├── Components/
        │   └── Default/
        │       └── Code.razor
        ├── ExpForge.Documentation.Presentation.csproj
        ├── Layout/
        │   ├── MainBar.razor
        │   ├── MainFooter.razor
        │   └── MainLayout.razor
        ├── Pages/
        │   ├── Docs/
        │   │   ├── Commands/
        │   │   │   ├── Component/
        │   │   │   │   └── New-Component.razor
        │   │   │   ├── Documentation/
        │   │   │   │   └── Generate-Documentation.razor
        │   │   │   └── Widget/
        │   │   │       ├── New-Widget.razor
        │   │   │       └── Rename-Widget.razor
        │   │   ├── Explore.razor
        │   │   └── Getting-Started.razor
        │   ├── Home/
        │   │   └── Index.razor
        │   └── Index.razor
        ├── Program.cs
        ├── Properties/
        │   └── launchSettings.json
        ├── Services/
        │   └── ThemeService.cs
        ├── wwwroot/
        │   ├── 404.html
        │   ├── css/
        │   │   └── app.css
        │   ├── favicon.png
        │   ├── icon-192.png
        │   ├── img/
        │   │   ├── background.png
        │   │   └── icon.png
        │   ├── index.html
        │   └── lib/
        │       └── bootstrap/
        │           └── dist/
        │               ├── css/
        │               └── js/
        └── _Imports.razor
```

### Descrição dos Diretórios e Arquivos Principais:

-   **`ExpForge.Documentation.sln`**: O arquivo de solução principal do Visual Studio para o projeto.
-   **`src/`**: Contém o código-fonte da aplicação.
    -   **`ExpForge.Documentation.Presentation/`**: O projeto Blazor que hospeda a interface de documentação.
        -   **`App.razor`**: O componente raiz da aplicação Blazor.
        -   **`Components/`**: Contém componentes Blazor reutilizáveis.
            -   **`Default/Code.razor`**: Um componente para exibir blocos de código.
        -   **`Layout/`**: Define os layouts da aplicação.
            -   **`MainBar.razor`, `MainFooter.razor`, `MainLayout.razor`**: Componentes para a barra de navegação, rodapé e layout principal, respectivamente.
        -   **`Pages/`**: Contém as páginas navegáveis da aplicação.
            -   **`Docs/`**: Páginas dedicadas à documentação.
                -   **`Commands/`**: Documentação específica para comandos.
                    -   **`Component/New-Component.razor`**: Documentação para o comando de criação de novos componentes.
                    -   **`Documentation/Generate-Documentation.razor`**: Documentação para o comando de geração de documentação.
                    -   **`Widget/New-Widget.razor`, `Rename-Widget.razor`**: Documentação para comandos relacionados a widgets.
                -   **`Explore.razor`**: Página para explorar a documentação.
                -   **`Getting-Started.razor`**: Guia de primeiros passos.
            -   **`Home/Index.razor`**: Página inicial da seção Home.
            -   **`Index.razor`**: Página inicial principal da aplicação.
        -   **`Program.cs`**: O ponto de entrada da aplicação Blazor.
        -   **`Services/ThemeService.cs`**: Serviço para gerenciar temas da aplicação.
        -   **`wwwroot/`**: Contém arquivos estáticos como CSS, JavaScript, imagens e outros recursos.
            -   **`css/app.css`**: Estilos CSS personalizados da aplicação.
            -   **`img/`**: Imagens utilizadas na documentação.
            -   **`lib/bootstrap/`**: Biblioteca Bootstrap para estilização.

## Como Contribuir

Para contribuir com a documentação, siga os passos abaixo:

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/celinhodaltro/ExpForge.Documentation.git
    ```
2.  **Navegue até o diretório do projeto:**
    ```bash
    cd ExpForge.Documentation/src/ExpForge.Documentation.Presentation
    ```
3.  **Restaure as dependências:**
    ```bash
    dotnet restore
    ```
4.  **Execute a aplicação:**
    ```bash
    dotnet run
    ```
    A aplicação estará disponível em `https://localhost:5001` (ou outra porta configurada).
5.  **Crie ou edite arquivos `.razor`** nas pastas `Pages/Docs` para adicionar ou atualizar a documentação.
6.  **Envie suas alterações** através de um Pull Request.

## Licença

Este projeto está licenciado sob a licença MIT. Consulte o arquivo `LICENSE` (se existir) para mais detalhes.
