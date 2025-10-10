# Explicação das Alterações no CI/CD do ExpForge.Documentation

Este documento detalha as melhorias e as regras inseridas no fluxo de CI/CD do projeto `ExpForge.Documentation`, com foco em melhores práticas e na manutenção da funcionalidade de auto-merge.

## Arquivo de Workflow: `main.yml`

O arquivo `main.yml` localizado em `.github/workflows/main.yml` define o fluxo de trabalho do CI/CD. As principais seções e alterações são explicadas abaixo:

### 1. Gatilhos (`on`)

O workflow é acionado nas seguintes situações:

-   **`push` para `master` e branches `documentation**`**: Qualquer push para a branch `master` ou para qualquer branch que comece com `documentation` (ex: `documentation-feature`, `documentation-fix`) irá disparar o workflow.
-   **`pull_request` para `master`**: Abertura ou atualização de um Pull Request (PR) direcionado à branch `master` também aciona o workflow.

```yaml
on:
  push:
    branches: [ master, 'documentation**' ]
  pull_request:
    branches: [ master ]
```

### 2. Permissões (`permissions`)

As permissões foram explicitamente definidas para seguir o princípio do menor privilégio. O workflow agora solicita permissões de `write` para `contents` e `pull-requests`, que são necessárias para criar releases, fazer deploy para GitHub Pages e para a funcionalidade de auto-merge.

```yaml
permissions:
  contents: write
  pull-requests: write
```

### 3. Variáveis de Ambiente (`env`)

Uma variável de ambiente `CSPROJ_PATH` foi definida para o caminho do arquivo `.csproj`, tornando o workflow mais fácil de manter e menos propenso a erros se a estrutura do projeto mudar ligeiramente.

```yaml
env:
  CSPROJ_PATH: ./src/ExpForge.Documentation.Presentation/ExpForge.Documentation.Presentation.csproj
```

### 4. Job: `build`

Este job é responsável por construir e publicar a aplicação Blazor. Ele foi aprimorado com nomes mais descritivos para os passos e a inclusão de `fetch-depth: 0` para garantir que o histórico completo do Git esteja disponível para a verificação de versão.

-   **`Checkout repository`**: Clona o repositório.
-   **`Setup .NET SDK`**: Configura o SDK do .NET 9.0.x.
-   **`Restore dependencies`**: Restaura as dependências do projeto.
-   **`Publish Blazor application`**: Publica a aplicação Blazor em modo `Release`.
-   **`Adjust base href for GitHub Pages`**: Modifica o `base href` no `index.html` para que a aplicação funcione corretamente no GitHub Pages, que geralmente serve projetos em um subdiretório (`/ExpForge.Documentation/`).
-   **`Check version difference`**: Este passo verifica a versão do projeto no `.csproj` e compara com a última tag de release. Se houver uma nova versão, ele define variáveis de saída para os jobs `deploy` e `release`.
-   **`Upload artifact`**: Salva os arquivos publicados como um artefato, que será usado pelo job `deploy`.

### 5. Job: `auto-merge`

Este job é crucial para manter a sincronização dos comandos e garantir que as branches de documentação sejam mescladas automaticamente. As regras para o auto-merge são:

-   **Condição (`if`)**: O auto-merge só será executado se:
    -   O Pull Request for criado pelo **dono do repositório** (`github.actor == github.repository_owner`).
    -   A branch de origem (`head_ref`) do Pull Request contiver a string `documentation` (`contains(github.head_ref, 'documentation')`).
-   **Dependência (`needs`)**: Este job só será executado após o job `build` ser concluído com sucesso.
-   **Permissões**: Requer permissões de `write` para `contents` e `pull-requests`.
-   **Passos**: 
    -   **`Debug info`**: Adicionado para facilitar a depuração, mostrando informações do contexto do GitHub Actions.
    -   **`Checkout repository for auto-merge`**: Clona o repositório novamente.
    -   **`Install GitHub CLI`**: Instala a ferramenta de linha de comando do GitHub.
    -   **`Authenticate GitHub CLI`**: Autentica o CLI usando um `GH_TOKEN` (que deve ser um Personal Access Token com permissões adequadas para merge de PRs, armazenado como um segredo do GitHub).
    -   **`Auto-merge Pull Request`**: Utiliza o `gh CLI` para mesclar o Pull Request. As opções `--squash` (mescla todos os commits em um único commit), `--admin` (permite ignorar restrições de branch se o token tiver permissão) e `--delete-branch` (exclui a branch de origem após o merge) são usadas para um fluxo de trabalho limpo.

```yaml
  auto-merge:
    runs-on: ubuntu-latest
    if: (github.actor == github.repository_owner ) && contains(github.head_ref, 'documentation') 
    needs: build

    permissions:
      contents: write
      pull-requests: write

    steps:
      - name: Debug info (para verificar contexto)
        run: |
          echo "Event: ${{ github.event_name }}"
          echo "Actor: ${{ github.actor }}"
          echo "Owner: ${{ github.repository_owner }}"
          echo "Head ref: ${{ github.head_ref }}"
          echo "Base ref: ${{ github.base_ref }}"

      - name: Checkout repository for auto-merge
        uses: actions/checkout@v4

      - name: Install GitHub CLI
        run: |
          sudo apt-get update
          sudo apt-get install -y gh

      - name: Authenticate GitHub CLI
        run: gh auth setup-git
        env:
          GH_TOKEN: ${{ secrets.GH_TOKEN }}

      - name: Auto-merge Pull Request
        run: |
          gh pr merge ${{ github.event.pull_request.number }} --squash --admin --delete-branch
        env:
          GH_TOKEN: ${{ secrets.GH_TOKEN }}
```

**Importante**: Para que o `auto-merge` funcione, você precisará configurar um `GH_TOKEN` nos segredos do seu repositório GitHub. Este token deve ter permissões para `repo` (para acesso completo ao repositório) e `pull_requests` (para gerenciar PRs).

### 6. Job: `deploy`

Este job é responsável por fazer o deploy da aplicação Blazor para o GitHub Pages.

-   **Condição (`if`)**: Só é executado se o push for para a branch `master` e se houver uma nova versão detectada pelo job `build`.
-   **Dependência (`needs`)**: Depende do job `build`.
-   **Passos**: 
    -   **`Checkout repository for deploy`**: Clona o repositório.
    -   **`Download build artifact`**: Baixa o artefato gerado pelo job `build`.
    -   **`Deploy to GitHub Pages`**: Utiliza a action `peaceiris/actions-gh-pages@v4` para fazer o deploy. O `GITHUB_TOKEN` é usado para autenticação, e os arquivos são publicados na branch `gh-pages` a partir do diretório `release/wwwroot`.

### 7. Job: `release`

Este job cria um novo release no GitHub quando uma nova versão é detectada.

-   **Condição (`if`)**: Só é executado se o push for para a branch `master` e se houver uma nova versão detectada pelo job `build`.
-   **Dependências (`needs`)**: Depende dos jobs `build` e `deploy`.
-   **Passos**: 
    -   **`Checkout repository for release`**: Clona o repositório.
    -   **`Create GitHub Release`**: Utiliza a action `actions/create-release@v1` para criar um release com base na tag de versão gerada pelo job `build`.

## Regras Inseridas e Melhores Práticas

As seguintes regras e melhores práticas foram incorporadas:

1.  **Princípio do Menor Privilégio**: As permissões do workflow (`permissions`) foram explicitamente definidas para conceder apenas o acesso necessário, reduzindo o risco de segurança.
2.  **Nomes Descritivos**: Os nomes dos passos (`name`) foram tornados mais claros e descritivos para melhorar a legibilidade e a manutenção do workflow.
3.  **Reutilização de Variáveis**: O uso de `env.CSPROJ_PATH` centraliza a configuração de caminhos, facilitando futuras alterações.
4.  **Verificação de Versão**: A lógica de `version-check` garante que releases e deploys só ocorram quando uma nova versão for realmente detectada no `.csproj`, evitando deploys desnecessários.
5.  **Auto-Merge Controlado**: O auto-merge é restrito a Pull Requests de branches de documentação criados pelo dono do repositório, garantindo que apenas conteúdo de documentação validado e de fontes confiáveis seja mesclado automaticamente.
6.  **Uso de `gh CLI` para Auto-Merge**: A utilização do `gh CLI` para o auto-merge é uma prática robusta, permitindo controle granular sobre o processo de merge (squash, delete branch).
7.  **Artefatos de Build**: O uso de artefatos (`actions/upload-artifact` e `actions/download-artifact`) garante que os jobs `deploy` e `release` trabalhem com os mesmos arquivos gerados pelo `build`, promovendo consistência.
8.  **Tokens de Acesso**: A necessidade de `GITHUB_TOKEN` e `GH_TOKEN` (para o auto-merge) é destacada, reforçando a importância da gestão segura de segredos.

Essas alterações visam criar um pipeline de CI/CD mais seguro, eficiente e fácil de manter, ao mesmo tempo em que automatiza o processo de merge para branches de documentação, conforme solicitado.
