using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using SampleWebServices.Models;
using Dapper;

namespace SampleWebServices.DAL
{
    public class RestaurantDAL
    {
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }

        public IEnumerable<Restaurant> GetAll()
        {
            string strSql = @"select * from Restaurants order by NamaRestaurant asc";
            using(MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                return conn.Query<Restaurant>(strSql);
            }
        }

        public void Insert(Restaurant resto)
        {
            string strSql = @"insert into Restaurants(NamaRestaurant) values(@NamaRestaurant)";
            using(MySqlConnection conn = new MySqlConnection(GetConnectionString()))
            {
                try
                {
                    var param = new { NamaRestaurant = resto.NamaRestaurant };
                    conn.Execute(strSql, param);
                }
                catch (MySqlException mysqlEx)
                {
                    throw new Exception(mysqlEx.Message);
                }
            }
        }
    }
}