using AutoMapper;
using Nunana.DTOs;
using Nunana.Models;
using Nunana.ViewModels;

namespace Nunana.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Map Domain Objects to DTOs

            CreateMap<Room, RoomsListViewModel>()
                .ForMember(rl => rl.RoomType, opt => opt.MapFrom(r => r.Type.ToString()));
            CreateMap<Tenant, TenantsListViewModel>()
                .ForMember(tvm => tvm.FullName, opt => opt.MapFrom(t => t.FullName));
            CreateMap<Room, RoomFormViewModel>();
            CreateMap<Tenant, TenantFormViewModel>();
            CreateMap<Tenant, TenantSearchDto>()
                .ForMember(tvm => tvm.Name, opt => opt.MapFrom(t => t.FullName));

            //Map DTOs to Domain Objects

            CreateMap<RoomFormViewModel, Room>()
                .ForMember(r => r.Id, opt => opt.Ignore())
                .ForMember(r => r.DateCreated, opt => opt.Ignore())
                .ForMember(r => r.IsCurrentlyRented, opt => opt.Ignore())
                .ForMember(r => r.CreatedBy, opt => opt.Ignore());

            CreateMap<TenantFormViewModel, Tenant>()
                .ForMember(t => t.Id, opt => opt.Ignore())
                .ForMember(t => t.DateCreated, opt => opt.Ignore());

            CreateMap<SaveRentalDto, Rental>()
                .ForMember(t => t.Room, opt => opt.Ignore())
                .ForMember(t => t.CreatedBy, opt => opt.Ignore())
                .ForMember(t => t.DateCreated, opt => opt.Ignore())
                .ForMember(t => t.StartDate, opt => opt.Ignore())
                .ForMember(t => t.EndDate, opt => opt.Ignore())
                .ForMember(t => t.DateCancelled, opt => opt.Ignore())
                .ForMember(t => t.IsCancelled, opt => opt.Ignore());
        }
    }
}