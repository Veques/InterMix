using System;
using System.Windows.Media;

namespace Intermix.Models.LibrarySystemModels
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string Publisher { get; set; }
        public string Status { get; set; }
        public SolidColorBrush StatusColor { get; set; }

    }
}
