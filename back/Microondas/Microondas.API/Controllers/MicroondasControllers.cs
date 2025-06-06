using Microondas.API.Model.DataTransferObject;
using Microondas.API.Model.Entity;
using Microondas.API.Model.Service;
using Microondas.API.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Microondas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MicroondasControllers : ControllerBase
    {
        private MicroondasService _service;

        public MicroondasControllers()
        {
            _service = new MicroondasService();
        }

        [HttpPost]
        public IActionResult Insert([FromBody] MicroondasDto opcao)
        {
            try
            {
                var novaOpcao= _service.InstanciarObjeto(opcao, null);
                _service.Insert(novaOpcao);
                return Ok(ResponseWrapper<int>.CreateSuccessResponse(novaOpcao.id, "Opção inserida com sucesso."));
            }
            catch (Exception msg)
            {
                return BadRequest(ResponseWrapper<string>.CreateErrorResponse("Erro ao inserir nova opção: " + msg.Message));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MicroondasDto opcao)
        {
            try
            {
                if (opcao == null)
                    return BadRequest(ResponseWrapper<string>.CreateErrorResponse("Dados inválidos."));

                var updateOpcao = _service.InstanciarObjeto(opcao, id);
                _service.Update(updateOpcao);
                return Ok(ResponseWrapper<MicroondasEntity>.CreateSuccessResponse(updateOpcao, "Sucesso"));
            }
            catch (Exception msg)
            {
                return BadRequest(ResponseWrapper<string>.CreateErrorResponse("Erro ao atualizar opção: " + msg.Message));
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _service.DeleteById(id);
                return Ok(ResponseWrapper<string>.CreateSuccessResponse("Opção excluída com sucesso."));
            }
            catch (Exception msg)
            {
                return BadRequest(ResponseWrapper<string>.CreateErrorResponse("Erro ao excluir opção: " + msg.Message));
            }
        }


        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            try
            {
                MicroondasEntity? opcao = _service.FindById(id);
                if (opcao == null)
                {
                    return NotFound(ResponseWrapper<MicroondasEntity>.CreateErrorResponse("Opção não encontrada."));
                }
                return Ok(new ResponseWrapper<MicroondasEntity>(opcao, "Sucesso"));
            }
            catch (Exception msg)
            {
                return BadRequest(ResponseWrapper<string>.CreateErrorResponse("Erro ao buscar opção: " + msg.Message));
            }

        }

        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {
                List<MicroondasEntity> opcoes = _service.FindAll();
                return Ok(new ResponseWrapper<List<MicroondasEntity>>(opcoes, "Sucesso"));
            }
            catch (Exception msg)
            {
                return BadRequest(ResponseWrapper<string>.CreateErrorResponse("Erro ao buscar opções: " + msg.Message));
            }
        }
    }
}
