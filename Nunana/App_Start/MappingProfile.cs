using AutoMapper;
using Nunana.DTOs;
using Nunana.Extensions;
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

            CreateMap<Room, RoomFormViewModel>();

            CreateMap<Room, RoomDto>();

            CreateMap<Tenant, TenantsListViewModel>()
                .ForMember(tvm => tvm.FullName, opt => opt.MapFrom(t => t.FullName));

            CreateMap<Tenant, TenantFormViewModel>();

            CreateMap<Tenant, TenantSearchDto>()
                .ForMember(tvm => tvm.Name, opt => opt.MapFrom(t => t.FullName));

            CreateMap<Rental, RentalListViewModel>()
                .ForMember(vm => vm.RentalStartDate, opt => opt.MapFrom(t => t.StartDate.ToShortDateString()))
                .ForMember(vm => vm.RentalEndDate, opt => opt.MapFrom(t => t.EndDate.ToShortDateString()))
                .ForMember(vm => vm.TenantName, opt => opt.MapFrom(t => t.Tenant.FullName))
                .ForMember(vm => vm.CreatorName, opt => opt.MapFrom(t => t.CreatedBy))
                .ForMember(vm => vm.RoomNumber, opt => opt.MapFrom(t => t.Room.RoomNumber))
                .ForMember(vm => vm.NumberOfMonths, opt => opt.MapFrom(t => DateTimeExtensions.GetDifferenceInMonths(t.StartDate, t.EndDate)));



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