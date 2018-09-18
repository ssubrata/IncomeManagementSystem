
using AutoMapper;
using Desktop.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Helper
{
    public static class Map
    {

        private static bool _intialized = false;
        public static void Initialize()
        {
            if (_intialized) return;

            Mapper.Initialize(config =>
            {
               
                config.CreateMap<IncomeSource, VmIncomeSource>().ReverseMap();
                config.CreateMap<SpendSource, VmSpendSource>().ReverseMap();
                config.CreateMap<User, VmUser>()
                .ForMember(d => d.VmRole, s => s.MapFrom(op => op.Role))
                .ReverseMap();

                config.CreateMap<Role, VmRole>().ReverseMap();

                config.CreateMap<Income, VmIncome>()
                .ForMember(d => d.Id, s => s.MapFrom(op => op.IncomeId))
                .ForMember(d => d.VmIncomeSource, s => s.MapFrom(op => op.IncomeSource)).ReverseMap();

                config.CreateMap<Spend, VmSpend>()
                .ForMember(d => d.Id, s => s.MapFrom(op => op.SpendId))
                .ForMember(d => d.VmSpendSource, s => s.MapFrom(op => op.SpendSource)).ReverseMap();

                _intialized = true;


            });
        }
    }
}
