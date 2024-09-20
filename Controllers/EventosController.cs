    using DevEvents.API.Entities;
    using DevEvents.API.Persistence;
    using Microsoft.AspNetCore.Mvc;
    
    
    namespace DevEvents.API.Controllers;
    [ApiController]
    [Route("/api/eventos")]
    public class EventosController : ControllerBase
    {
            private readonly EventosDbContext _contexto;
        
            //Injeção de Dependências
            public EventosController(EventosDbContext contexto)
            {
                _contexto = contexto;
            }    
        
            /// <summary>
            /// Listagem de eventos
            /// </summary>
            /// <param name="id"></param>
            /// <response code="200">Sucesso</response>
            /// <returns>Lista de eventos</returns>
            [HttpGet]
            public IActionResult GetAll()
            {
                var eventos = _contexto.Eventos;
                return Ok(eventos);
            }

            /// <summary>
            /// Detalhes de Evento
            /// </summary>
            /// <param name="id">Identificador do Evento</param>
            /// <response code="404">Não encontrado </response>
            /// <response code="200">Sucesso </response>
            /// <returns>Detalhes de Evento</returns>
            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                var evento = _contexto.Eventos.SingleOrDefault(e => e.Id == id);
                if (evento == null)
                {
                    return NotFound();
                }
                return Ok(evento);
            }
            
            /// <summary>
            /// Cadastro de Evento
            /// </summary>
            /// <remarks>
            ///  {
            ///     "titulo": "Jornada .NET",
            ///     "descricao": "Evento para destravar carreiras e acelerar resultados",
            ///     "dataInicio": "2024-09-20T13:35:55.751Z",
            ///     "dataFim": "2024-09-24T13:35:55.751Z",
            ///     "organizador": "Luis Dev"
            ///  }
            /// </remarks>
            /// <param name="evento">Dados do Evento</param>
            /// <returns>Evento recém criado</returns>
            /// <response code="201">Sucesso</response>
            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            
            public IActionResult Post(Evento evento)
            {
                _contexto.Eventos.Add(evento);
                _contexto.SaveChanges();
                return CreatedAtAction(nameof(GetById), 
                new { id = evento.Id }, evento    
                    );
            }
            
            /// <summary>
            /// Atualização de Evento
            /// </summary>
            /// <remarks>
            ///  {
            ///     "id": 1,
            ///     "titulo": "Jornada .NET",
            ///     "descricao": "Evento para destravar carreiras e acelerar resultados",
            ///     "dataInicio": "2024-09-20T13:35:55.751Z",
            ///     "dataFim": "2024-09-24T13:35:55.751Z",
            ///     "organizador": "Luis Dev"
            ///  }
            /// </remarks>
            /// <param name="id">Identificador do Evento></param>
            /// <param name="evento">Dados do Evento</param>
            /// <returns>Evento recém criado</returns>
            /// <response code="204">Sucesso</response>
            /// <response code="404">Não encontrado</response>
            [HttpPut("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public IActionResult Put(int id, Evento evento)
            {
                var eventoExistente = _contexto.Eventos.SingleOrDefault(e => e.Id == id);
                
                if (eventoExistente == null)
                {
                    return NotFound();
                }
                
                eventoExistente.Update(evento.Titulo, evento.Descricao, evento.DataInicio, evento.DataFim);
                
                _contexto.Eventos.Update(eventoExistente);
                _contexto.SaveChanges();
                return NoContent();
            }
            
            /// <summary>
            /// Remoção de Evento
            /// </summary>
            /// <param name="id">Identificador do Evento</param>
            /// <response code="404">Não encontrado </response>
            /// <response code="200">Sucesso </response>
            /// <returns></returns>
            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]

            public IActionResult Delete(int id)
            {
                var evento = _contexto.Eventos.SingleOrDefault(e => e.Id == id);
                
                if (evento == null)
                {
                    return NotFound();
                }
                
                _contexto.Eventos.Remove(evento);
                _contexto.SaveChanges();
                
                return NoContent();
            }
    }