using AutoMapper;
using Store.APIs.DTOs;
using Store.Core.Entities;

namespace Store.APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<StoreInfo, StoreToReturnDTO>().ForMember(dest => dest.StoreFileDate, 
                opt => opt.MapFrom(src => $"{src.StoreFileDate.Day}/{src.StoreFileDate.Month}/{src.StoreFileDate.Year}"));
            
            CreateMap<Transaction, TransactionToReturnDTO>();

            CreateMap<Good, GoodToReturnDTO>()
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.Select(t => new TransactionToReturnDTO
                {
                    TransactionID = t.TransactionID,
                    TransactionDate = $"{t.TransactionDate.Day}/{t.TransactionDate.Month}/{t.TransactionDate.Year}",
                    Amount = t.Amount,
                    Direction = t.Direction,    
                    Comment = t.Comment?? string.Empty
                })));
           

              
        }
    }
    
}
