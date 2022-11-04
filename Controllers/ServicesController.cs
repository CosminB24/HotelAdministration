using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Configuration;
using HotelAdministration.Models;


namespace HotelAdministration.Controllers
{
    public class ServicesController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["api_con"].ConnectionString);
        ServicesViewModel serv = new ServicesViewModel();
        public List<ServicesViewModel> Get()
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetServices", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            List<ServicesViewModel> FirstService = new List<ServicesViewModel>();
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    ServicesViewModel serv = new ServicesViewModel();
                    serv.ServiceName = Dt.Rows[i]["ServiceName"].ToString();
                    serv.ServicePrice = Convert.ToInt32(Dt.Rows[i]["ServicePrice"]);
                    FirstService.Add(serv);
                }

            }
            if (FirstService.Count > 0)
            {
                return FirstService;
            }
            else
            {
                return null;
            }
        }
        public ServicesViewModel Get(int id)
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetOneService", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            Da.SelectCommand.Parameters.AddWithValue("@ServiceID", id);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            ServicesViewModel serv = new ServicesViewModel();
            if (Dt.Rows.Count > 0)
            {
                serv.ServiceId = Convert.ToInt32(Dt.Rows[0]["ServiceID"]);
                serv.ServiceName = Dt.Rows[0]["ServiceName"].ToString();
                serv.ServicePrice = Convert.ToInt32(Dt.Rows[0]["ServicePrice"]);

            }
            if (serv != null)
            {
                return serv;
            }
            else
            {
                return null;
            }
        }
        public string Post(ServicesViewModel serv)
        {
            string message = "";
            if (serv != null)
            {
                SqlCommand Scmd = new SqlCommand("AddService", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@ServiceName", serv.ServiceName);
                Scmd.Parameters.AddWithValue("@ServicePrice", serv.ServicePrice);

                con.Open();
                int i = Scmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    message = "Data added succesfully!";
                }
                else
                {
                    message = "No data was provided!";
                }
            }
            return message;
        }
        public string Put(int id, ServicesViewModel serv)
        {
            string message = "";
            if (serv != null)
            {
                SqlCommand Scmd = new SqlCommand("UpdateService", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@ServiceID", serv.ServiceId);
                Scmd.Parameters.AddWithValue("@ServiceName", serv.ServiceName);
                Scmd.Parameters.AddWithValue("@ServicePrice", serv.ServicePrice);

                con.Open();
                int i = Scmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    message = "Data added succesfully!";
                }
                else
                {
                    message = "No data was provided!";
                }
            }
            return message;
        }
        public string Delete(int id)
        {
            string message = "";
            SqlCommand Scmd = new SqlCommand("DeleteService", con);
            Scmd.CommandType = CommandType.StoredProcedure;
            Scmd.Parameters.AddWithValue("@ServiceID", id);
            con.Open();
            int i = Scmd.ExecuteNonQuery();
            con.Close();

            if (i > 0)
            {
                message = "Data deleted succesfully!";
            }
            else
            {
                message = "No data was deleted!";
            }
            return message;
        }
    }
}