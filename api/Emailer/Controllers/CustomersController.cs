using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Emailer.Controllers
{
    [ApiVersion("1.0")]
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
        
        [HttpGet(Name = "GetCustomers")]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _repository.GetAllAsync();
        }

        [HttpPost(Name = "AddCustomer")]
        public async Task<Customer> Add([FromBody] Customer customer)
        {
            await _repository.AddAsync(customer);
            return customer;
        }

        [HttpPut(Name = "UpdateCustomer")]
        public async Task<Customer> Update([FromBody] Customer customer)
        {
            await _repository.UpdateAsync(customer);
            return customer;
        }

        [HttpDelete(Name = "DeleteCustomer")]
        public async Task Delete([FromBody] Customer customer)
        {
            await _repository.DeleteAsync(customer);
        }
    }
}