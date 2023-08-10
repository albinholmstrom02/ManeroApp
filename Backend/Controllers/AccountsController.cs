using Backend.Contexts;
using Backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _context;
        public AccountsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var accounts = _context.Accounts.ToList();
            if (accounts.Count == 0)
            {
                return NotFound("Accounts not available");
            }
            return Ok(accounts);
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account == null)
            {
                return NotFound($"Account details not found with id {id}");
            }
            return Ok(account);
        }

        [HttpPost]
        public IActionResult SignUp(AccountEntity account)
        {
            _context.Add(account);
            _context.SaveChanges();
            return Ok("Account created");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var accountEntity = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Email == model.Email && a.Password == model.Password);
            
            if (accountEntity != null)
            {
                return Ok(accountEntity.Id);
            }
            else
            {
                return Unauthorized();
            }
        }
        
        [HttpPut]
        public IActionResult Edit(AccountEntity account)
        {
            if (account == null || account.Id == 0)
            {
                if (account == null)
                {
                    return BadRequest("Account data is invalid.");
                }
                else if (account.Id == 0)
                {
                    return BadRequest($"Account Id {account.Id} is invalid");
                }
            }
            var model = _context.Accounts.Find(account.Id);
            if (model == null)
            {
                return BadRequest($"Account Id {account.Id} is invalid");
            }

            model.Id = account.Id;
            model.Name = account.Name;
            model.Email = account.Email;
            model.Password = account.Password;
            _context.SaveChanges();
            return Ok("Account details updated");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var account = _context.Accounts.Find(id);
            if (account == null)
            {
                return NotFound($"Account not found with id {id}");
            }

            _context.Accounts.Remove(account);
            _context.SaveChanges();
            return Ok("Account details deleted.");
        }
    }
}
