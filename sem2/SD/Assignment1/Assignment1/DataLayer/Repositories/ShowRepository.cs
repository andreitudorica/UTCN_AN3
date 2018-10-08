using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.DataLayer.Repositories
{
    class ShowRepository : DataLayer.Contracts.IShowRepository
    {
        private List<DataLayer.Models.ShowDTO> inMemoryShows = new List<Models.ShowDTO>();
        public ShowRepository()
        {
            //mocking data
            getAllFromDB();
            //update(new DataLayer.Models.ShowDTO { showname = "admin", Password = "JNGuicG0l9amrBO1wj1MuA==", Title = "admin", ID = 1 });
        }

        public List<DataLayer.Models.ShowDTO> getAll()
        {
            return inMemoryShows;
        }

        public bool getAllFromDB()
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "SELECT * FROM Shows";
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    DataLayer.Models.ShowDTO show = new Models.ShowDTO();
                    show.ID = (Int32)dr["ID"];
                    show.Title = dr["Title"].ToString();
                    show.Genre = dr["Genre"].ToString();
                    show.Distribution = dr["Distribution"].ToString();
                    show.Date = DateTime.Parse(dr["Date"].ToString());
                    show.NumberOfTickets = (Int32)dr["NumberOfTickets"];
                    inMemoryShows.Add(show);
                }
                command.Connection.Close();
                return true;
            }
            catch { }

            return false;
        }
        private SqlCommand buildCommand(SqlCommand command, DataLayer.Models.ShowDTO show)
        {
            command.Parameters.AddWithValue("ID", show.ID);
            command.Parameters.AddWithValue("NumberOfTickets", show.NumberOfTickets);
            command.Parameters.AddWithValue("Genre", show.Genre);
            command.Parameters.AddWithValue("Date", show.Date.ToString());
            command.Parameters.AddWithValue("Distribution", show.Distribution);
            command.Parameters.AddWithValue("Title", show.Title);
            return command;
        }

        public bool create(DataLayer.Models.ShowDTO show)
        {
            try
            {
                SqlCommand command = buildCommand(new SqlCommand(), show);
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "INSERT INTO Shows (Title,Genre,Distribution,Date,NumberOfTickets) VALUES (@Title,@Genre,@Distribution,@Date,@NumberOfTickets)";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //to be optimized
                inMemoryShows = new List<Models.ShowDTO>();
                getAllFromDB();
                return true;
            }
            catch { }

            return false;
        }
        public bool update(DataLayer.Models.ShowDTO show)
        {
            try
            {
                SqlCommand command = buildCommand(new SqlCommand(), show);
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "UPDATE Shows Set Title= @Title,Genre=@Genre,Distribution=@Distribution,Date=@Date,NumberOfTickets=@NumberOfTickets where ID=@ID";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //update DTO
                //DataLayer.Models.ShowDTO showOld = inMemoryShows.Find(x => x.ID == show.ID);
                // showOld = show;
                //to be checked
                inMemoryShows = new List<Models.ShowDTO>();
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
                command.CommandText = "DELETE FROM Shows where ID=@ID";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //update si in DTO
                inMemoryShows.Remove(inMemoryShows.Find(x => x.ID == ID));
                return true;
            }
            catch { }

            return false;
        }
    }
}
