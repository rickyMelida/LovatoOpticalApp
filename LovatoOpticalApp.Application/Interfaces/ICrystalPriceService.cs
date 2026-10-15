using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Core.Enums;

namespace LovatoOpticalApp.Application.Interfaces
{
    public interface ICrystalPriceService
    {
        Task<List<CrystalTypeDto>> GetCrystalPriceByTypeAsync(CrystalTypeEnum crystalType);
    }
}