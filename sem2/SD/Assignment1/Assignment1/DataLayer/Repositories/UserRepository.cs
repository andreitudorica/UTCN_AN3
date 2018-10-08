using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1.DataLayer.Repositories
{
    class UserRepository : DataLayer.Contracts.IUserRepository
    {
        private  List<DataLayer.Models.UserDTO> inMemoryUsers = new List<Models.UserDTO>();
        public UserRepository()
        {
            //mocking data
             getAllFromDB();
            //update(new DataLayer.Models.UserDTO { Username = "admin", Password = "JNGuicG0l9amrBO1wj1MuA==", Title = "admin", ID = 1 });
        }

        public List<DataLayer.Models.UserDTO> getAll()
        {
            return inMemoryUsers;
        }

        public bool getAllFromDB()
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "SELECT * FROM Users";
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    DataLayer.Models.UserDTO user = new Models.UserDTO();
                    user.ID = (Int32)dr["ID"];
                    user.Username = dr["Username"].ToString();
                    user.Password = dr["Password"].ToString();
                    user.Title = dr["Title"].ToString();
                    inMemoryUsers.Add(user);
                }
                command.Connection.Close();
                return true;
            }
            catch { }

            return false;
        }
        private SqlCommand buildCommand(SqlCommand command, DataLayer.Models.UserDTO user)
        {
            command.Parameters.AddWithValue("ID", user.ID);
            command.Parameters.AddWithValue("Username", user.Username);
            command.Parameters.AddWithValue("Password", user.Password);
            command.Parameters.AddWithValue("Title", user.Title);
            return command;
        }

        public bool create(DataLayer.Models.UserDTO user)
        {
            try
            {
                SqlCommand command = buildCommand(new SqlCommand(), user);
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "INSERT INTO Users (Username,Password,Title) VALUES (@Username,@Password,@Title)";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //to be optimized
                inMemoryUsers = new List<Models.UserDTO>();
                getAllFromDB();
                return true;
            }
            catch { }

            return false;
        }
        public bool update(DataLayer.Models.UserDTO user)
        {
            try
            {
                SqlCommand command = buildCommand(new SqlCommand(), user);
                command.Connection = new SqlConnection("Data Source=198.38.83.33;Initial Catalog=geluvac_assignment1;User ID=geluvac_andreitudorica;Password=Andrei1234");
                command.Connection.Open();
                command.CommandText = "UPDATE Users Set Username= @Username,Password=@Password,Title=@Title where ID=@ID";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //update DTO
                //DataLayer.Models.UserDTO userOld = inMemoryUsers.Find(x => x.ID == user.ID);
                // userOld = user;
                //to be checked
                inMemoryUsers = new List<Models.UserDTO>();
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
                command.CommandText = "DELETE FROM Users where ID=@ID";
                command.ExecuteNonQuery();
                command.Connection.Close();
                //update si in DTO
                inMemoryUsers.Remove(inMemoryUsers.Find(x => x.ID == ID));
                return true;
            }
            catch { }

            return false;
        }
    }
}
