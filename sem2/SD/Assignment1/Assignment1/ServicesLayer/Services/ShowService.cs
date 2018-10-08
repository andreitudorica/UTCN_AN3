using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Services
{
    class ShowService : ServicesLayer.Contracts.IShowService
    {
        private readonly DataLayer.Contracts.IShowRepository repository;
        private readonly ServicesLayer.Contracts.IShowMapper mapper;
        public ShowService()
        {
            repository = new DataLayer.Repositories.ShowRepository();
            mapper = new ServicesLayer.Services.ShowMapper();
        }

        public List<ServicesLayer.Models.ShowModel> getAll()
        {
            List<DataLayer.Models.ShowDTO> showDTOs = repository.getAll();
            List<ServicesLayer.Models.ShowModel> models = new List<Models.ShowModel>();
            showDTOs.ForEach(x => models.Add(mapper.map(x))); 
            foreach(ServicesLayer.Models.ShowModel x in models)
                Console.WriteLine(x.Date);
            return models;
        }

        public void ticketSold(int showID)
        {
            ServicesLayer.Models.ShowModel ticket = getAll().Find(x => x.ID == showID);
            if(ticket.NumberOfTickets!=0)
                ticket.NumberOfTickets--;
            update(ticket.ID, ticket.Title, ticket.Genre, ticket.Distribution, ticket.Date, ticket.NumberOfTickets);
        }

        public bool titleExists(string title)
        {
            if (getAll().Find(x => x.Title == title) == null)
                return false;
            return true;
        }

        public bool IDExists(int ID)
        {
            if (getAll().Find(x => x.ID == ID) == null)
                return false;
            return true;
        }

        public void add(string Title, string Genre, string Distribution, DateTime Date, int NumberOfTickets)
        {
            ServicesLayer.Models.ShowModel model = new Models.ShowModel() { Title = Title, Genre = Genre, Distribution = Distribution, NumberOfTickets = NumberOfTickets, Date = Date, ID = 0 };
            bool rez=repository.create(mapper.map(model));
        }

        public void update(int ID, string Title, string Genre, string Distribution, DateTime Date, int NumberOfTickets)
        {
            ServicesLayer.Models.ShowModel model = new Models.ShowModel() { Title = Title, Genre = Genre, Distribution = Distribution, NumberOfTickets = NumberOfTickets, Date = Date, ID = ID };
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
