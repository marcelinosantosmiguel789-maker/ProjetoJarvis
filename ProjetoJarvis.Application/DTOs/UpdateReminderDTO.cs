namespace ProjetoJarvis.Application.DTOs
{
    public class UpdateReminderDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExecuteAt { get; set; }
    }
}
