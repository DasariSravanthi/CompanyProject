using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class RollNumberController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public RollNumberController(CompanyDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allRollNumbers")]
    public ActionResult<IEnumerable<RollNumber>> GetRollNumbers() {

        var rollNumbers = _dbContext.RollNumbers.Include(_ => _.ReceiptDetails).ToList();

        return Ok(rollNumbers);
    }

    [HttpGet("getRollNumber/{id}")]
    public ActionResult GetRollNumberById(int id) {

        var rollNumber = _dbContext.RollNumbers.Include(_ => _.ReceiptDetails).FirstOrDefault(_ => _.RollNumberId == id);

        if (rollNumber == null)
        {
            return NotFound();
        }

        return Ok(rollNumber);
    }

    [HttpPost("addRollNumber")]
    public ActionResult AddRollNumber(RollNumberDto payloadRollNumber) {

        var newRollNumber = _mapper.Map<RollNumber>(payloadRollNumber);

        _dbContext.RollNumbers.Add(newRollNumber);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetRollNumberById), new { id = newRollNumber.RollNumberId }, newRollNumber);
    }

    [HttpPut("updateRollNumber/{id}")]
    public ActionResult UpdateRollNumber(int id, RollNumberDto payloadRollNumber) {

        var existingRollNumber = _dbContext.RollNumbers.Find(id);
        if (existingRollNumber == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadRollNumber, existingRollNumber);

        _dbContext.RollNumbers.Update(existingRollNumber);
        _dbContext.SaveChanges();

        return Ok(existingRollNumber);
    }

    [HttpDelete("deleteRollNumber/{id}")]
    public ActionResult DeleteRollNumber(int id)
    {
        var rollNumber = _dbContext.RollNumbers.Find(id);
        if (rollNumber == null)
        {
            return NotFound();
        }

        _dbContext.RollNumbers.Remove(rollNumber);
        _dbContext.SaveChanges();

        return NoContent();
    }
}