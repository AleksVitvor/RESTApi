namespace RESTWithSwagger.Profiles
{
    using AutoMapper;
    using Models.DataModels;
    using Models.FrontModels;
    using Models.ViewModels;

    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Region, RegionDto>()
                .ForMember(dst => dst.Name, src => src.MapFrom(r => r.Name))
                .ForMember(dst => dst.Weathers, src => src.MapFrom(r => r.Weathers))
                .ReverseMap();

            CreateMap<Weather, WeatherDto>()
                .ForMember(dst => dst.DateAndTime, src => src.MapFrom(r => r.DateAndTime))
                .ForMember(dst => dst.Degrees, src => src.MapFrom(r => r.Degrees))
                .ReverseMap();

            CreateMap<Region, WeatherDisplay>()
                .ForMember(dst => dst.RegionName, src => src.MapFrom(r => r.Name))
                .ForMember(dst => dst.Weathers, src => src.MapFrom(r => r.Weathers))
                .ReverseMap();

            CreateMap<WeatherCreation, Weather>()
                .ForMember(dst => dst.DateAndTime, src => src.MapFrom(r => r.DateAndTime))
                .ForMember(dst => dst.Degrees, src => src.MapFrom(r => r.Degrees))
                .ReverseMap();

            CreateMap<RegionCreation, Region>()
                .ForMember(dst => dst.Name, src => src.MapFrom(r => r.Name))
                .ReverseMap();
        }
    }
}
