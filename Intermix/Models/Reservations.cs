using System;

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
