using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Services
{
    class TicketService : ServicesLayer.Contracts.ITicketService
    {
        private readonly DataLayer.Contracts.ITicketRepository repository;
        private readonly ServicesLayer.Contracts.ITicketMapper mapper;
        public TicketService()
        {
            repository = new DataLayer.Repositories.TicketRepository();
            mapper = new ServicesLayer.Services.TicketMapper();
        }

        public List<ServicesLayer.Models.TicketModel> getAll()
        {
            List<DataLayer.Models.TicketDTO> TicketDTOs = repository.getAll();
            List<ServicesLayer.Models.TicketModel> models = new List<Models.TicketModel>();
            TicketDTOs.ForEach(x => models.Add(mapper.map(x)));
            return models;
        }


        public bool seatIsTaken(int row,int seat, int showID)
        {
            if(getAll().Find(x=>x.Row==row && x.Seat==seat && x.ShowID==showID)==null)
            return false;
            return true;
        }

        public bool IDExists(int ID)
        {
            if (getAll().Find(x => x.ID == ID) == null)
                return false;
            return true;
        }

        public void add(int ShowID, int Row, int Seat)
        {
            ServicesLayer.Models.TicketModel model = new Models.TicketModel() { ShowID = ShowID, Row = Row, Seat = Seat, ID = 0 };
            bool rez = repository.create(mapper.map(model));
        }

        public void update(int ID, int ShowID, int Row, int Seat)
        {
            ServicesLayer.Models.TicketModel model = new Models.TicketModel() { ShowID = ShowID, Row = Row, Seat = Seat, ID = ID };
            bool res;//for testing
            res = repository.update(mapper.map(model));
        }

        public void delete(int ID)
        {
            bool res;//for testing
            res = repository.delete(ID);
        }
    }
}
