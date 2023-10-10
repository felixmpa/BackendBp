using Bp.Client.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Bp.Common;

namespace Bp.Client.Service.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> customersRepository;

        public CustomersController(IRepository<Customer> customersRepository)
        {
            this.customersRepository = customersRepository;
        }

        // GET customers/
        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAsync()
        {
            var customers = (await customersRepository
                                    .GetAllAsync(c => c.Person))
                                    .Select(customer => customer);
            return customers;
        }

        // GET customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetByIdAsync(int id)
        {
            var customer = await customersRepository.GetAsync(id, c => c.Person);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }


        // POST /customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostAsync(Customer newCustomer)
        {
            await customersRepository.CreateAsync(newCustomer);

            var createdCustomer = await customersRepository.GetAsync(newCustomer.Id);

            if (createdCustomer != null)
            {
                return StatusCode(201, createdCustomer);
            }

            return StatusCode(500, "Internal server error: Failed to create customer");
        }


        // PUT /customers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> PutAsync(int id, Customer updatedCustomer)
        {
            var existingCustomer = await customersRepository.GetAsync(id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.Status = updatedCustomer.Status;
            existingCustomer.Password = updatedCustomer.Password;
            existingCustomer.Person = new Person
            {
                Id = existingCustomer.PersonId,
                Identification = updatedCustomer.Person.Identification,
                Name = updatedCustomer.Person.Name,
                Sex = updatedCustomer.Person.Sex,
                Age = updatedCustomer.Person.Age,
                Address = updatedCustomer.Person.Address,
                Phone = updatedCustomer.Person.Phone,
            };

            await customersRepository.UpdateAsync(existingCustomer);

            return Ok();
        }

        // DELETE /customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var customer = await customersRepository.GetAsync(id, c => c.Person);

            if (customer == null)
            {
                return NotFound();
            }

            await customersRepository.RemoveAsync(customer.Id);

            return Ok();
        }


    }
}
