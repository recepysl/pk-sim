using Microsoft.AspNetCore.Mvc;
using SimApi.Data.Context;
using SimApi.Data.Domain;

namespace SimApi.Service.Controllers;

[Route("simapi/v1/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private SimDbContext context;
    public CategoryController(SimDbContext context)
    {
        this.context = context;
    }


    [HttpGet]
    public List<Category> GetAll()
    {
        var list = context.Set<Category>().ToList();
        return list;
    }

    [HttpGet("{id}")]
    public Category GetById(int id)
    {
        var row = context.Set<Category>().Where(x => x.Id == id).FirstOrDefault();
        return row;
    }

    [HttpPost]
    public void Post([FromBody] Category request)
    {
        context.Set<Category>().Add(request);
        context.SaveChanges();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Category request)
    {
        request.Id = id;
        context.Set<Category>().Update(request);
        context.SaveChanges();
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var row = context.Set<Category>().Where(x => x.Id == id).FirstOrDefault();
        context.Set<Category>().Remove(row);
        context.SaveChanges();
    }

}
