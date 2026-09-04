using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Order;

namespace LovatoOpticalApp.Application.Interfaces
{
    public interface ICrystalOrderWorkService
    {
        Task<Guid> CreateCrystalOrderWork(CrystalOrderWorkRequestDto request);
        Task<PagedResult<CrystalOrderWorkResponseDto>> GetCrystalOrderWorks(PaginationParams parameters);
        Task<CrystalOrderWorkResponseDto?> GetById(Guid id);
        Task<int> GetNextIndex();
    }
}
