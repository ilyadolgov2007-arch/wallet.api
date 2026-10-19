using Microsoft.AspNetCore.Mvc;
using Wallet.Core;
using Wallet.Api.Requests;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private static readonly List<Account> _accounts = new();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_accounts);
        }

        [HttpPost]
        public IActionResult Create(CreateAccountRequest request)
        {
            Account account = new(request.Name);
            _accounts.Add(account);
            return Ok(account);
        }
    }
}
