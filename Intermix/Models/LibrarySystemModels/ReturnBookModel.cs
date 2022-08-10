using System;

namespace Intermix.Models.LibrarySystemModels
{
    public class ReturnBook
    {
        public bool IsChecked { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
