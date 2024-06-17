using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class SlittingDetailController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public SlittingDetailController(CompanyDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allSlittingDetails")]
    public ActionResult<IEnumerable<SlittingDetail>> GetSlittingDetails() {

        var slittingDetails = _dbContext.SlittingDetails.Include(_ => _.ProductionSlittings).ToList();

        return Ok(slittingDetails);
    }

    [HttpGet("getSlittingDetail/{id}")]
    public ActionResult GetSlittingDetailById(int id) {

        var slittingDetail = _dbContext.SlittingDetails.Include(_ => _.ProductionSlittings).FirstOrDefault(_ => _.SlittingDetailId == id);

        if (slittingDetail == null)
        {
            return NotFound();
        }

        return Ok(slittingDetail);
    }

    [HttpPost("addSlittingDetail")]
    public ActionResult AddSlittingDetail(SlittingDetailDto payloadSlittingDetail) {

        var newSlittingDetail = _mapper.Map<SlittingDetail>(payloadSlittingDetail);

        _dbContext.SlittingDetails.Add(newSlittingDetail);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetSlittingDetailById), new { id = newSlittingDetail.SlittingDetailId }, newSlittingDetail);
    }

    [HttpPut("updateSlittingDetail/{id}")]
    public ActionResult UpdateSlittingDetail(int id, SlittingDetailDto payloadSlittingDetail) {

        var existingSlittingDetail = _dbContext.SlittingDetails.Find(id);
        if (existingSlittingDetail == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadSlittingDetail, existingSlittingDetail);

        _dbContext.SlittingDetails.Update(existingSlittingDetail);
        _dbContext.SaveChanges();

        return Ok(existingSlittingDetail);
    }

    [HttpDelete("deleteSlittingDetail/{id}")]
    public ActionResult DeleteSlittingDetail(int id)
    {
        var slittingDetail = _dbContext.SlittingDetails.Find(id);
        if (slittingDetail == null)
        {
            return NotFound();
        }

        _dbContext.SlittingDetails.Remove(slittingDetail);
        _dbContext.SaveChanges();

        return NoContent();
    }
}