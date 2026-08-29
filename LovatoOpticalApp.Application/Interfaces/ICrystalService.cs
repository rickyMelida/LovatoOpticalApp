using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Common;

namespace LovatoOpticalApp.Application.Interfaces
{
    public interface ICrystalService
    {
        Task<Guid> CreateCrystal(CrystalDtoRequest crystalDto);
        Task<CrystalDtoResponse> GetCrystalById(Guid id);
    }
}