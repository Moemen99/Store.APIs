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
                opt => opt.MapFrom(src => $"{src.StoreFileDate.Date.Day}/{src.StoreFileDate.Date.Month}/{src.StoreFileDate.Date.Year}"));

            CreateMap<Transaction, TransactionToReturnDTO>().ForMember(dest => dest.TransactionDate, opt => opt.
            MapFrom(src => $"{src.TransactionDate.Date.Day}-{src.TransactionDate.Date.Month}-{src.TransactionDate.Date.Year}" ));

            CreateMap<Good, GoodToReturnDTO>()
                .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions.Select(t => new TransactionToReturnDTO
                {
                    GoodID = t.GoodID,
                    TransactionID = t.TransactionID,
                    TransactionDate = $"{t.TransactionDate.Day}/{t.TransactionDate.Month}/{t.TransactionDate.Year}",
                    Amount = t.Amount,
                    Direction = t.Direction,    
                    Comment = t.Comment?? string.Empty
                })));
           

              
        }
    }
    
}
