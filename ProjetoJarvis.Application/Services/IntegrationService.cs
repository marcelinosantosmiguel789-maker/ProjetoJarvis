using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IIntegrationRepository _repository;
        private readonly IMapper _mapper;

        public IntegrationService(IIntegrationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IntegrationDTO?> GetByIdAsync(int id)
        {
            var integration = await _repository.GetByIdAsync(id);
            return integration == null ? null : _mapper.Map<IntegrationDTO>(integration);
        }

        public async Task<IEnumerable<IntegrationDTO>> GetAllAsync()
        {
            var integrations = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<IntegrationDTO>>(integrations);
        }

        public async Task<IntegrationDTO> AddAsync(CreateIntegrationDTO dto)
        {
            var integration = _mapper.Map<Integration>(dto);
            await _repository.AddAsync(integration);
            return _mapper.Map<IntegrationDTO>(integration);
        }

        public async Task UpdateAsync(UpdateIntegrationDTO dto)
        {
            var integration = await _repository.GetByIdAsync(dto.Id);
            if (integration == null)
                throw new KeyNotFoundException($"Integration with id {dto.Id} not found.");

            var updatedIntegration = _mapper.Map<Integration>(dto);
            await _repository.UpdateAsync(updatedIntegration);
        }

        public async Task DeleteAsync(int id)
        {
            var integration = await _repository.GetByIdAsync(id);
            if (integration == null)
                throw new KeyNotFoundException($"Integration with id {id} not found.");

            await _repository.DeleteAsync(id);
        }
    }
}
