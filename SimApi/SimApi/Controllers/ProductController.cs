using Microsoft.AspNetCore.Mvc;
using SimApi.Data.Context;
using SimApi.Data.Domain;
using SimApi.Data.Uow;

namespace SimApi.Service.Controllers;

[Route("simapi/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    public ProductController(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }


    [HttpGet]
    public List<Product> GetAll()
    {
        var list = unitOfWork.GetRepository<Product>().GetAll();
        return list;
    }

    [HttpGet("{id}")]
    public Product GetById(int id)
    {
        var row = unitOfWork.GetRepository<Product>().GetById(id);
        return row;
    }

    [HttpPost]
    public void Post([FromBody] Product request)
    {
        unitOfWork.GetRepository<Product>().Insert(request);
        unitOfWork.Complete();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Product request)
    {
        request.Id = id;
        unitOfWork.GetRepository<Product>().Update(request);
        unitOfWork.Complete();
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        unitOfWork.GetRepository<Product>().DeleteById(id);
        unitOfWork.Complete();
    }

}
