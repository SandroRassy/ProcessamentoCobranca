namespace ProcessamentoCobranca.API.Models.DTO
{
    public class CobrancaDTO
    {
        public string DataVencimento { get; set; }
        public string CPF { get; set; }
        public string ValorCobranca { get; set; }

        public CobrancaDTO()
        {

        }

        public CobrancaDTO(string dataVencimento, string cpf, string valorcobranca)
        {
            DataVencimento = dataVencimento;
            CPF = cpf;
            ValorCobranca = valorcobranca;
        }
    }
}
