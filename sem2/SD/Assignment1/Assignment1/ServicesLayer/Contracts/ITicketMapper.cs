using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Contracts
{
    interface ITicketMapper
    {
        DataLayer.Models.TicketDTO map(ServicesLayer.Models.TicketModel model);
        ServicesLayer.Models.TicketModel map(DataLayer.Models.TicketDTO dto);
    }
}
