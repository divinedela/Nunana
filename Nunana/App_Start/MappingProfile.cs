using AutoMapper;
using Nunana.Models;
using Nunana.ViewModels;

namespace Nunana.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomsListViewModel>()
                .ForMember(rl => rl.RoomType, opt => opt.MapFrom(r => r.Type.ToString()));

            CreateMap<Tenant, TenantsListViewModel>()
                .ForMember(tvm => tvm.FullName, opt => opt.MapFrom(t => t.FullName));
        }
    }
}