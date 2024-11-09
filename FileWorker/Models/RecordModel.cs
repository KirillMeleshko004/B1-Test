namespace FileWorker.Domain.Entities
{
    public class RecordModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Latin { get; set; } = null!;
        public string Cyrillic { get; set; } = null!;
        public int PositiveEven { get; set; }
        public double Float { get; set; }
    }
}