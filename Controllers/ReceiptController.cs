using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceiptController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public ReceiptController(CompanyDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allReceipts")]
    public ActionResult<IEnumerable<Receipt>> GetReceipts() {

        var receipts = _dbContext.Receipts.Include(_ => _.Suppliers).ToList();

        return Ok(receipts);
    }

    [HttpGet("getReceipt/{id}")]
    public ActionResult GetReceiptById(int id) {

        var receipt = _dbContext.Receipts.Include(_ => _.Suppliers).FirstOrDefault(_ => _.ReceiptId == id);

        if (receipt == null)
        {
            return NotFound();
        }

        return Ok(receipt);
    }

    [HttpPost("addReceipt")]
    public ActionResult AddReceipt(ReceiptDto payloadReceipt) {

        var newReceipt = _mapper.Map<Receipt>(payloadReceipt);

        _dbContext.Receipts.Add(newReceipt);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetReceiptById), new { id = newReceipt.ReceiptId }, newReceipt);
    }

    [HttpPut("updateReceipt/{id}")]
    public ActionResult UpdateReceipt(int id, ReceiptDto payloadReceipt) {

        var existingReceipt = _dbContext.Receipts.Find(id);
        if (existingReceipt == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadReceipt, existingReceipt);

        _dbContext.Receipts.Update(existingReceipt);
        _dbContext.SaveChanges();

        return Ok(existingReceipt);
    }

    [HttpDelete("deleteReceipt/{id}")]
    public ActionResult DeleteReceipt(int id)
    {
        var receipt = _dbContext.Receipts.Find(id);
        if (receipt == null)
        {
            return NotFound();
        }

        _dbContext.Receipts.Remove(receipt);
        _dbContext.SaveChanges();

        return NoContent();
    }
}