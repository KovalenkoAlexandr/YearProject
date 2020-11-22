using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Shared
{
    public class Users
    {
        private /*static*/ int id = -1;
        private /*static*/ string fio;
        private /*static*/ int id_role;

        internal /*static*/ int Id { get => id; set => id = value; }
        internal /*static*/ string Fio { get => fio; set => fio = value; }
        internal /*static*/ int Id_role { get => id_role; set => id_role = value; }

        internal Users()
        {
            Fio = "Гість";
            Id_role = 0;
        }

        internal /*static*/ void Logout()
        {
            Id_role = 0;
            Fio = "Гість";
        }

        internal /*static*/ bool IsValidUser(string name, string password)
        {
            bool validUser = false;
            try
            {
                using (var sqlConn = DBUtils.GetDBConnection())
                {
                    sqlConn.Open();
                    var sqlCmd = new MySqlCommand("AuthoriseUser", sqlConn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlCmd.Parameters.AddWithValue("_Username", name);
                    sqlCmd.Parameters.AddWithValue("_Password", password);
                    using (var dataReader = sqlCmd.ExecuteReader())
                    {
                        validUser = dataReader.Read();
                        if (validUser)
                        {
                            Id_role = (int)dataReader["ID_role"];
                            Fio = dataReader["fio"].ToString();
                            Id = (int)dataReader["ID_u"];
                        }
                    }

                }
                return validUser;
            }
            catch (Exception e)
            {
                MyMessages.ErrorMessage(e.Message);
                return validUser;
            }
        }

        internal void GetUser(string fio, int id, int role)
        {
            Fio = fio;
            Id = id;
            Id_role = role;
        }
    }
}