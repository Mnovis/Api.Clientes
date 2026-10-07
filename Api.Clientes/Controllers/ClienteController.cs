using Api.Clientes.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Clientes.Controllers
{
    [Route("api/v1/clientes")]
    [ApiController]
    public class ClienteController (ClienteRepository clienteRepository) : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Dictionary<string, object> request)
        {
            // Criando um objeto da classe Cliente
            var cliente = new Cliente();

            // Capturando os dados
            cliente.Nome = request["nome"].ToString()!;
            cliente.Email = request["email"].ToString()!;
            cliente.Cpf = request["cpf"].ToString()!;

            // Salvando no banco de ddados
            clienteRepository.Inserir(cliente);
            return Ok(new 
            {
                mensagem = "Cliente cadastrado com sucesso!",
                dados = cliente
            });
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Dictionary<string, object> request)
        {
            var cliente = clienteRepository.ObterPorId(id);
            if (cliente == null)
            {
                return NotFound(new { mensagem = "Cliente não encontrado para edição." });
            }

            // Modificando os dados
            cliente.Nome = request["nome"].ToString()!;
            cliente.Email = request["email"].ToString()!;
            cliente.Cpf = request["cpf"].ToString()!;

            // Atualizando no banco de ddados
            clienteRepository.Atualizar(cliente);
            return Ok(new
            {
                mensagem = "Cliente atualizado com sucesso!",
                dados = cliente
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var cliente = clienteRepository.ObterPorId(id);
            if (cliente == null)
            {
                return NotFound(new { mensagem = "Cliente não encontrado para exclusão." });
            }

            clienteRepository.Excluir(id);

            return Ok(new
            {
                mensagem = "Cliente excluído com sucesso!",
                dados = cliente
            });
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clientes = clienteRepository.ObterTodos();

            return Ok(clientes); 
        }
    }
}
