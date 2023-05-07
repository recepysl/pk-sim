using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimApi.Data.Domain;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Service.Controllers;

[Route("simapi/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;
    private IMapper mapper;
    public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }


    [HttpGet]
    public List<ProductResponse> GetAll()
    {
        var list = unitOfWork.ProductRepository.GetAll();
        var mapped = mapper.Map<List<ProductResponse>>(list);
        return mapped;
    }

    [HttpGet("{id}")]
    public ProductResponse GetById(int id)
    {
        var row = unitOfWork.ProductRepository.GetById(id);
        var mapped = mapper.Map<ProductResponse>(row);
        return mapped;
    }

    [HttpPost]
    public void Post([FromBody] ProductRequest request)
    {
        var entity = mapper.Map<Product>(request);
        unitOfWork.ProductRepository.Insert(entity);
        unitOfWork.Complete();
    }

    [HttpPut("{id}")]
    public void Put(int id, [FromBody] ProductResponse request)
    {
        request.Id = id;
        var entity = mapper.Map<Product>(request);
        unitOfWork.ProductRepository.Update(entity);
        unitOfWork.Complete();
    }


    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        unitOfWork.ProductRepository.DeleteById(id);
        unitOfWork.Complete();
    }

}
