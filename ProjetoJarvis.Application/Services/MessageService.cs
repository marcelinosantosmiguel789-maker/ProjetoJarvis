using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Interfaces;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MessageDTO?> GetByIdAsync(int id)
        {
            var message = await _repository.GetByIdAsync(id);
            return message == null ? null : _mapper.Map<MessageDTO>(message);
        }

        public async Task<IEnumerable<MessageDTO>> GetAllAsync()
        {
            var messages = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MessageDTO>>(messages);
        }

        public async Task<MessageDTO> AddAsync(CreateMessageDTO dto)
        {
            var message = _mapper.Map<Message>(dto);
            await _repository.AddAsync(message);
            return _mapper.Map<MessageDTO>(message);
        }
    }
}
