using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace PocProjetoCache.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IDistributedCache _cache;

        public UsuarioController(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }

        [HttpDelete]
        public IActionResult Delete(string usuario)
        {
            var value = _cache.Get(usuario);

            if (value is null)
                return NotFound("Usuario nao encontrado");

            _cache.Remove(usuario);

            return Ok("Usuario excluido com sucesso");
        }

        [HttpGet]
        public IActionResult Get(string usuario)
        {
            if (string.IsNullOrEmpty(usuario))
                return BadRequest("Parametros incorretos. Favor enviar o usuario desejado");

            var value = _cache.GetString(usuario);

            if (value is null)
                return NotFound("Usuario nao encontrado");

            return Ok(value);
        }

        [HttpPut]
        public IActionResult Set(string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(senha))
                return BadRequest("Parametros incorretos. Favor enviar usuario e senha");
            
            _cache.SetString(usuario, senha);

            return Ok("Usuario cadastrado com sucesso!");
        }
    }
}