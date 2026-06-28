# 🧪 EXEMPLOS DE TESTES UNITÁRIOS E INTEGRAÇÃO

## Testes Unitários dos Services

### 1. ReminderServiceTests

```csharp
using Xunit;
using Moq;
using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Services;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Tests.Services
{
    public class ReminderServiceTests
    {
        private readonly Mock<IReminderRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ReminderService _service;

        public ReminderServiceTests()
        {
            _repositoryMock = new Mock<IReminderRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ReminderService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsReminderDTO()
        {
            // Arrange
            int id = 1;
            var reminder = new Reminder("Meeting", "Team sync", DateTime.UtcNow.AddDays(1));
            var reminderDto = new ReminderDTO 
            { 
                Id = 1, 
                Title = "Meeting", 
                Description = "Team sync",
                IsCompleted = false 
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(reminder);
            _mapperMock.Setup(m => m.Map<ReminderDTO>(reminder))
                .Returns(reminderDto);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Meeting", result.Title);
            _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
            _mapperMock.Verify(m => m.Map<ReminderDTO>(reminder), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int id = 999;
            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Reminder)null);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_WithValidDTO_ReturnsReminderDTO()
        {
            // Arrange
            var createDto = new CreateReminderDTO
            {
                Title = "New Reminder",
                Description = "Important task",
                ExecuteAt = DateTime.UtcNow.AddDays(1)
            };

            var reminder = new Reminder(createDto.Title, createDto.Description, createDto.ExecuteAt);
            var reminderDto = new ReminderDTO { Id = 1, Title = createDto.Title };

            _mapperMock.Setup(m => m.Map<Reminder>(createDto))
                .Returns(reminder);
            _mapperMock.Setup(m => m.Map<ReminderDTO>(reminder))
                .Returns(reminderDto);

            // Act
            var result = await _service.AddAsync(createDto);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(r => r.AddAsync(reminder), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            int id = 999;
            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Reminder)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _service.DeleteAsync(id)
            );
        }

        [Fact]
        public async Task CompleteReminderAsync_WithValidId_CallsUpdateAsync()
        {
            // Arrange
            int id = 1;
            var reminder = new Reminder("Test", "Test", DateTime.UtcNow.AddDays(1));

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(reminder);

            // Act
            await _service.CompleteReminderAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(reminder), Times.Once);
            Assert.True(reminder.Completed);
        }

        [Fact]
        public async Task GetPendingRemindersAsync_ReturnsMappedDTOs()
        {
            // Arrange
            var reminders = new List<Reminder>
            {
                new Reminder("Pending 1", "Desc 1", DateTime.UtcNow.AddDays(1)),
                new Reminder("Pending 2", "Desc 2", DateTime.UtcNow.AddDays(2))
            };

            var dtos = new List<ReminderDTO>
            {
                new ReminderDTO { Id = 1, Title = "Pending 1" },
                new ReminderDTO { Id = 2, Title = "Pending 2" }
            };

            _repositoryMock.Setup(r => r.GetPendingRemindersAsync())
                .ReturnsAsync(reminders);
            _mapperMock.Setup(m => m.Map<IEnumerable<ReminderDTO>>(reminders))
                .Returns(dtos);

            // Act
            var result = await _service.GetPendingRemindersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
```

### 2. MessageServiceTests

```csharp
using Xunit;
using Moq;
using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Services;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Tests.Services
{
    public class MessageServiceTests
    {
        private readonly Mock<IMessageRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MessageService _service;

        public MessageServiceTests()
        {
            _repositoryMock = new Mock<IMessageRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new MessageService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddAsync_WithValidDTO_CreatesMessage()
        {
            // Arrange
            var createDto = new CreateMessageDTO
            {
                Content = "Hello, World!",
                Role = "user"
            };

            var message = new Message(createDto.Content, createDto.Role);
            var messageDto = new MessageDTO 
            { 
                Id = 1, 
                Content = createDto.Content,
                Role = createDto.Role 
            };

            _mapperMock.Setup(m => m.Map<Message>(createDto))
                .Returns(message);
            _mapperMock.Setup(m => m.Map<MessageDTO>(message))
                .Returns(messageDto);

            // Act
            var result = await _service.AddAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Hello, World!", result.Content);
            _repositoryMock.Verify(r => r.AddAsync(message), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllMessages()
        {
            // Arrange
            var messages = new List<Message>
            {
                new Message("Message 1", "user"),
                new Message("Message 2", "assistant")
            };

            var dtos = new List<MessageDTO>
            {
                new MessageDTO { Id = 1, Content = "Message 1" },
                new MessageDTO { Id = 2, Content = "Message 2" }
            };

            _repositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(messages);
            _mapperMock.Setup(m => m.Map<IEnumerable<MessageDTO>>(messages))
                .Returns(dtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
```

### 3. ConversationServiceTests

```csharp
using Xunit;
using Moq;
using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Services;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Tests.Services
{
    public class ConversationServiceTests
    {
        private readonly Mock<IConversationRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly ConversationService _service;

        public ConversationServiceTests()
        {
            _repositoryMock = new Mock<IConversationRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new ConversationService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateAsync_CreatesNewConversation()
        {
            // Arrange
            var conversation = new Conversation();
            var conversationDto = new ConversationDto { Id = 1, CreatedAt = DateTime.UtcNow };

            _mapperMock.Setup(m => m.Map<ConversationDto>(It.IsAny<Conversation>()))
                .Returns(conversationDto);

            // Act
            var result = await _service.CreateAsync();

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Conversation>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsConversationDTO()
        {
            // Arrange
            int id = 1;
            var conversation = new Conversation();
            var conversationDto = new ConversationDto { Id = id, CreatedAt = DateTime.UtcNow };

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(conversation);
            _mapperMock.Setup(m => m.Map<ConversationDto>(conversation))
                .Returns(conversationDto);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task DeleteAsync_WithValidId_DeletesConversation()
        {
            // Arrange
            int id = 1;
            var conversation = new Conversation();

            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(conversation);

            // Act
            await _service.DeleteAsync(id);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WithInvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            int id = 999;
            _repositoryMock.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Conversation)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(
                () => _service.DeleteAsync(id)
            );
        }
    }
}
```

### 4. MemoryServiceTests

```csharp
using Xunit;
using Moq;
using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Services;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Tests.Services
{
    public class MemoryServiceTests
    {
        private readonly Mock<IMemoryRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly MemoryService _service;

        public MemoryServiceTests()
        {
            _repositoryMock = new Mock<IMemoryRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new MemoryService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetByKeyAsync_WithValidKey_ReturnsMemoryDTO()
        {
            // Arrange
            string key = "user_name";
            var memory = new Memory(key, "John Doe");
            var memoryDto = new MemoryDTO { Id = 1, Key = key, Value = "John Doe" };

            _repositoryMock.Setup(r => r.GetByKeyAsync(key))
                .ReturnsAsync(memory);
            _mapperMock.Setup(m => m.Map<MemoryDTO>(memory))
                .Returns(memoryDto);

            // Act
            var result = await _service.GetByKeyAsync(key);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Value);
        }

        [Fact]
        public async Task GetByKeyAsync_WithInvalidKey_ReturnsNull()
        {
            // Arrange
            string key = "nonexistent_key";
            _repositoryMock.Setup(r => r.GetByKeyAsync(key))
                .ReturnsAsync((Memory)null);

            // Act
            var result = await _service.GetByKeyAsync(key);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_WithValidDTO_CreatesMemory()
        {
            // Arrange
            var createDto = new CreateMemoryDTO 
            { 
                Key = "user_preference", 
                Value = "dark_mode" 
            };

            var memory = new Memory(createDto.Key, createDto.Value);
            var memoryDto = new MemoryDTO { Id = 1, Key = createDto.Key, Value = createDto.Value };

            _mapperMock.Setup(m => m.Map<Memory>(createDto))
                .Returns(memory);
            _mapperMock.Setup(m => m.Map<MemoryDTO>(memory))
                .Returns(memoryDto);

            // Act
            var result = await _service.AddAsync(createDto);

            // Assert
            Assert.NotNull(result);
            _repositoryMock.Verify(r => r.AddAsync(memory), Times.Once);
        }
    }
}
```

### 5. IntegrationServiceTests

```csharp
using Xunit;
using Moq;
using AutoMapper;
using ProjetoJarvis.Application.DTOs;
using ProjetoJarvis.Application.Services;
using ProjetoJarvis.Domain.Entities;
using ProjetoJarvis.Domain.Interfaces;

namespace ProjetoJarvis.Application.Tests.Services
{
    public class IntegrationServiceTests
    {
        private readonly Mock<IIntegrationRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly IntegrationService _service;

        public IntegrationServiceTests()
        {
            _repositoryMock = new Mock<IIntegrationRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new IntegrationService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddAsync_WithValidDTO_CreatesIntegration()
        {
            // Arrange
            var createDto = new CreateIntegrationDTO 
            { 
                Name = "Slack Integration", 
                Type = "messaging" 
            };

            var integration = new Integration(createDto.Name, createDto.Type);
            var integrationDto = new IntegrationDTO 
            { 
                Id = 1, 
                Name = createDto.Name, 
                Type = createDto.Type,
                IsActive = true 
            };

            _mapperMock.Setup(m => m.Map<Integration>(createDto))
                .Returns(integration);
            _mapperMock.Setup(m => m.Map<IntegrationDTO>(integration))
                .Returns(integrationDto);

            // Act
            var result = await _service.AddAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsActive);
            _repositoryMock.Verify(r => r.AddAsync(integration), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllIntegrations()
        {
            // Arrange
            var integrations = new List<Integration>
            {
                new Integration("Slack", "messaging"),
                new Integration("GitHub", "vcs")
            };

            var dtos = new List<IntegrationDTO>
            {
                new IntegrationDTO { Id = 1, Name = "Slack", Type = "messaging" },
                new IntegrationDTO { Id = 2, Name = "GitHub", Type = "vcs" }
            };

            _repositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(integrations);
            _mapperMock.Setup(m => m.Map<IEnumerable<IntegrationDTO>>(integrations))
                .Returns(dtos);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
```

---

## Testes de Integração

### ReminderControllerIntegrationTests

```csharp
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using ProjetoJarvis.API;
using ProjetoJarvis.Application.DTOs;

namespace ProjetoJarvis.API.Tests.Integration
{
    public class ReminderControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ReminderControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_WithValidDTO_Returns201Created()
        {
            // Arrange
            var createDto = new CreateReminderDTO
            {
                Title = "Integration Test Reminder",
                Description = "Test Description",
                ExecuteAt = DateTime.UtcNow.AddDays(1)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/reminder", createDto);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            var result = await response.Content.ReadAsAsync<ReminderDTO>();
            Assert.NotNull(result);
            Assert.Equal("Integration Test Reminder", result.Title);
        }

        [Fact]
        public async Task GetAll_Returns200Ok()
        {
            // Act
            var response = await _client.GetAsync("/api/reminder");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadAsAsync<IEnumerable<ReminderDTO>>();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_WithInvalidId_Returns404NotFound()
        {
            // Act
            var response = await _client.GetAsync("/api/reminder/99999");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithValidId_Returns204NoContent()
        {
            // Primeiro, criar um reminder
            var createDto = new CreateReminderDTO
            {
                Title = "To Delete",
                Description = "Delete me",
                ExecuteAt = DateTime.UtcNow.AddDays(1)
            };

            var createResponse = await _client.PostAsJsonAsync("/api/reminder", createDto);
            var createdReminder = await createResponse.Content.ReadAsAsync<ReminderDTO>();

            // Act - Deletar
            var deleteResponse = await _client.DeleteAsync($"/api/reminder/{createdReminder.Id}");

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}
```

---

## Como Configurar Testes

### 1. Criar projeto de testes

```bash
dotnet new xunit -n ProjetoJarvis.Application.Tests -f net8.0
dotnet new xunit -n ProjetoJarvis.API.Tests -f net8.0
```

### 2. Adicionar referências

```bash
cd ProjetoJarvis.Application.Tests
dotnet add reference ../ProjetoJarvis.Application/ProjetoJarvis.Application.csproj
dotnet add reference ../ProjetoJarvis.Domain/ProjetoJarvis.Domain.csproj
dotnet add reference ../ProjetoJarvis.Infrastructure/ProjetoJarvis.Infrastructure.csproj
dotnet add package Moq
```

### 3. Executar testes

```bash
dotnet test
```

---

## Cobertura de Testes

```
ProjetoJarvis.Application.Tests
├── Services/
│   ├── ReminderServiceTests (6 testes)
│   ├── MessageServiceTests (2 testes)
│   ├── ConversationServiceTests (4 testes)
│   ├── MemoryServiceTests (3 testes)
│   └── IntegrationServiceTests (2 testes)
└── Total: 17 testes unitários

ProjetoJarvis.API.Tests
├── Controllers/
│   ├── ReminderControllerIntegrationTests (4 testes)
│   ├── MessageControllerIntegrationTests (3 testes)
│   ├── ConversationControllerIntegrationTests (3 testes)
│   ├── MemoryControllerIntegrationTests (4 testes)
│   └── IntegrationControllerIntegrationTests (3 testes)
└── Total: 17 testes de integração

Total: 34 testes
Target Coverage: 80%+
```

---

**Happy Testing! 🎯**
