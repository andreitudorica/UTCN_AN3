using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Services
{
    class ShowMapper:ServicesLayer.Contracts.IShowMapper
    {
        public ServicesLayer.Models.ShowModel map(DataLayer.Models.ShowDTO dto)
        {
            ServicesLayer.Models.ShowModel model = new ServicesLayer.Models.ShowModel();
            model.ID = dto.ID;
            model.Title = dto.Title;
            model.Genre = dto.Genre;
            model.Distribution = dto.Distribution;
            model.Date = dto.Date;
            model.NumberOfTickets = dto.NumberOfTickets;
            return model;
        }
        public DataLayer.Models.ShowDTO map(ServicesLayer.Models.ShowModel model)
        {
            DataLayer.Models.ShowDTO dto = new DataLayer.Models.ShowDTO();
            dto.ID = model.ID;
            dto.Title = model.Title;
            dto.Genre = model.Genre;
            dto.Distribution = model.Distribution;
            dto.Date = model.Date;
            dto.NumberOfTickets = model.NumberOfTickets;
            return dto;
        }
    }
}
