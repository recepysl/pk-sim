using AutoMapper;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation;

public class CurrencyService : BaseService<Currency, CurrencyRequest, CurrencyResponse>, ICurrencyService
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

}
