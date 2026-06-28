using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Domain.Entities;

namespace ProjetoJarvis.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Reminder mappings
            CreateMap<Reminder, ReminderDTO>()
                .ForMember(dest => dest.ReminderDate, opt => opt.MapFrom(src => src.ExecuteAt))
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.Completed));

            CreateMap<CreateReminderDTO, Reminder>()
                .ConstructUsing(src => new Reminder(src.Title, src.Description, src.ExecuteAt));

            CreateMap<UpdateReminderDTO, Reminder>()
                .ConstructUsing(src => new Reminder(src.Title, src.Description, src.ExecuteAt));

            // Message mappings
            CreateMap<Message, MessageDTO>();
            CreateMap<CreateMessageDTO, Message>()
                .ConstructUsing(src => new Message(src.Content, src.Role));

            // Conversation mappings
            CreateMap<Conversation, ConversationDto>();
            CreateMap<CreateConversationDTO, Conversation>();

            // Memory mappings
            CreateMap<Memory, MemoryDTO>();
            CreateMap<CreateMemoryDTO, Memory>()
                .ConstructUsing(src => new Memory(src.Key, src.Value));

            CreateMap<UpdateMemoryDTO, Memory>()
                .ConstructUsing(src => new Memory(src.Key, src.Value));

            // Integration mappings
            CreateMap<Integration, IntegrationDTO>();
            CreateMap<CreateIntegrationDTO, Integration>()
                .ConstructUsing(src => new Integration(src.Name, src.Type));

            CreateMap<UpdateIntegrationDTO, Integration>()
                .ConstructUsing(src => new Integration(src.Name, src.Type));
        }
    }
}
