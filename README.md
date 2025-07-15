# Skopia_NET


A API ControleLancamento foi contruida da seguinte forma:

	* net 8 - Devido ser uma versão de suporte de longo tempo pela microsoft e ser a mais recente lançada;
	
	* Sqlite - Foi escolhido esse banco pela sua portabilidade e devido a sua criação ser um arquivo com final .db e o banco é executado de forma local;
	
	* EntityFramework - Foi escolhido esse ORM para que o banco de dados ficasse "desacoplado", facilitando uma migração de banco bastando alterar o drive de conexão. Além disso, facilita a interação com a base de dados, podendo utilizar o LINQ para interações com a base e sem precisar "re-escrever" nunhuma instrução junto ao banco ;
	
	* Migration - Foi escolhido para que a base seja criada / atualizada de forma automátizada;
	
	* CRQS - Foi utilizada essa "arquitetura" visando dar maior agilidade a aplicação e podendo até termos a utilização de um banco NoSQL para efetuação das consultas e um banco relacional para a persistência dos dados. Em alguns casos, essa arquitetura é utilizada ( como num e-commerce, por exemplo ) para agilizar o carregamento de informações na tela pelo front ( teria um processo rodando em backend que atualiza o banco NoSql com as informações persistidas pelo banco relacional ou podemos usar um serviço de fila (RabbitMQ ou ServiceBus, por exemplo) para que esse sincronismo seja feito );
	
			No caso do CQRS utilizado as seguintes classse:  
			
				* Query - Classe que utilizamos para "iniciar" os orquestradores ( Handler ) de pesquisas ( querys ). Foi utilizada a bibliotecar Flunt.Br para efetuar as validações das informações enviadas pelo controller;
				
				* Command - Classe que utilizamos para "iniciar" os orquestradores ( Handler ) de comandos na aplicação ( insert, update, deletes, processamentos ). Foi utilizada a bibliotecar Flunt.Br para efetuar as validações das informações enviadas pelo controller;
				
				* Handler - Classe que permite que possamos "orquestras" as ações e retornar para o controller. usamos o conceito de "Fail fast". Utilizamos o método Validate da classe Query ou command logo no inicio da execução do handle, para que caso tenha algum problema de validação, ocorra o erro logo e fique mais rápido a execução do sistema, já que não fará interações com bancos, com api's externas e etc.
				
			
	* Mediator - Utilizado para que baste acionar a classe Query ou Command e o mediator consegue localizar o handler que utiliza essa classe e iniciar o processamento correto;
	
	* Xunit - Utilizado para efetuar os testes unitários da api. Escolhido por ser o mais utilizado na plataforma Microsoft, incluisive sendo recomendado pela empresa;
	
Para a execução do serviço, basta executa-lo, pois irá aplicar a criação do banco de dados autimaticamente, pois foi configurado para que o migration execute na subida da aplicação e caso não tenha o banco ou nenhuma alteração aplicada no banco, será atualizado.

	
	Para execução no docker, basta executar o comando docker-compose up --build no diretório que o código foi baixado

        Temos cadastrados 2 usuários com perfis diferentes:
		* Manager - Carlos, com Uuid - 5cb61a7e-2c0b-41ca-a03b-18d01c0daa3f
  		* User - Alberto, com Uuid - 6c4c7515-3030-4998-8541-a2a3b168ced4

 	Não estamos "espondo" os id's dos registros das tabelas, e sim o Uuid para aumentar a segurança
	
Refinamento:
	
	Visando melhorias futuras segue perguntas:
	
	* Qual seria a volumetria da utilização do sistema? ( visando alteração de arquitetura );

 	* Devemos utilizar JWT para validação da permissão de utilização das API's?
	
Final:

    Visando a manutenção de rastreabilidade do sistema, as deleções não são "físicas" e sim "lógicas", permitindo gerar relatórios mais completos para entendimento da evolução das informações.
	
	* Se a volumetria de usuários for um número considerável, alterar o banco de dados para um banco mais potente, como Oracle ,Microsoft SQL Server ou Postgre. Possivel implementação de microserviços para podermos ganhar escalabilidade em volume de consumo da api e estrutura já preparada para a inclusão de um banco não relacional para a recuperação das informações de tasks pendentes para que a carga das telas de visualizações sejam mais rápidas ( possivel uso de MongoDB )
	
	* Além de gravar as ações de alteração das tasks, também deveriamos incluir log's de execução na api.

 	* Com a inclusão de um JWT, não precisariamos mais informar nas chamadas das API's qual o Uuid do usuário. Essa informação viria dentro do JWT e com isso, validariamos o usuário e seu perfil internamente, evitando o recebimento de mais uma informação por parametro e aumentando a segurança da api;
	
 	
	

	
