using System;

namespace Intermix.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorSurname { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Publisher { get; set; }
        public int IsAvailable { get; set; } //is available to loan
        public int IsReserved { get; set; }

    }
}
