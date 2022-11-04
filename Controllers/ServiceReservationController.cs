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
    public class ServiceReservationController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["api_con"].ConnectionString);
        ServiceReservationViewModel res = new ServiceReservationViewModel();
        public List<ServiceReservationViewModel> Get()
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetServiceRes", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            List<ServiceReservationViewModel> FirstServRes = new List<ServiceReservationViewModel>();
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    ServiceReservationViewModel res = new ServiceReservationViewModel();
                    res.ServiceId = Convert.ToInt32(Dt.Rows[i]["ServiceID"]);
                    res.ServiceReservationPrice = Convert.ToInt32(Dt.Rows[i]["ServiceReservationPrice"]);
                    FirstServRes.Add(res);
                }

            }
            if (FirstServRes.Count > 0)
            {
                return FirstServRes;
            }
            else
            {
                return null;
            }
        }
        public ServiceReservationViewModel Get(int id)
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetOneServiceRes", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            Da.SelectCommand.Parameters.AddWithValue("@ReservationID", id);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            ServiceReservationViewModel res = new ServiceReservationViewModel();
            if (Dt.Rows.Count > 0)
            {
                res.ServiceId = Convert.ToInt32(Dt.Rows[0]["ServiceID"]);
                res.ServiceReservationPrice = Convert.ToInt32(Dt.Rows[0]["ServiceReservationPrice"]);

            }
            if (res != null)
            {
                return res;
            }
            else
            {
                return null;
            }
        }
        public string Post(ServiceReservationViewModel res)
        {
            string message = "";
            if (res != null)
            {
                SqlCommand Scmd = new SqlCommand("AddServiceRes", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@ServiceID", res.ServiceId);
                Scmd.Parameters.AddWithValue("@ServiceReservationPrice", res.ServiceReservationPrice);

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
        public string Put(int id, ServiceReservationViewModel res)
        {
            string message = "";
            if (res != null)
            {
                SqlCommand Scmd = new SqlCommand("UpdateServiceRes", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@ReservationID", res.ServiceReservationId);
                Scmd.Parameters.AddWithValue("@ServiceID", res.ServiceId);
                Scmd.Parameters.AddWithValue("@ServiceReservationPrice", res.ServiceReservationPrice);

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
            SqlCommand Scmd = new SqlCommand("DeleteServiceRes", con);
            Scmd.CommandType = CommandType.StoredProcedure;
            Scmd.Parameters.AddWithValue("@ReservationID", id);
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