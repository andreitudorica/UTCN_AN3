using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.ServicesLayer.Contracts
{
    interface ITicketService
    {
        List<ServicesLayer.Models.TicketModel> getAll();
        bool seatIsTaken(int row, int seat,int showID);
        bool IDExists(int ID);
        void add(int ShowID, int Row, int Seat);
        void update(int ID, int ShowID, int Row, int Seat);
        void delete(int ID);
    }
}
