using System.ComponentModel.DataAnnotations;
using InvoicesBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesBackend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("invoice")]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public class InvoiceController : ControllerBase
    {
        public InvoiceController()
        {

        }

        /// <summary>
        /// Retrieve All Invoices in a Date Range
        /// </summary>
        /// <param name="startDate">Start of Date Range</param>
        /// <param name="endDate">End of Date Range</param>
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<InvoiceDTO>))]
        public ActionResult<List<InvoiceDTO>> List(
            [FromQuery] DateTimeOffset? startDate = null, 
            [FromQuery] DateTimeOffset? endDate = null)
        {
            return Ok();
        }

        /// <summary>
        /// Create a New Invoice
        /// </summary>
        /// <param name="invoiceForCreationDTO">Object containing necessary properties for Invoice Creation</param>
        /// <returns></returns>
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InvoiceDTO))]
        public ActionResult<InvoiceDTO> Create([FromBody][Required] InvoiceForCreationDTO invoiceForCreationDTO)
        {
            return Created();
        }

        [HttpGet("remove/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Remove(int id)
        {
            return NoContent();
        }
    }
}