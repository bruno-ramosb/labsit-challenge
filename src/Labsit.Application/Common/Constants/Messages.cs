namespace Labsit.Application.Common.Constants
{
    public static class Messages
    {
        public const string SUCCESSUL_OPERATION = "Operação realizada com sucesso.";
        public const string SUCCESSUL_SHOP = "Compra realizada com sucesso.";
        public const string CUSTOMER_BE_AT_LEAST_18_YEARS_OLD = "O cliente deve ter pelo menos 18 anos.";
        public const string DOCUMENT_ALREADY_IN_USE = "O CPF já está em uso.";
        public const string CUSTOMER_ALREADY_HAVE_BANKACCOUNT = "O cliente já possui conta bancária.";
        public const string CARD_ALREADY_CREATED = "Cartão já cadastrado.";
        public const string VALUE_MUST_BE_GREATER_THAN_ZERO = "Valor deve ser maior do que 0.";
        public const string INSUFFICIENT_FUNDS = "Saldo insuficiente.";

        #region Document fields
        public const string DOCUMENT_INVALID = "CPF inválido.";
        #endregion

        #region Not found
        public const string BANK_ACCOUNT_NOT_FOUND = "Conta bancária não encontrada.";
        public const string CUSTOMER_NOT_FOUND = "Cliente não encontrado.";
        #endregion

        #region Required fields
        public const string NAME_REQUIRED = "O nome é obrigatório.";
        public const string TRANSCTION_TYPE_REQUIRED = "Tipo da transação é obrigatório.";
        public const string DESCRIPTION_REQUIRED = "Descrição é obrigatório.";
        public const string BANK_ACCOUNT_ID_REQUIRED = "ID da conta bancária é obrigatório.";
        public const string CUSTOMER_ID_REQUIRED = "ID do cliente é obrigatório";
        #endregion

        #region Card validation
        public const string INVALID_CARD_DETAILS = "Dados do cartão inválido.";
        public const string VERIFICATION_CODE_REQUIRED = "Código de segurança obrigatório";
        public const string EXPIRED_CARD = "Cartão expirado.";
        public const string VERIFICATION_CODE_LENGHT_MUST_BE_EQUALS_3 = "Código de segurança deve conter 3 dígitos.";
        public const string BRAND_REQUIRED = "Bandeira é obrigatório.";
        public const string CARD_NUMBER_REQUIRED = "Número do cartão é obrigatório.";
        public const string HOLDER_NAME_REQUIRED = "Nome do portador é obrigatório.";
        #endregion
    }
}
