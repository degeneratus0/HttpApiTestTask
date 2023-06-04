using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.DTOs;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _repository;
        private readonly IConverter<User, UserDTO> _converter;

        public UserController(IRepository<User> repository, IConverter<User, UserDTO> converter)
        {
            _repository = repository;
            _converter = converter;

        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<UserDTO> Get()
        {
            return _repository.ReadAll().Select(_converter.ToDTO);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(string id)
        {
            User? user = _repository.Read(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_converter.ToDTO(user));
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] UserDTO userDTO)
        {
            User user = _converter.FromDTO(userDTO);
            _repository.Add(user);
            return Ok(userDTO);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] UserDTO userDTO)
        {
            userDTO.IdNumber = id;
            if (_repository.Read(id) == null)
            {
                return NotFound();
            }
            _repository.Edit(id, _converter.FromDTO(userDTO));
            return Ok(userDTO);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            if (_repository.Read(id) == null)
            {
                return NotFound();
            }
            _repository.Delete(id);
            return NoContent();
        }
    }
}
