using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

using CompanyApp.Data;
using CompanyApp.Models.Entity;
using CompanyApp.Models.DTO;

namespace CompanyApp.Controllers;

[ApiController]
[Route("[controller]")]
public class IssueController : ControllerBase {
    
    private readonly CompanyDbContext _dbContext;
    private readonly IMapper _mapper;

    public IssueController(CompanyDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
         _mapper = mapper;
    }

    [HttpGet("allIssues")]
    public ActionResult<IEnumerable<Issue>> GetIssues() {

        var issues = _dbContext.Issues.Include(_ => _.RollNumbers).Include(_ => _.ProductStocks).ToList();

        return Ok(issues);
    }

    [HttpGet("getIssue/{id}")]
    public ActionResult GetIssueById(int id) {

        var issue = _dbContext.Issues.Include(_ => _.RollNumbers).Include(_ => _.ProductStocks).FirstOrDefault(_ => _.IssueId == id);

        if (issue == null)
        {
            return NotFound();
        }

        return Ok(issue);
    }

    [HttpPost("addIssue")]
    public ActionResult AddIssue(IssueDto payloadIssue) {

        var newIssue = _mapper.Map<Issue>(payloadIssue);

        _dbContext.Issues.Add(newIssue);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetIssueById), new { id = newIssue.IssueId }, newIssue);
    }

    [HttpPut("updateIssue/{id}")]
    public ActionResult UpdateIssue(int id, IssueDto payloadIssue) {

        var existingIssue = _dbContext.Issues.Find(id);
        if (existingIssue == null)
        {
            return NotFound();
        }

        _mapper.Map(payloadIssue, existingIssue);

        _dbContext.Issues.Update(existingIssue);
        _dbContext.SaveChanges();

        return Ok(existingIssue);
    }

    [HttpDelete("deleteIssue/{id}")]
    public ActionResult DeleteIssue(int id)
    {
        var issue = _dbContext.Issues.Find(id);
        if (issue == null)
        {
            return NotFound();
        }

        _dbContext.Issues.Remove(issue);
        _dbContext.SaveChanges();

        return NoContent();
    }
}