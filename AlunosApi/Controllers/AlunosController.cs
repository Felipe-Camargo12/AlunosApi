using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Produces("application/json")]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Erro ao obter alunos");
            }
        }

        [HttpGet("Name")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoByName([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByName(nome);

                if (alunos == null)
                    return NotFound($"Não existe alunos com o nome {nome}");
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Request Inválido");
            }
        }

        private const string GetAlunoRouteName = "GetAluno";

        [HttpGet("{id:int}", Name = GetAlunoRouteName)]
        public async Task<ActionResult<Aluno>> GetAlunoById(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAlunoById(id);
                if (aluno == null)
                    return NotFound($"Não existe aluno com id: {id}");
                return Ok(aluno);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Request Inválido");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(GetAlunoRouteName, new { id = aluno.Id }, aluno);
            }
            catch
            {
                return BadRequest("Request inválido!");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"O aluno de id {id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Id não é correspondente");
                }
            }
            catch
            {
                return BadRequest("Request inválido ");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAlunoById(id);
                if (aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($"O aluno com ID {id} foi deletado!");
                }
                else
                {
                    return NotFound($"Aluno com ID {id} não encontrado.");
                }
            }
            catch
            {
                return BadRequest("Request inválido");
            }
        }
    }
}

