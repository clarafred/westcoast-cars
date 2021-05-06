using API.Entities;
using API.ViewModels;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Vehicle, PresVehicleViewModel>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Description));

                CreateMap<AddNewVehicleViewModel, Vehicle>();
        }
    }
}