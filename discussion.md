# Ressalvas
Foram tomados alguns pressupostos:
- Dado que o banco disponibiliza um cartão primário, os saldos e limites foram atreladas ao banco, na regra atual é permitido a geração de um único cartão na conta bancária, mas possibilitaria a geração e controle de saldo em diversos cartões.
- Os layers da aplicação foi simplificado para facilitar o desenvolvimento
- O serviço de pagamento e validação do cartão foi simplificado no repository, fazendo o papel do gateway para aprovação da compra
- Não foram feitas todas coberturas de testes
