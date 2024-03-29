﻿using System;

namespace Intermix.Models
{
    public class Loans
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturn { get; set; }
        public int WasExtended { get; set; }
    }
}
