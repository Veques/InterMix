using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.Models.LibrarySystemModels
{
    public class LoanBook
    {
        public bool IsChecked { get; set; }
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? FullName { get; set; }
        public DateTime PublishDate { get; set; }
        public string Publisher { get; set; }
        public int? IsAvailable { get; set; }
    }
}
