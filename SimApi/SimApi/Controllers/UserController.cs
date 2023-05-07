using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimApi.Data.Domain;
using SimApi.Data.Repository;
using SimApi.Schema;

namespace SimApi.Service.Controllers;

[Route("simapi/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository repository;
    private IMapper mapper;
    public UserController(IMapper mapper, IUserRepository repository)
    {
        this.repository = repository;
        this.mapper = mapper;
    }


    [HttpGet]
    public List<UserResponse> GetAll()
    {
        var list = repository.GetAll();
        var mapped = mapper.Map<List<UserResponse>>(list);
        return mapped;
    }

    [HttpGet("{id}")]
    public UserResponse GetById(int id)
    {
        var row = repository.GetById(id);
        var mapped = mapper.Map<UserResponse>(row);
        return mapped;
    }

    [HttpPost]
    public void Post([FromBody] UserRequest request)
    {
        var entity = mapper.Map<User>(request);
        repository.Insert(entity);
        repository.Complete();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] UserResponse request)
    {
        request.Id = id;
        var entity = mapper.Map<User>(request);
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
