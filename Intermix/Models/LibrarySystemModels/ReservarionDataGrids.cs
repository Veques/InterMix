using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.Models.LibrarySystemModels
{
    public class ReservarionDataGrids
    {
        public bool IsChecked { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime PossibleLoanDate { get; set; }
        public DateTime EndOfReservation { get; set; }
    }
}
