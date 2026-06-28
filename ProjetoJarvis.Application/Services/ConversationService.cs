using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _repository;
        private readonly IMapper _mapper;

        public ConversationService(IConversationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ConversationDto?> GetByIdAsync(int id)
        {
            var conversation = await _repository.GetByIdAsync(id);
            return conversation == null ? null : _mapper.Map<ConversationDto>(conversation);
        }

        public async Task<IEnumerable<ConversationDto>> GetAllAsync()
        {
            var conversations = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ConversationDto>>(conversations);
        }

        public async Task<ConversationDto> CreateAsync()
        {
            var conversation = new Conversation();
            await _repository.AddAsync(conversation);
            return _mapper.Map<ConversationDto>(conversation);
        }

        public async Task UpdateAsync(int id)
        {
            var conversation = await _repository.GetByIdAsync(id);
            if (conversation == null)
                throw new KeyNotFoundException($"Conversation with id {id} not found.");

            await _repository.UpdateAsync(conversation);
        }

        public async Task DeleteAsync(int id)
        {
            var conversation = await _repository.GetByIdAsync(id);
            if (conversation == null)
                throw new KeyNotFoundException($"Conversation with id {id} not found.");

            await _repository.DeleteAsync(id);
        }
    }
}
