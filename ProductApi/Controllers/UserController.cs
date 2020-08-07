using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Repository;
using ProductApi.Services;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Auth([FromBody] User model)
        {
            var user = await _repo.GetUser(model.UserName, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);

            return Ok(new { 
                User = user, 
                Token = token
            });
        }

        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.ListUsers());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            if (user != null)
            {

                user.Password = UserService.GeneratePassword(user.Password);
                _repo.Add(user);
                await _repo.SaveChangesAsync();
                return Created($"/api/User/{user.Id}", user);
            }
            else
                return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            _repo.Update(user);
            await _repo.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _repo.GetUser(id);

            if (user == null)
                return NotFound();

            _repo.Delete(user);
            await _repo.SaveChangesAsync();

            return Ok();
        }


        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => string.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";
    }
}
