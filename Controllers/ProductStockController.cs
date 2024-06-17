using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductStockController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductStockController(CompanyDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductStocks")]
    public ActionResult<IEnumerable<ProductStock>> GetProductStocks() {

        var productStocks = _dbContext.ProductStocks.Include(_ => _.ProductDetails).Include(_ => _.Sizes).ToList();

        return Ok(productStocks);
    }

    [HttpGet("getProductStock/{id}")]
    public ActionResult GetProductStockById(Int16 id) {

        var productStock = _dbContext.ProductStocks.Include(_ => _.ProductDetails).Include(_ => _.Sizes).FirstOrDefault(_ => _.ProductStockId == id);

        if (productStock == null)
        {
            return NotFound();
        }

        return Ok(productStock);
    }

    [HttpPost("addProductStock")]
    public ActionResult AddProductStock(ProductStockDto payloadProductStock) {

        var newProductStock = _mapper.Map<ProductStock>(payloadProductStock);

        _dbContext.ProductStocks.Add(newProductStock);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductStockById), new { id = newProductStock.ProductStockId }, newProductStock);
    }

    [HttpPut("updateProductStock/{id}")]
    public ActionResult UpdateProductStock(Int16 id, ProductStockDto payloadProductStock) {

        var existingProductStock = _dbContext.ProductStocks.Find(id);
        if (existingProductStock == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadProductStock, existingProductStock);

        _dbContext.ProductStocks.Update(existingProductStock);
        _dbContext.SaveChanges();

        return Ok(existingProductStock);
    }

    [HttpDelete("deleteProductStock/{id}")]
    public ActionResult DeleteProductStock(Int16 id)
    {
        var productDetail = _dbContext.ProductStocks.Find(id);
        if (productDetail == null)
        {
            return NotFound();
        }

        _dbContext.ProductStocks.Remove(productDetail);
        _dbContext.SaveChanges();

        return NoContent();
    }
}