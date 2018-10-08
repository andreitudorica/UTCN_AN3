using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1.DataLayer.Models;
using Assignment1.ServicesLayer.Models;

namespace Assignment1.ServicesLayer.Services
{
    class TicketMapper : ServicesLayer.Contracts.ITicketMapper
    {
        public TicketModel map(TicketDTO dto)
        {
            ServicesLayer.Models.TicketModel model = new ServicesLayer.Models.TicketModel();
            model.ID = dto.ID;
            model.ShowID = dto.ShowID;
            model.Row = dto.Row;
            model.Seat = dto.Seat;
            return model;
        }

        public TicketDTO map(TicketModel model)
        {
            DataLayer.Models.TicketDTO dto = new DataLayer.Models.TicketDTO();
            dto.ID = model.ID;
            dto.ShowID = model.ShowID;
            dto.Row = model.Row;
            dto.Seat = model.Seat;
            return dto;
        }
    }
}
