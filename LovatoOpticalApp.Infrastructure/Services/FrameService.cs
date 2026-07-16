using LovatoOpticalApp.Core.Entities;
using LovatoOpticalApp.Core.Interfaces;
using LovatoOpticalApp.Persistence;

namespace LovatoOpticalApp.Application.Services;

public class FrameService : IProductService<Frame>
{
    private readonly ProductRepository<Frame> _repository;

    public FrameService(ProductRepository<Frame> repository)
        => _repository = repository;

    public Task AddAsync(Frame product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Frame>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Frame?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Frame product)
    {
        throw new NotImplementedException();
    }
}