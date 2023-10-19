using AutoMapper;
using BusinessObjects;
using DTOs;
using DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountDTO, Account>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Account, RegisterRequest>().ReverseMap();
            CreateMap<AccountDTO, RegisterRequest>().ReverseMap();
            
            //Hiep
            CreateMap<AccountUpdateProfileRequest, Account>().ReverseMap();
        }
    }
}
