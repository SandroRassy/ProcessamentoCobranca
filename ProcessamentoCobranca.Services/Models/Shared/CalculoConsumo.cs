namespace ProcessamentoCobranca.Services.Models.Shared
{
    public interface CalculoConsumo
    {
        string cpf { get; set; }
        string idBoleto { get; set; }
    }
}
