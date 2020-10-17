using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Emailer.Controllers
{
    [ApiController]
    [Route( "[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> _repository;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(IRepository<Customer> repository, ILogger<CustomersController> logger)
        {
            _logger = logger;
            _repository = repository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpPost]
        public async Task<Customer> Add([FromBody] Customer customer)
        {
            await _repository.AddAsync(customer);
            return customer;
        }

        [HttpPut]
        public async Task<Customer> Update([FromBody] Customer customer)
        {
            await _repository.UpdateAsync(customer);
            return customer;
        }

        [HttpDelete]
        public async Task Delete([FromBody] Customer customer)
        {
            await _repository.DeleteAsync(customer);
        }
    }
}