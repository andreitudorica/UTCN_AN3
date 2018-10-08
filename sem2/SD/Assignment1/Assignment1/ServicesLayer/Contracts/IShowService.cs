using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Contracts
{
    interface IShowService
    {
        List<ServicesLayer.Models.ShowModel> getAll();
        void ticketSold(int showID);
        void add(string Title, string Genre, string Distribution, DateTime Date, int NumberOfTickets);
        void update(int ID, string Title, string Genre, string Distribution, DateTime Date, int NumberOfTickets);
        void delete(int ID);
        bool titleExists(string title);
        bool IDExists(int ID);
    }
}
