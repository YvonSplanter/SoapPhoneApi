using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using SoapApiPhone.Model;


namespace SoapApiPhone.DAO
{
    public static class PhoneDao
    {
        private static readonly MySqlConnection cnx = new MySqlConnection();

        public static void OpenConnection() {
            cnx.ConnectionString = "Server=127.0.0.1;Uid=root;Pwd=root;Database=catalogue;";
            cnx.Open();
        }

        public static CatalogPhone GetAll() {
            OpenConnection();
            var phones = new CatalogPhone();
            var cmd = new MySqlCommand();
            //DB request
            cmd.CommandText = "SELECT * FROM phone";
            cmd.Connection = cnx;
            //execute and read request
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                var phone = new Phone();
                phone.ID = dr.GetString("id");
                phone.Name = dr.GetString("name");
                phone.Price = dr.GetFloat("price");
                if(dr["image"] != DBNull.Value)
                    phone.Image = dr.GetString("image");
                if (dr["description"] != DBNull.Value)
                    phone.Description = dr.GetString("description");
                phones.Phones.Add(phone);
            }
            CloseConnection();

            return phones;
        }

        public static Phone GetById(string id) {
            OpenConnection();
            var phone = new Phone();
            var cmd = new MySqlCommand();
            // DB request
            cmd.Connection = cnx;
            cmd.CommandText = "SELECT * FROM phone WHERE id=@pid";
            cmd.Parameters.AddWithValue("@pid",id);
            cmd.Prepare();
            // execute and read DB request
            MySqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read()) {
                phone.ID = dr.GetString("id");
                phone.Name = dr.GetString("name");
                phone.Price = dr.GetFloat("price");
                if (dr["image"] != DBNull.Value)
                    phone.Image = dr.GetString("image");
                if (dr["description"] != DBNull.Value)
                    phone.Description = dr.GetString("description");
            }
            CloseConnection();
            return phone;
        }

        private static void CloseConnection() {
            cnx.Close();
        }
    }
}
