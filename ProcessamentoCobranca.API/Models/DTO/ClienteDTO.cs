namespace ProcessamentoCobranca.API.Models.DTO
{
    public class ClienteDTO
    {
        public string Nome { get; set; }
        public string Estado { get; set; }
        public string CPF { get; set; }

        public ClienteDTO(string nome, string estado, string cpf)
        {
            Nome = nome;
            Estado = estado;
            CPF = cpf;
        }
    }
}
