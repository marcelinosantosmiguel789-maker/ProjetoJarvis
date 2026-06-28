namespace ProjetoJarvis.Application.DTOs
{
    public class CreateReminderDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExecuteAt { get; set; }
    }
}
