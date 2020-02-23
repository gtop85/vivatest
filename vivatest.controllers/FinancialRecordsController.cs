using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vivatest.models;
using vivatest.services;

namespace vivatest.controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FinancialRecordsController : ControllerBase
    {
        readonly IFinancialRecordsService Service;
        public FinancialRecordsController(IFinancialRecordsService service)
        {
            Service = service;
        }

        /// <summary>
        /// Create a single or a series of financial records
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///         "records":[ 
        ///           { 
        ///             "product":"Paseo",
        ///             "country":"Mexico",
        ///             "segment":"Government",
        ///             "discountBand":"High",
        ///             "unitsSold":2000,
        ///             "manufacturingPrice":23,
        ///             "salePrice":34,
        ///             "grossSales":200000,
        ///             "discounts":26000,
        ///             "sales":122200,
        ///             "salesInEuro":134444,
        ///             "profit":23400,
        ///             "costOfGoodsSold":67800,
        ///             "date":"2012-04-21T18:25:43-05:00"
        ///             }
        ///         ]
        ///      }
        ///
        /// </remarks>
        /// <param name="request"></param>
        /// <response code="200">Returns the inserted, the number of successful and failed records </response>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CreateResponse))]
        public async Task<ActionResult<CreateResponse>> CreateRecordsAsync([FromBody] CreateRequest request)
        {
            var result = await Service.CreateRecordsAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// Search for financial records
        /// </summary>
        /// <remarks>
        /// Search for a single or a series of records by id, product, country, segment and discount code
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <param name="segment"></param>
        /// <param name="country"></param>
        /// <param name="discountBand"></param>
        /// <response code="200">Returns the requested records</response>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CreatedRecord>))]
        public async Task<ActionResult<List<CreatedRecord>>> SearchRecordsAsync([FromQuery] Guid? id, string product, string segment, string country, string discountBand)
        {
            ProductDMO searchQuery = new ProductDMO
            {
                Id = id,
                Product = product,
                Segment = segment,
                Country = country,
                DiscountBand = discountBand
            };
            return Ok(await Service.SearchRecordsAsync(searchQuery));
        }

        /// <summary>
        /// Update a specific financial record
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///             "id": "F9FA66A5-3063-4E4F-B36C-AEFAD2BAF0DF"
        ///             "product":"Paseo",
        ///             "country":"Mexico",
        ///             "segment":"Government",
        ///             "discountBand":"High",
        ///             "unitsSold":2000,
        ///             "manufacturingPrice":23,
        ///             "salePrice":34,
        ///             "grossSales":200000,
        ///             "discounts":26000,
        ///             "sales":122200,
        ///             "salesInEuro":134444,
        ///             "profit":23400,
        ///             "costOfGoodsSold":67800,
        ///             "date":"2012-04-21T18:25:43-05:00"
        ///      }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> UpdateRecordAsync(Guid id, [FromBody] Record record)
        {
            var result = await Service.UpdateRecordAsync(id, record);

            if (result)
                return Ok();

            return BadRequest();
        }

        /// <summary>
        /// Delete a specific financial record
        /// </summary>
        /// <remarks>
        /// Delete a record by a given id
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="204">No Content</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteRecordAsync(Guid id)
        {
            var result = await Service.DeleteRecordAsync(id);

            if (result)
                return NoContent();

            return NotFound();
        }

        /// <summary>
        /// Get reports for a requested time period
        /// </summary>
        /// <remarks>
        /// Get reports for the records found in the asked time frame
        /// </remarks>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <response code="200">Returns a list of reports</response>
        [HttpGet("report")]
        [ProducesResponseType(200, Type = typeof(List<Report>))]
        public async Task<ActionResult<List<Report>>> GetReportAsync(DateTime dateFrom, DateTime dateTo)
        {
            return Ok(await Service.GetReportAsync(dateFrom, dateTo));
        }
    }
}