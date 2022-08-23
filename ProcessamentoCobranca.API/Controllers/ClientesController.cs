using DocumentValidator;
using Microsoft.AspNetCore.Mvc;
using ProcessamentoCobranca.API.Models.DTO;
using ProcessamentoCobranca.Domain.Entities;
using ProcessamentoCobranca.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProcessamentoCobranca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteServices _clienteServices;

        public ClientesController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }

        // GET: api/<ClientesController>
        //[HttpGet]
        //public IEnumerable<Cliente> Get()
        //{
        //    //return new string[] { "value1", "value2" };
        //    var teste = _clienteServices.QueryAll();
        //    return teste;
        //}

        // GET api/<ClientesController>/5
        [HttpGet("cpf")]
        public ActionResult Get(string cpf)
        {
            try
            {
                if (CPFValidate(cpf))
                    return Ok(_clienteServices.QueryFilter(String.Empty, cpf));
                else
                    return BadRequest();
            }
            catch (Exception exception)
            {
                //_logger.LogError(exception, exception.Message, product);
                Response.StatusCode = 400;
                return new JsonResult($"Erro: {exception.Message}");
            }
        }

        // POST api/<ClientesController>
        [HttpPost]
        public ActionResult Post([FromBody] ClienteDTO cliente)
        {
            try
            {
                if (ClienteValidate(cliente))
                    _clienteServices.Insert(ClienteFill(cliente));

                return Ok(cliente);
            }
            catch (Exception exception)
            {
                //_logger.LogError(exception, exception.Message, product);
                Response.StatusCode = 400;
                return new JsonResult($"Erro: {exception.Message}");
            }
        }

        // PUT api/<ClientesController>/5
        [HttpPut("")]
        public ActionResult Put(string nome, string estado, string cpf)
        {
            try
            {
                var cliente = new ClienteDTO(nome, estado, cpf);
                if (ClienteValidate(cliente))
                    _clienteServices.Insert(ClienteFill(cliente));

                return Ok(cliente);
            }
            catch (Exception exception)
            {
                //_logger.LogError(exception, exception.Message, product);
                Response.StatusCode = 400;
                return new JsonResult($"Erro: {exception.Message}");
            }
        }

        // DELETE api/<ClientesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        private Cliente ClienteFill(ClienteDTO cliente)
        {
            return new Cliente(cliente.Nome.ToUpper(), cliente.Estado.ToUpper(), cliente.CPF.ToUpper());
        }

        private bool ClienteValidate(ClienteDTO cliente)
        {
            bool Validado = false;
            if (cliente != null)
            {
                if (!String.IsNullOrEmpty(cliente.Nome) || !String.IsNullOrEmpty(cliente.Estado) || !String.IsNullOrEmpty(cliente.CPF))
                {
                    if (cliente.Estado.Length > 2)
                        throw new Exception($"O campo Estado não pode ser maior que 2 caracteres");

                    if (cliente.Nome.Length > 80)
                        throw new Exception($"O campo nOME não pode ser maior que 80 caracteres");

                    if (!CpfValidation.Validate(cliente.CPF))
                        throw new Exception($"CPF Inválido!");

                    var clienteBase = _clienteServices.QueryFilter(String.Empty, cliente.CPF);
                    if (clienteBase != null)
                        throw new Exception($"CPF já cadastrado!");

                    Validado = true;
                }
                else
                {
                    throw new Exception($"Nenhum dos campos não podem estar em branco.");
                }
            }
            else
            {
                throw new Exception($"Nenhum dos campos não podem estar em branco.");
            }

            return Validado;
        }

        private bool CPFValidate(string cpf)
        {
            bool Validado = false;
            if (!String.IsNullOrEmpty(cpf))
            {
                if (!CpfValidation.Validate(cpf))
                    throw new Exception($"CPF Inválido!");

                Validado = true;
            }
            else
            {
                throw new Exception($"O campo CPF não podem estar em branco.");
            }

            return Validado;
        }
    }
}
