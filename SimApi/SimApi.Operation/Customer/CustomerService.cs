using AutoMapper;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation;

public class CustomerService : BaseService<Customer, CustomerRequest, CustomerResponse>, ICustomerService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }


    public override ApiResponse<List<CustomerResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.Repository<Customer>().GetAllWithInclude("Accounts.Transactions");
            var mapped = mapper.Map<List<Customer>, List<CustomerResponse>>(entityList);
            return new ApiResponse<List<CustomerResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<CustomerResponse>>(ex.Message);
        }
    }

    public override ApiResponse<CustomerResponse> GetById(int id)
    {
        try
        {
            var entity = unitOfWork.Repository<Customer>().GetByIdWithInclude(id, "Accounts.Transactions");
            if (entity is null)
            {
                return new ApiResponse<CustomerResponse>("Record not found");
            }

            var mapped = mapper.Map<Customer, CustomerResponse>(entity);
            return new ApiResponse<CustomerResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CustomerResponse>(ex.Message);
        }
    }
}
