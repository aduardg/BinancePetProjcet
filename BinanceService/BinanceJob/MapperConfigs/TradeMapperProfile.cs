using AutoMapper;
using Domain.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceJob.MapperConfigs
{
    public class TradeMapperProfile:Profile
    {
        public TradeMapperProfile()
        {
            CreateMap<TradeElementView, TradeElement>()
                  .ForMember(e => e.id, e => e.MapFrom(opt => opt.id))
                  .ForMember(e => e.price, e => e.MapFrom(opt => opt.price))
                  .ForMember(e => e.qty, e => e.MapFrom(opt => opt.qty))
                  .ForMember(e => e.isBuyerMaker, e => e.MapFrom(opt => opt.isBuyerMaker))
                  .ForMember(e => e.isBestMatch, e => e.MapFrom(opt => opt.isBestMatch))
                  .ForMember(e => e.namePart, e => e.MapFrom(opt => opt.namePart))
                  .ForMember(e => e.checkColumn, e => e.MapFrom(opt => opt.checkColumn))
                  .ForMember(e => e.time, e => e.Ignore())
                  .AfterMap((entity, dto, __) =>
                  {
                      dto.time = DateTimeOffset.FromUnixTimeMilliseconds(entity.time).DateTime;
                  });
        }
    }
}
