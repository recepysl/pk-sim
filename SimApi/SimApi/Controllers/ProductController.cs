using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Repository;
using SimApi.Schema;

namespace SimApi.Service.Controllers;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository repository;
    private IMapper mapper;
    public ProductController(IMapper mapper, IProductRepository repository)
    {
        this.repository = repository;
        this.mapper = mapper;
    }


    [HttpGet]
    public List<ProductResponse> GetAll()
    {
        var list = repository.GetAll();
        var mapped = mapper.Map<List<ProductResponse>>(list);
        return mapped;
    }

    [HttpGet("{id}")]
    public ProductResponse GetById(int id)
    {
        var row = repository.GetById(id);
        var mapped = mapper.Map<ProductResponse>(row);
        return mapped;
    }

    [HttpPost]
    public void Post([FromBody] ProductRequest request)
    {
        var entity = mapper.Map<Product>(request);
        repository.Insert(entity);
        repository.Complete();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] ProductResponse request)
    {
        request.Id = id;
        var entity = mapper.Map<Product>(request);
        repository.Update(entity);
        repository.Complete();
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        repository.DeleteById(id);
        repository.Complete();
    }

}
