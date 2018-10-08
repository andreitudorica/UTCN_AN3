using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.DataLayer.Contracts
{
    interface ITicketRepository
    {
        List<DataLayer.Models.TicketDTO> getAll();
        bool create(DataLayer.Models.TicketDTO ticket);
        bool update(DataLayer.Models.TicketDTO ticket);
        bool delete(int ID);
    }
}
