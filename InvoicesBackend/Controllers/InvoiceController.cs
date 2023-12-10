using InvoicesBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesBackend.Controllers
{
    [Route("invoice")]
    public class InvoiceController : ControllerBase
    {
        public InvoiceController()
        {

        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult List(DateOnly? startDate = null, DateOnly? endDate = null)
        {
            return Ok();
        }

        [HttpGet("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(InvoiceForCreationDTO invoiceForCreationDTO)
        {
            return Created();
        }

        [HttpGet("remove")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Remove(int id)
        {
            return NoContent();
        }
    }
}