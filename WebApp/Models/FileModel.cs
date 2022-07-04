namespace WebApp.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Extension { get; set; }
        public int Size { get; set; }
        public string UserId { get; set; }
    }
}
