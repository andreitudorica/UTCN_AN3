using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Models
{
    class ShowModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Distribution { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfTickets { get; set; }
    }
}
