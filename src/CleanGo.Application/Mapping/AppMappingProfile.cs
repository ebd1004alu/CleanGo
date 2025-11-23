using AutoMapper;
using CleanGo.Application.DTOs.Bookings;
using CleanGo.Application.DTOs.Cleaners;
using CleanGo.Application.DTOs.Services;
using CleanGo.Application.DTOs.Users;
using CleanGo.Domain.Entities;

namespace CleanGo.Application.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            // USER
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src =>
                        DateTime.SpecifyKind(src.DateOfBirth, DateTimeKind.Utc)));

            CreateMap<User, UserDto>();


            // CLEANER
            CreateMap<CreateCleanerDto, Cleaner>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src =>
                        DateTime.SpecifyKind(src.DateOfBirth, DateTimeKind.Utc)))
                .ForMember(dest => dest.HireDate,
                    opt => opt.MapFrom(src =>
                        DateTime.SpecifyKind(src.HireDate, DateTimeKind.Utc)));

            CreateMap<Cleaner, CleanerDto>();


            // SERVICE
            CreateMap<CreateServiceDto, Service>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Service, ServiceDto>();


            // BOOKING
            CreateMap<CreateBookingDto, Booking>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.BookingDate,
                    opt => opt.MapFrom(src =>
                        DateTime.SpecifyKind(src.BookingDate, DateTimeKind.Utc)));

            CreateMap<Booking, BookingDto>();
        }
    }
}
