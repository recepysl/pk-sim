using AutoMapper;
using SimApi.Data;
using SimApi.Data.Domain;

namespace SimApi.Schema;

public class MapperProfile : Profile 
{
    public MapperProfile()
    {

        CreateMap<Account, AccountResponse>();
        CreateMap<AccountRequest, Account>();

        CreateMap<Transaction, TransactionResponse>();

        CreateMap<ApplicationUserRequest, ApplicationUser>();
        CreateMap<ApplicationUser, ApplicationUserResponse>();
    }


}
