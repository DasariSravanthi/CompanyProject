using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductionCalendaringController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductionCalendaringController(CompanyDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductionCalendarings")]
    public ActionResult<IEnumerable<ProductionCalendaring>> GetProductionCalendarings() {

        var productionCalendarings = _dbContext.ProductionCalendarings.Include(_ => _.ProductionCoatings).ToList();

        return Ok(productionCalendarings);
    }

    [HttpGet("getProductionCalendaring/{id}")]
    public ActionResult GetProductionCalendaringById(int id) {

        var productionCalendaring = _dbContext.ProductionCalendarings.Include(_ => _.ProductionCoatings).FirstOrDefault(_ => _.ProductionCalendaringId == id);

        if (productionCalendaring == null)
        {
            return NotFound();
        }

        return Ok(productionCalendaring);
    }

    [HttpPost("addProductionCalendaring")]
    public ActionResult AddProductionCalendaring(ProductionCalendaringDto payloadProductionCalendaring) {

        var newProductionCalendaring = _mapper.Map<ProductionCalendaring>(payloadProductionCalendaring);

        _dbContext.ProductionCalendarings.Add(newProductionCalendaring);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductionCalendaringById), new { id = newProductionCalendaring.ProductionCalendaringId }, newProductionCalendaring);
    }

    [HttpPut("updateProductionCalendaring/{id}")]
    public ActionResult UpdateProductionCalendaring(int id, ProductionCalendaringDto payloadProductionCalendaring) {

        var existingProductionCalendaring = _dbContext.ProductionCalendarings.Find(id);
        if (existingProductionCalendaring == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadProductionCalendaring, existingProductionCalendaring);

        _dbContext.ProductionCalendarings.Update(existingProductionCalendaring);
        _dbContext.SaveChanges();

        return Ok(existingProductionCalendaring);
    }

    [HttpDelete("deleteProductionCalendaring/{id}")]
    public ActionResult DeleteProductionCalendaring(int id)
    {
        var productionCalendaring = _dbContext.ProductionCalendarings.Find(id);
        if (productionCalendaring == null)
        {
            return NotFound();
        }

        _dbContext.ProductionCalendarings.Remove(productionCalendaring);
        _dbContext.SaveChanges();

        return NoContent();
    }
}