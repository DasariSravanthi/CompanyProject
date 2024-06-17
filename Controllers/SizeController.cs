using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SizeController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public SizeController(CompanyDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    [HttpGet("allSizes")]
    public ActionResult<IEnumerable<Size>> GetSizes() {

        var sizes = _dbContext.Sizes.ToList();

        return Ok(sizes);
    }

    [HttpGet("getSize/{id}")]
    public ActionResult GetSizeById(byte id) {

        var size = _dbContext.Sizes.Find(id);

        if (size == null)
        {
            return NotFound();
        }

        return Ok(size);
    }

    [HttpPost("addSize")]
    public ActionResult AddSize(SizeDto payloadSize) {

        var newSize = _mapper.Map<Size>(payloadSize);

        _dbContext.Sizes.Add(newSize);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetSizeById), new { id = newSize.SizeId }, newSize);
    }

    [HttpPut("updateSize/{id}")]
    public ActionResult UpdateSize(byte id, SizeDto payloadSize) {

        var existingSize = _dbContext.Sizes.Find(id);
        if (existingSize == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadSize, existingSize);

        _dbContext.Sizes.Update(existingSize);
        _dbContext.SaveChanges();

        return Ok(existingSize);
    }

    [HttpDelete("deleteSize/{id}")]
    public ActionResult DeleteSize(byte id)
    {
        var size = _dbContext.Sizes.Find(id);
        if (size == null)
        {
            return NotFound();
        }

        _dbContext.Sizes.Remove(size);
        _dbContext.SaveChanges();

        return NoContent();
    }
}