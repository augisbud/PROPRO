using System.ComponentModel.DataAnnotations;
using AutoMapper;
using InvoicesBackend.Entities;
using InvoicesBackend.Models;
using InvoicesBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesBackend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("invoice")]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public class InvoiceController(IMapper mapper, InvoicesRepository invoicesRepository) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly InvoicesRepository _invoicesRepository = invoicesRepository;

        /// <summary>
        /// Retrieve an Invoice by ID
        /// </summary>
        /// <param name="id">The ID of the Invoice to retrieve</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}", Name = "GetInvoice")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvoiceDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<InvoiceDTO> Get(int id)
        {
            var invoice = _invoicesRepository.GetInvoiceById(id);
            if (invoice == null)
                return NotFound();

            var invoiceDto = _mapper.Map<InvoiceDTO>(invoice);
            
            return Ok(invoiceDto);
        }

        /// <summary>
        /// Create a New Invoice
        /// </summary>
        /// <param name="invoiceForCreationDTO">Object containing necessary properties for Invoice Creation</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InvoiceDTO))]
        public ActionResult<InvoiceDTO> Create([FromBody][Required] InvoiceForCreationDTO invoiceForCreationDTO)
        {
            var invoice = _mapper.Map<Invoice>(invoiceForCreationDTO);

            var createdInvoice = _invoicesRepository.AddInvoice(invoice);
            var invoiceDto = _mapper.Map<InvoiceDTO>(createdInvoice);

            return CreatedAtRoute(
                "GetInvoice",
                new { id = invoiceDto.Id }, 
                invoiceDto
            );
        }

        /// <summary>
        /// Remove an Invoice
        /// </summary>
        /// <param name="id">ID of an Invoice to Remove</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("remove/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Remove(int id)
        {
            var invoice = _invoicesRepository.GetInvoiceById(id);
            if (invoice == null)
                return NotFound();

            _invoicesRepository.RemoveInvoice(invoice);

            return NoContent();
        }

        /// <summary>
        /// Retrieve All Invoices in a Date Range
        /// </summary>
        /// <param name="startDate">Start of Date Range</param>
        /// <param name="endDate">End of Date Range</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<InvoiceDTO>))]
        public ActionResult<List<InvoiceDTO>> List(
            [FromQuery] DateTimeOffset? startDate = null, 
            [FromQuery] DateTimeOffset? endDate = null)
        {
            var invoices = _invoicesRepository.RetrieveInvoices(startDate, endDate);

            return Ok(_mapper.Map<IEnumerable<InvoiceDTO>>(invoices));
        }
    }
}