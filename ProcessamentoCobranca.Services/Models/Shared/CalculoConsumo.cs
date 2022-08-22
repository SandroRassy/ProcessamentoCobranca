using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessamentoCobranca.Services.Models.Shared
{
    public interface CalculoConsumo
    {
        string cpf { get; set; }
        string idBoleto { get; set; }
    }
}
