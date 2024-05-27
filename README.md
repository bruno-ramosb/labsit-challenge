# Desafio Labsit

É necessário docker e docker-compose para rodar a aplicação.

Na raiz do projeto executar o comando:
```shell
docker-compose up --build -d
```
A aplicacão fica disponivel na porta http:8080

Os testes unitarios são executadas juntos com docker-compose

Para facilitar o teste, foi criado um seed de um cadastro para realizar as operações, disponibilizados os endpoint em [postman_collection](./postman_collection.json) contém as rotas salvas no postman para facilitar o uso.

