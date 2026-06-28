using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Services
{
    public class MemoryService : IMemoryService
    {
        private readonly IMemoryRepository _repository;
        private readonly IMapper _mapper;

        public MemoryService(IMemoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MemoryDTO?> GetByIdAsync(int id)
        {
            var memory = await _repository.GetByIdAsync(id);
            return memory == null ? null : _mapper.Map<MemoryDTO>(memory);
        }

        public async Task<MemoryDTO?> GetByKeyAsync(string key)
        {
            var memory = await _repository.GetByKeyAsync(key);
            return memory == null ? null : _mapper.Map<MemoryDTO>(memory);
        }

        public async Task<IEnumerable<MemoryDTO>> GetAllAsync()
        {
            var memories = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemoryDTO>>(memories);
        }

        public async Task<MemoryDTO> AddAsync(CreateMemoryDTO dto)
        {
            var memory = _mapper.Map<Memory>(dto);
            await _repository.AddAsync(memory);
            return _mapper.Map<MemoryDTO>(memory);
        }

        public async Task UpdateAsync(UpdateMemoryDTO dto)
        {
            var memory = await _repository.GetByIdAsync(dto.Id);
            if (memory == null)
                throw new KeyNotFoundException($"Memory with id {dto.Id} not found.");

            var updatedMemory = _mapper.Map<Memory>(dto);
            await _repository.UpdateAsync(updatedMemory);
        }

        public async Task DeleteAsync(int id)
        {
            var memory = await _repository.GetByIdAsync(id);
            if (memory == null)
                throw new KeyNotFoundException($"Memory with id {id} not found.");

            await _repository.DeleteAsync(id);
        }
    }
}
