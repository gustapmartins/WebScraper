# Web Scraper

![App Screenshot](https://contraponto.digital/wp-content/uploads/2022/02/web-.jpg)

O Web Scraper é uma ferramenta para coletar informações de produtos de várias lojas online. Este projeto utiliza Selenium WebDriver para automatizar a navegação web e coletar os dados de produtos de sites específicos.

## Funcionalidades

- Navega automaticamente até a página de resultados de uma pesquisa por um produto específico.
- Extrai informações como título, preço, desconto e link de produtos encontrados.
- Salva os dados coletados em um arquivo JSON para análise posterior.

## Pré-requisitos

- .NET Core 3.1 ou superior
- Google Chrome instalado
- ChromeDriver compatível com a versão do Chrome instalada

## Instalação

1. Clone o repositório para o seu ambiente local.
2. Certifique-se de ter o .NET Core instalado em sua máquina.
3. Baixe o ChromeDriver compatível com a versão do Chrome instalada e adicione o diretório ao seu PATH.
4. Execute o comando `dotnet build` na raiz do projeto para compilar o código.

## Uso

1. Abra o arquivo `appsettings.json` e configure as opções necessárias, como o link para o site de onde deseja coletar os dados.
2. Execute o aplicativo com o comando `dotnet run`.
3. Aguarde até que o processo de coleta de dados seja concluído.
4. Os dados coletados serão salvos em um arquivo JSON no diretório especificado nas configurações.

## Documentação da Model

#### Mapeamento dos atributos necessários para fazer essa raspagem

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `title`      | `string` | **Obrigatório**. Titulo do produto  |

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `price`      | `string` | **Obrigatório**. Preço do produto |

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `linkAttribute`      | `string` | **Obrigatório**. Link do produto |

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `discount`      | `string` | **Opcional**. Desconto do produto (se houver) |

#### O Atributo discount e o preço são usandos para fazer a comparação e ver se houve uma redução de preço nos produtos

Recebe dois números e retorna a sua soma.

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues para relatar problemas ou propor novas funcionalidades. Se deseja contribuir com código, siga os passos:

1. Fork o repositório.
2. Crie uma branch para sua feature (`git checkout -b feature/nova-feature`).
3. Faça commit das suas alterações (`git commit -am 'Adiciona nova feature'`).
4. Faça push para a branch (`git push origin feature/nova-feature`).
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a MIT License. Consulte o arquivo [LICENSE](LICENSE) para obter mais detalhes.
