using System;

namespace Intermix.Models.LibrarySystemModels
{
    public class PrivateLoans
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
