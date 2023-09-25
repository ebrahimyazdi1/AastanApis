using AasanApis.Models;
using AutoMapper;

namespace AasanApis.Infrastructure.Mapper
{
    public class AastanProfile :Profile
    {
        public AastanProfile()
        {
            CreateMap<RefreshTokenReqDTO, RefreshTokenReq>().ReverseMap();
            CreateMap<MatchingEncryptReqDTO, MatchingEncryptReq>().ReverseMap();
        }
    }
}
