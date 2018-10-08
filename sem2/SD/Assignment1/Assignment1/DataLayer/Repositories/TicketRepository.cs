using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.DataLayer.Repositories
{
    class TicketRepository: DataLayer.Contracts.ITicketRepository
    {
        private List<DataLayer.Models.TicketDTO> inMemoryTickets = new List<Models.TicketDTO>();
        public TicketRepository()
        {
            //mocking data
            getAllFromDB();
        }

        public List<DataLayer.Models.TicketDTO> getAll()
        {
            return inMemoryTickets;
        }

        public bool getAllFromDB()
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "SELECT * FROM Tickets";
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    DataLayer.Models.TicketDTO Ticket = new Models.TicketDTO();
                    Ticket.ID = (Int32)dr["ID"];
                    Ticket.ShowID = (Int32)dr["ShowID"];
                    Ticket.Row = (Int32)dr["Row"];
                    Ticket.Seat = (Int32)dr["Seat"];
                    inMemoryTickets.Add(Ticket);
                }
                command.Connection.Close();
                return true;
            }
            catch { }

            return false;
        }
        private SqlCommand buildCommand(SqlCommand command, DataLayer.Models.TicketDTO Ticket)
        {
            command.Parameters.AddWithValue("ID", Ticket.ID);
            command.Parameters.AddWithValue("ShowID", Ticket.ShowID);
            command.Parameters.AddWithValue("Row", Ticket.Row);
            command.Parameters.AddWithValue("Seat", Ticket.Seat);
            return command;
        }

        public bool create(DataLayer.Models.TicketDTO Ticket)
        {
            try
            {
                SqlCommand command = buildCommand(new SqlCommand(), Ticket);
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "INSERT INTO Tickets (ShowID,Row,Seat) VALUES (@ShowID,@Row,@Seat)";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //to be optimized
                inMemoryTickets = new List<Models.TicketDTO>();
                getAllFromDB();
                return true;
            }
            catch { }

            return false;
        }
        public bool update(DataLayer.Models.TicketDTO Ticket)
        {
            try
            {
                SqlCommand command = buildCommand(new SqlCommand(), Ticket);
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "UPDATE Tickets Set ShowID= @ShowID,Row=@Row,Seat=@Seat where ID=@ID";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //update DTO
                //DataLayer.Models.TicketDTO TicketOld = inMemoryTickets.Find(x => x.ID == Ticket.ID);
                // TicketOld = Ticket;
                //to be checked
                inMemoryTickets = new List<Models.TicketDTO>();
                getAllFromDB();
                return true;
            }
            catch { }

            return false;
        }
        //sa se faca update cu camp nou de deleted 
        public bool delete(int ID)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();

                command.Parameters.AddWithValue("ID", ID);
                command.CommandText = "DELETE FROM Tickets where ID=@ID";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //update si in DTO
                inMemoryTickets.Remove(inMemoryTickets.Find(x => x.ID == ID));
                return true;
            }
            catch { }

            return false;
        }
    }
}
