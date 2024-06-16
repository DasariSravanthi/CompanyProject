using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductDetailController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductDetailController(CompanyDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allProductDetails")]
    public ActionResult<IEnumerable<ProductDetail>> GetProductDetails() {

        var productDetails = _dbContext.ProductDetails.Include(_ => _.Products).ToList();

        return Ok(productDetails);
    }

    [HttpGet("getProductDetail/{id}")]
    public ActionResult GetProductDetailById(byte id) {

        var productDetail = _dbContext.ProductDetails.Include(_ => _.Products).FirstOrDefault(_ => _.ProductDetailId == id);

        if (productDetail == null)
        {
            return NotFound();
        }

        return Ok(productDetail);
    }

    [HttpPost("addProductDetail")]
    public ActionResult AddProductDetail(ProductDetailDto payloadProductDetail) {

        var newProductDetail = _mapper.Map<ProductDetail>(payloadProductDetail);

        _dbContext.ProductDetails.Add(newProductDetail);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetProductDetailById), new { id = newProductDetail.ProductDetailId }, newProductDetail);
    }

    [HttpPut("updateProductDetail/{id}")]
    public ActionResult UpdateProductDetail(byte id, ProductDetailDto payloadProductDetail) {

        var existingProductDetail = _dbContext.ProductDetails.Find(id);
        if (existingProductDetail == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadProductDetail, existingProductDetail);

        _dbContext.ProductDetails.Update(existingProductDetail);
        _dbContext.SaveChanges();

        return Ok(existingProductDetail);
    }

    [HttpDelete("deleteProductDetail/{id}")]
    public ActionResult DeleteProductDetail(byte id)
    {
        var productDetail = _dbContext.ProductDetails.Find(id);
        if (productDetail == null)
        {
            return NotFound();
        }

        _dbContext.ProductDetails.Remove(productDetail);
        _dbContext.SaveChanges();

        return NoContent();
    }
}