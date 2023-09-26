using AasanApis.Data.Entities;
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
            CreateMap<ShahkarRequestsLogEntity,ShahkarRequestsLogDTO>().ReverseMap();
            CreateMap<RefreshTokenResDTO,RefreshTokenRes>().ReverseMap();
            CreateMap<MatchingEncryptResDTO,MatchingEncryptRes>().ReverseMap();
        }
    }
}
