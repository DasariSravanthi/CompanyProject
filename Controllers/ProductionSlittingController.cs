using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductionSlittingController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductionSlittingController(CompanyDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductionSlittings")]
    public ActionResult<IEnumerable<ProductionSlitting>> GetProductionSlittings() {

        var productionSlittings = _dbContext.ProductionSlittings.Include(_ => _.ProductionCalendarings).ToList();

        return Ok(productionSlittings);
    }

    [HttpGet("getProductionSlitting/{id}")]
    public ActionResult GetProductionSlittingById(int id) {

        var productionSlitting = _dbContext.ProductionSlittings.Include(_ => _.ProductionCalendarings).FirstOrDefault(_ => _.ProductionSlittingId == id);

        if (productionSlitting == null)
        {
            return NotFound();
        }

        return Ok(productionSlitting);
    }

    [HttpPost("addProductionSlitting")]
    public ActionResult AddProductionSlitting(ProductionSlittingDto payloadProductionSlitting) {

        var newProductionSlitting = _mapper.Map<ProductionSlitting>(payloadProductionSlitting);

        _dbContext.ProductionSlittings.Add(newProductionSlitting);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductionSlittingById), new { id = newProductionSlitting.ProductionSlittingId }, newProductionSlitting);
    }

    [HttpPut("updateProductionSlitting/{id}")]
    public ActionResult UpdateProductionSlitting(int id, ProductionSlittingDto payloadProductionSlitting) {

        var existingProductionSlitting = _dbContext.ProductionSlittings.Find(id);
        if (existingProductionSlitting == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadProductionSlitting, existingProductionSlitting);

        _dbContext.ProductionSlittings.Update(existingProductionSlitting);
        _dbContext.SaveChanges();

        return Ok(existingProductionSlitting);
    }

    [HttpDelete("deleteProductionSlitting/{id}")]
    public ActionResult DeleteProductionSlitting(int id)
    {
        var productionSlitting = _dbContext.ProductionSlittings.Find(id);
        if (productionSlitting == null)
        {
            return NotFound();
        }

        _dbContext.ProductionSlittings.Remove(productionSlitting);
        _dbContext.SaveChanges();

        return NoContent();
    }
}