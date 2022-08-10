using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.Models.LibrarySystemModels
{
    public class Loan
    {
        public string Username { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime LastReservationDay { get; set; }
        public string Status { get; set; }
    }
}
