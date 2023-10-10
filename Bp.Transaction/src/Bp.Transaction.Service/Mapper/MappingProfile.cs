using AutoMapper;
using Bp.Transaction.Service.Entities;

namespace Bp.Transaction.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<TransactionBalance, TransactionBalanceDto>().ForMember(dest => dest.Account, opt => opt.Ignore()); 
        }
    }
}