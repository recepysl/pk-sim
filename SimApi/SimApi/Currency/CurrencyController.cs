using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Service;


[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService service;

    public CurrencyController(ICurrencyService service)
    {
        this.service = service;
    }

    [HttpGet]
    public ApiResponse<List<CurrencyResponse>> GetAll()
    {
        var list = service.GetAll();
        return list;
    }

    [HttpGet("{id}")]
    public ApiResponse<CurrencyResponse> GetById(int id)
    {
        var model = service.GetById(id);
        return model;
    }

    [HttpPost]
    public ApiResponse Post([FromBody] CurrencyRequest request)
    {
        return service.Insert(request);
    }

    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] CurrencyRequest request)
    {
        return service.Update(id, request);
    }

    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        return service.Delete(id);
    }
}
