using DocumentacaoSwagger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentacaoSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly Contexto _contexto;

        public SeriesController(Contexto contexto)
        {
            _contexto = contexto;
        }


        /// <summary>
        /// Pega todas as series cadastradas
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição
        /// GET /Series
        /// </remarks>
        /// <returns>Lista de series cadastradas</returns>
        /// <response code="200">Lista de series cadastradas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Serie>>> PegarTodosAsync()
        {
            return await _contexto.Series.ToListAsync();
        }

        /// <summary>
        /// Salva a serie recebida
        /// </summary>
        /// <param name="serie">Serie a ser salva no banco de dados</param>
        /// <remarks>
        /// Exemplo de requisição
        /// POST /Series
        /// {
        ///     "SerieId" : "Não precisa preencher"
        ///     "Titulo" : "Star Trek",
        ///     "Sinopse" : "Tudo começou quando uma estrela super massiva explodiu...",
        ///     "Temporada" : 4",
        ///     "NumeroEpisodios" : 25",
        ///     "Elenco" : Maria Albergue, Shako Takamoto, Suzana Viera",
        ///     "Categoria" : Luta, Romance, Guerra"
        /// }
        /// </remarks>
        /// <returns>Mensagem de confirmação que a serie foi salva</returns>
        /// <response code="200">A Serie foi salva corretamente</response>
        /// <response code="400">Dados inválidos</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Serie>> SalvarAsync(Serie serie)
        {
            if (ModelState.IsValid)
            {
                await _contexto.AddAsync(serie);
                await _contexto.SaveChangesAsync();
                return Ok(new
                {
                    mensagem = $"A serie {serie.Titulo}, {serie.Temporada}ª temporada foi salva corretamente"
                });
            }

            return BadRequest(new
            {
                mensagem = "Dados inválidos"
            });

        }

        /// <summary>
        /// Pega uma serie baseada em seu id
        /// </summary>
        /// <param name="serieId">Id usado para pegar a serie</param>
        /// <remarks>
        /// Exemplo de requisição
        /// GET Series/90
        /// {
        ///     "serieId" : 90,
        ///     "Titulo" : "Star Trek",
        ///     "Sinopse" : "Tudo começou quando uma estrela super massiva explodiu...",
        ///     "Temporada" : 4",
        ///     "NumeroEpisodios" : 25",
        ///     "Elenco" : Maria Albergue, Shako Takamoto, Suzana Viera",
        ///     "Categoria" : Luta, Romance, Guerra"
        /// }
        /// </remarks>
        /// <returns> Serie com base em seu identificador</returns>
        /// <response code="200"> Retorna a serie pega no banco de dados</response>
        /// <response code="400"> Identificador inválido</response>
        /// /// <response code="404"> Serie não encontrada</response>
        [HttpGet("{serieId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Serie>> PegarPeloIdAsync(string serieId)
        {
            if (string.IsNullOrEmpty(serieId))
            {
                return BadRequest(new
                {
                    mensagem = $"O identificador é inválido"
                });
            }

            else
            {
                if(!int.TryParse(serieId, out int value))
                {
                    return BadRequest(new
                    {
                        mensagem = $"O identificador {serieId} é inválido"
                    });
                }

                else
                {
                    int id = int.Parse(serieId);
                    Serie serie = await _contexto.Series.FindAsync(id);

                    if(serie != null)
                    {
                        return serie;
                    }

                    return NotFound(new
                    {
                        mensagem = $"A serie com identificador {id} não foi encontrada ou nunca existiu"
                    });
                }
            }
        }

        /// <summary>
        /// Atualiza a serie recebida
        /// </summary>
        /// <param name="serie">Serie a ser atualizada</param>
        /// <remarks>
        /// Exemplo de requisição
        /// PUT /Series
        /// {
        ///     "serieId" : 90,
        ///     "Titulo" : "Star Trek",
        ///     "Sinopse" : "Tudo começou quando uma estrela super massiva explodiu...",
        ///     "Temporada" : 4",
        ///     "NumeroEpisodios" : 25",
        ///     "Elenco" : Maria Albergue, Shako Takamoto, Suzana Viera",
        ///     "Categoria" : Luta, Romance, Guerra"
        /// }
        /// </remarks>
        /// <returns> Serie atualizada</returns>
        /// <response code="200"> Serie atualizada corretamente</response>
        /// <response code="400"> Erro ao atualizar a serie recebida</response>        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Serie>> AtualizarAsync(Serie serie)
        {
            if (ModelState.IsValid)
            {
                _contexto.Series.Update(serie);
                await _contexto.SaveChangesAsync();

                return Ok(new
                {
                    mensagem = $"A serie {serie.Titulo} {serie.Temporada} Temporada foi atualizada corretamente"
                });
            }

            return BadRequest(new
            {
                mensagem = $"Houve algum erro ao tentar atualizar a serie {serie.Titulo}, {serie.Temporada}ª temporada"
            });
        }

        /// <summary>
        /// Exclui uma serie baseado em seu id
        /// </summary>
        /// <param name="serieId">Id usado para pegar a serie</param>
        /// <remarks>
        /// Exemplo de requisição
        /// DELETE Series/90
        /// {
        ///     "serieId" : 90    
        /// }
        /// </remarks>
        /// <returns> Serie excluída corretamente</returns>
        /// <response code="200"> Serie excluída</response>
        /// <response code="400"> Identificador inválido</response>
        /// <response code="404"> Serie não encontrada</response>
        [HttpDelete("{serieId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Serie>> ExcluirAsync(string serieId)
        {
            if (string.IsNullOrEmpty(serieId))
            {
                return BadRequest(new
                {
                    mensagem = $"O identificador é inválido"
                });
            }

            else
            {
                if (!int.TryParse(serieId, out int value))
                {
                    return BadRequest(new
                    {
                        mensagem = $"O identificador {serieId} é inválido"
                    });
                }

                else
                {
                    int id = int.Parse(serieId);
                    Serie serie = await _contexto.Series.FindAsync(id);

                    if (serie != null)
                    {
                        _contexto.Series.Remove(serie);
                        await _contexto.SaveChangesAsync();

                        return Ok(new
                        {
                            mensagem = $"A serie {serie.Titulo}, {serie.Temporada}ª temporada foi excluída corretamente"
                        });
                    }

                    return NotFound(new
                    {
                        mensagem = $"A serie com o identificador {id} não foi encontrada ou nunca existiu"
                    });
                }
            }
        }


    }
}
