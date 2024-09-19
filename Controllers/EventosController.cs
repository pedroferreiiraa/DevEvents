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
        
            // GET api/eventos
            [HttpGet]
            public IActionResult GetAll()
            {
                var eventos = _contexto.Eventos;
                return Ok(eventos);
            }

            // GET api/eventos/1
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
            
            // POST api/eventos
            [HttpPost]
            public IActionResult Post(Evento evento)
            {
                _contexto.Eventos.Add(evento);
                _contexto.SaveChanges();
                return CreatedAtAction(nameof(GetById), 
                new { id = evento.Id }, evento    
                    );
            }
            
            // PUT api/eventos/1
            [HttpPut("{id}")]
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
            
            // DELETE api/eventos/1
            [HttpDelete("{id}")]
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