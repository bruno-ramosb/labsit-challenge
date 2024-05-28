# Ressalvas
Foram tomados alguns pressupostos:
- Dado que o banco disponibiliza um cartão primário, os saldos e limites foram atreladas ao banco, na regra atual é permitido a geração de um único cartão na conta bancária, mas possibilitaria a geração e controle de saldo em diversos cartões.
- O depósito de crédito aumenta o limite total do cliente e ajusta o limite disponível, nas compras e retirada apenas o limite disponivel é retirado
- Os layers da aplicação foi simplificado para facilitar o desenvolvimento
- O serviço de pagamento e validação do cartão foi simplificado no repository, fazendo o papel do gateway para aprovação da compra
- Foram utilizados FluentValidation para validações dos campos no lugar do Data Annotation e Model builder para configurar as entidades no banco de dados
- Não foram feitas todos as buscas
- Não foram feitas todas coberturas de testes
