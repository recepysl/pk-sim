using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Context;
using SimApi.Schema;
using System.Collections.Generic;

namespace SimApi.Service;

[Route("simapi/v1/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private SimDbContext context;
    private readonly IMapper mapper;

    public CustomerController(SimDbContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }

    [HttpGet]
    public ApiResponse<List<CustomerResponse>> GetAll()
    {
        var list = context.Set<Customer>()
            .Where(x => x.IsValid)
            .Include(x => x.Accounts).ThenInclude(x => x.Transactions)
            .ToList();
        var mapped = mapper.Map<List<Customer>, List<CustomerResponse>>(list);
        return new ApiResponse<List<CustomerResponse>>(mapped);
    }

    [HttpGet("{id}")]
    public ApiResponse<CustomerResponse> GetById(int id)
    {
        var entity = context.Set<Customer>().Where(x => x.Id == id)
             .Include(x => x.Accounts).ThenInclude(x => x.Transactions)
             .FirstOrDefault();
        var mapped = mapper.Map<Customer, CustomerResponse>(entity);
        return new ApiResponse<CustomerResponse>(mapped);
    }

    [HttpPost]
    public ApiResponse Post([FromBody] CustomerRequest request)
    {
        var mapped = mapper.Map<CustomerRequest, Customer>(request);
        var entity = context.Set<Customer>().Add(mapped);
        context.SaveChanges();
        return new ApiResponse();
    }

    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] CustomerRequest request)
    {
        var mapped = mapper.Map<CustomerRequest, Customer>(request);
        mapped.Id = id;
        var entity = context.Set<Customer>().Update(mapped);
        context.SaveChanges();
        return new ApiResponse();
    }

    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        var entity = context.Set<Customer>().Find(id);
        context.Set<Customer>().Remove(entity);
        context.SaveChanges();
        return new ApiResponse();
    }
}
