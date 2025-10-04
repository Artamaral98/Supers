# Projeto Supers

O presente projeto é composto por uma API construída com .NET CORE V 8.0 e um frontend desenvolvido com React + TypeScript e estilizado com tailwind css.

Para a arquitetura da API, o desenvolvimento foi realizado seguindo as boas práticas de programação, como abordagem do Domain Driven Design e Clean Architecture, design patterns como orientação a eventos e repositórios e aplicação de princípios SOLID.
Foram implementados testes unitários para os métodos de useCases, os testes utilizam Mocks afim de simular o ambiente real, visando garantir a plena funcionalidade e auxiliar na manutenção e implementações futuras.

# Fluxo do backend
O backend foi desenvolvido através de c# com ASP.NET com uma arquitetura baseada na abordagem Domain Driven Design (DDD), que consiste em dividir o sistema em quatro camadas principais: API, Application, Infrastrcture e Domain. 
Visando a organização, para este projeto foi realizado um desdobramento composto por duas adições a abordagem do DDD: Um projeto para exceções, responsável pelas exceções personalizadas do sistema, e um projeto de comunicação, onde estarão presentes os DTOs de requisições e resposta.

Responsabilidades dos projetos:
Api: Local onde são definidos os controllers, que recebem requisições e devolvem respostas (sucesso ou erro).

Appication: Projeto que recebe a requisição capturada e repassada pelo projeto e API. Neste projeto, são implementada as regras de negócio e verificações. Essa validação é realizada através de uma classe específica, usando a biblioteca FluentValidator, que checa
se todos os elementos da requisição estão válidos, caso contrário retorna uma mensagem de erro específica para a validação que não foi aprovada.
	
Infrastructure: Implementa os códigos que executam serviços externos a API, como Banco de Dados. A implementação do EntityFramework, criação das tabelas, migrations e conexão com SQL Server estão presentes neste projeto. Os métodos
responsáveis por criar, deletar, atualizar e listar (repositórios) estão presentes neste projeto.

Domain: Uma das ideias chave do DDD é criar uma linguagem comum a todos os envolvidos no projeto. Então todas as propriedades que compoem as entidades usuário ou transações estarão presentes neste 
projeto. Ex: nome, idade. Além disso, neste projeto estão situados as interfaces (contratos), que definem os métodos implementados em Infrastructure. Essa interface será recebida pelo projeto de 
Application, para implementar os métodos, separando as regras de negócio dos dados propriamente ditos, contidos em Infrastructure. Para isso, é utilizada a Injeção de dependência.

Exception: Projeto responsável por conter a base das exceções que serão tratadas e enviadas como resposta, além das mensagens de erros customizadas para cada caso.

Communication: Responsável por armazenar os modelos de Requests e Responses para cada funcionalidade da aplicação.

Fluxo: A requisição é recebida pela API -> API chama o controller pertinente de acordo com a rota, que chama o useCase do caso específico -> Requisição enviada para a regra de negócio (projeto de 
Application) dentro do useCase-> Após a validação, caso tudo esteja correto, haverá a adoção dos métodos do projeto de Infrastructure, repassados a através de uma interface para que seja 
realizada a operação pertinente, e haja a conversação com a base de dados e salvar as alterações.

Instruções para inicialização do projeto: Recomendo a utilização do Visual Studio 2022 para análise. Antes de tudo, cabe salientar que a aplicação utiliza banco de dados SQL server. Dessa forma, antes de inicializar o projeto, insira a Connection String referente ao seu login SQL server
- - "Connection": "Data Source=NOMEDOSEUSERVIDOR; Initial Catalog=SuperHerois;User ID=SEULOGONDOSQLSERVER;Password=SUASENHA;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
- A ConnectionString deverá sem implementada em "Connection": "". Você encontrará no diretório: Backend/src/Supers.API/appsettings/appsettings.Development
- - Não é necessária a criação manual do servidor/schema, apenas insira suas credenciais de login SQL SERVER e inicialize o projeto.

Após realizar a conexão com o banco de dados, siga as instruções para inicializar a aplicação:

Inicie a API teclando F5, ou cliando no botão de play acima.
Para iniciar o frontend, abra o terminal da solution com "ctrl + '" e insira os seguintes comandos:
  - cd frontend
  - cd supers.client
  - npm install
  - npm run dev
  - copie a url mostrada no terminal e cole em seu navegador.

Ao inciar a aplicação o swagger será aberto, demonstrando todos os endpoint do backend e suas funcionalidades.
Neste projeto é possível:
  - Criar um Super Herói.
  - Atualizar os dados de um Héroi já criado.
  - Deletar um herói.
  - Ou obter os dados de Herói ja cadastrado.
  - é possível também verificar a lista de super poderes cadastrados no sistema.
