using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.DataLayer.Models
{
    class TicketDTO
    {
        public int ID { get; set; }
        public int ShowID { get; set; }
        public int Row { get; set; }
        public int Seat { get; set; }
    }
}
