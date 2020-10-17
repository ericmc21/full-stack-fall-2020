using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Emailer.Controllers
{
    [ApiController]
    [Route( "[controller]")]
    public class EmailRecipientsController : Controller
    {
        private readonly IRepository<EmailRecipient> _repository;
        private readonly ILogger<EmailRecipientsController> _logger;

        public EmailRecipientsController(IRepository<EmailRecipient> repository
            , ILogger<EmailRecipientsController> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<EmailRecipient>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpPost]
        public async Task<EmailRecipient> Add([FromBody] EmailRecipient emailRecipient)
        {
            await _repository.AddAsync(emailRecipient);
            return emailRecipient;
        }

        [HttpPut]
        public async Task<EmailRecipient> Update([FromBody] EmailRecipient emailRecipient)
        {
            await _repository.UpdateAsync(emailRecipient);
            return emailRecipient;
        }

        [HttpDelete]
        public async Task Delete([FromBody] EmailRecipient emailRecipient)
        {
            await _repository.DeleteAsync(emailRecipient);
        }
    }
}