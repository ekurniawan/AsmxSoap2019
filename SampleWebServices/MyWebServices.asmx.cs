using SampleWebServices.DAL;
using SampleWebServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web.Services.Protocols;

namespace SampleWebServices
{
    /// <summary>
    /// Summary description for MyWebServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MyWebServices : System.Web.Services.WebService
    {
        private RestaurantDAL _restaurantDAL;
        public MyWebServices()
        {
            _restaurantDAL = new RestaurantDAL();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Restaurant> GetAllRestaurant()
        {
            return _restaurantDAL.GetAll().ToList();

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAllRestoJson()
        {
            return new JavaScriptSerializer().Serialize(_restaurantDAL.GetAll());
        }


        [WebMethod]
        public void InsertRestaurant(string namaRestaurant)
        {
            try
            {
                var resto = new Restaurant { NamaRestaurant = namaRestaurant };
                _restaurantDAL.Insert(resto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public AuthHeader Credentials;

        [SoapHeader("Credentials")]
        [WebMethod]
        public string SecurityWebService()
        {

            if (Credentials.UserName.ToLower() != "Erick" ||
            Credentials.Password.ToLower() != "Erick")
            {
                throw new SoapException("Unauthorized",
                SoapException.ClientFaultCode);
            }
            else
            {
                return "This berhasil mengakses Web service";
            }
        }

        public class AuthHeader : SoapHeader
        {
            public string UserName;
            public string Password;
        }
    }
}
