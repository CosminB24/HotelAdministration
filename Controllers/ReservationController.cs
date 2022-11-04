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
    public class ReservationController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["api_con"].ConnectionString);
        ReservationViewModel rez = new ReservationViewModel();
        public List<ReservationViewModel> Get()
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetReservations", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            List<ReservationViewModel> FirstReservation = new List<ReservationViewModel>();
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    ReservationViewModel rez = new ReservationViewModel();
                    rez.PersId = Convert.ToInt32(Dt.Rows[i]["PersonID"]);
                    rez.RNumber = Convert.ToInt32(Dt.Rows[i]["RoomNumber"]);
                    rez.CheckIn = Dt.Rows[i]["CheckIn"].ToString();
                    rez.CheckOut = Dt.Rows[i]["CheckOut"].ToString();
                    rez.ReservationPrice = Convert.ToInt32(Dt.Rows[i]["ReservationPrice"]);
                    FirstReservation.Add(rez);
                }

            }
            if (FirstReservation.Count > 0)
            {
                return FirstReservation;
            }
            else
            {
                return null;
            }
        }
        public ReservationViewModel Get(int id)
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetOneReservation", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            Da.SelectCommand.Parameters.AddWithValue("@ReservationID", id);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            ReservationViewModel rez = new ReservationViewModel();
            if (Dt.Rows.Count > 0)
            {
                rez.ResId = Convert.ToInt32(Dt.Rows[0]["ReservationID"]);
                rez.PersId = Convert.ToInt32(Dt.Rows[0]["PersonID"]);
                rez.RNumber = Convert.ToInt32(Dt.Rows[0]["RoomNumber"]);
                rez.CheckIn = Dt.Rows[0]["CheckIn"].ToString();
                rez.CheckOut = Dt.Rows[0]["CheckOut"].ToString();
                rez.ReservationPrice = Convert.ToInt32(Dt.Rows[0]["ReservationPrice"]);

            }
            if (rez != null)
            {
                return rez;
            }
            else
            {
                return null;
            }
        }
        public string Post(ReservationViewModel rez)
        {
            string message = "";
            if (rez != null)
            {
                SqlCommand Scmd = new SqlCommand("AddReservation", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@PersonID", rez.PersId);
                Scmd.Parameters.AddWithValue("@RoomNumber", rez.RNumber);
                Scmd.Parameters.AddWithValue("@CheckIn", rez.CheckIn);
                Scmd.Parameters.AddWithValue("@CheckOut", rez.CheckOut);
                Scmd.Parameters.AddWithValue("@ReservationPrice", rez.ReservationPrice);

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
        public string Put(int id, ReservationViewModel rez)
        {
            string message = "";
            if (rez != null)
            {
                SqlCommand Scmd = new SqlCommand("UpdateReservation", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@ReservationID", rez.ResId);
                Scmd.Parameters.AddWithValue("@PersonID", rez.PersId);
                Scmd.Parameters.AddWithValue("@RoomNumber", rez.RNumber);
                Scmd.Parameters.AddWithValue("@CheckIn", rez.CheckIn);
                Scmd.Parameters.AddWithValue("@CheckOut", rez.CheckOut);
                Scmd.Parameters.AddWithValue("@ReservationPrice", rez.ReservationPrice);

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
            SqlCommand Scmd = new SqlCommand("DeleteReservation", con);
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