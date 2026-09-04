using LovatoOpticalApp.Application.DTOs;
using LovatoOpticalApp.Application.DTOs.Order;
using LovatoOpticalApp.Application.Interfaces;
using LovatoOpticalApp.Core;
using LovatoOpticalApp.Persistence.Interfaces;

namespace LovatoOpticalApp.Application.Services
{
    public class CrystalOrderWorkService : ICrystalOrderWorkService
    {
        private readonly ICrystalOrderWorkRepository _repository;

        public CrystalOrderWorkService(ICrystalOrderWorkRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> CreateCrystalOrderWork(CrystalOrderWorkRequestDto request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var entity = new CrystalOrderWork
            {
                Material = request.Material ?? string.Empty,
                Index = int.TryParse(request.Index, out var idx) ? idx : 0,
                TreatmentNotes = request.TreatmentNotes ?? string.Empty,

                OD_ESF = request.OD_ESF ?? string.Empty,
                OD_CIL = request.OD_CIL ?? string.Empty,
                OD_AXIS = request.OD_AXIS ?? string.Empty,
                OD_ADD = request.OD_ADD ?? string.Empty,
                OD_DNP = request.OD_DNP ?? string.Empty,
                OD_HEIGHT = request.OD_HEIGHT ?? string.Empty,

                OI_ESF = request.OI_ESF ?? string.Empty,
                OI_CIL = request.OI_CIL ?? string.Empty,
                OI_AXIS = request.OI_AXIS ?? string.Empty,
                OI_ADD = request.OI_ADD ?? string.Empty,
                OI_DNP = request.OI_DNP ?? string.Empty,
                OI_HEIGHT = request.OI_HEIGHT ?? string.Empty,

                Mounting = request.Mounting ?? string.Empty,
                Horizontal = request.Horizontal ?? string.Empty,
                Vertical = request.Vertical ?? string.Empty,
                MajorDiagonal = request.MajorDiagonal ?? string.Empty,
                Bridge = request.Bridge ?? string.Empty,
                PantoscopicAngle = request.PantoscopicAngle ?? string.Empty,
                PanoramicAngle = request.PanoramicAngle ?? string.Empty,
                Observation = request.Observations ?? string.Empty,
            };

            await _repository.AddAsync(entity);
            return entity.Id;
        }

        public async Task<PagedResult<CrystalOrderWorkResponseDto>> GetCrystalOrderWorks(PaginationParams parameters)
        {
            parameters ??= new PaginationParams();

            var pageNumber = parameters.PageNumber > 0 ? parameters.PageNumber : 1;
            var pageSize = parameters.PageSize > 0 ? parameters.PageSize : 10;

            var (items, totalCount) = await _repository.GetPagedAsync(pageNumber, pageSize);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var safePageNumber = Math.Min(pageNumber, Math.Max(totalPages, 1));

            var mapped = items.Select(MapToResponse).ToList();

            return new PagedResult<CrystalOrderWorkResponseDto>
            {
                Items = mapped,
                TotalCount = totalCount,
                PageNumber = safePageNumber,
                PageSize = pageSize
            };
        }

        public async Task<CrystalOrderWorkResponseDto?> GetById(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? null : MapToResponse(entity);
        }

        public Task<int> GetNextIndex() => _repository.GetNextIndexAsync();

        private static CrystalOrderWorkResponseDto MapToResponse(CrystalOrderWork c) => new()
        {
            Id = c.Id,
            CreatedAt = c.CreatedAt,
            State = c.State.ToString(),
            Material = c.Material,
            Index = c.Index,
            TreatmentNotes = c.TreatmentNotes,
            OD_ESF = c.OD_ESF,
            OD_CIL = c.OD_CIL,
            OD_AXIS = c.OD_AXIS,
            OD_ADD = c.OD_ADD,
            OD_DNP = c.OD_DNP,
            OD_HEIGHT = c.OD_HEIGHT,
            OI_ESF = c.OI_ESF,
            OI_CIL = c.OI_CIL,
            OI_AXIS = c.OI_AXIS,
            OI_ADD = c.OI_ADD,
            OI_DNP = c.OI_DNP,
            OI_HEIGHT = c.OI_HEIGHT,
            Mounting = c.Mounting,
            Horizontal = c.Horizontal,
            Vertical = c.Vertical,
            MajorDiagonal = c.MajorDiagonal,
            Bridge = c.Bridge,
            PantoscopicAngle = c.PantoscopicAngle,
            PanoramicAngle = c.PanoramicAngle,
            Observation = c.Observation,
        };
    }
}
