using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Data;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Identity.Controllers;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/identity/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserIdentityService service;
    private IMapper mapper;
    public UserController(IMapper mapper, IUserIdentityService service)
    {
        this.service = service;
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
