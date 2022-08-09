using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermix.Models
{
    public class Reservations
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime ExpectedReturn { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime EndOfReservation { get; set; }

    }
}
