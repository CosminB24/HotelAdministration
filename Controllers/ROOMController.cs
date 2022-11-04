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
    public class ROOMController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["api_con"].ConnectionString);
        ROOMViewModel room = new ROOMViewModel();
       public List<ROOMViewModel> Get()
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetRooms", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            List<ROOMViewModel> FirstRoom = new List<ROOMViewModel>();
            if (Dt.Rows.Count > 0)
            {
                for(int i = 0;i < Dt.Rows.Count;i++)
                    {
                    ROOMViewModel room = new ROOMViewModel();
                    room.RoomId = Convert.ToInt32(Dt.Rows[i]["RoomID"]);
                    room.RoomNumber = Convert.ToInt32(Dt.Rows[i]["RoomNumber"]);
                    room.RoomPrice = Convert.ToInt32(Dt.Rows[i]["RoomPrice"]);
                    FirstRoom.Add(room);
                }
                
            }
            if(FirstRoom.Count > 0)
            {
                return FirstRoom;
            }
            else
            {
                return null;
            }
        }

        // GET api/values/5
        public ROOMViewModel Get(int id)
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetOneRoom", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            Da.SelectCommand.Parameters.AddWithValue("@RoomID", id);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            ROOMViewModel room = new ROOMViewModel();
            if (Dt.Rows.Count > 0)
            {
                    room.RoomId = Convert.ToInt32(Dt.Rows[0]["RoomID"]);
                    room.RoomNumber = Convert.ToInt32(Dt.Rows[0]["RoomNumber"]);
                    room.RoomPrice = Convert.ToInt32(Dt.Rows[0]["RoomPrice"]);

            }
            if (room != null)
            {
                return room;
            }
            else
            {
                return null;
            }
        }

        // POST api/values
        public string Post(ROOMViewModel room)
        {
            string message = "";
            if (room != null)
            {
                SqlCommand Scmd = new SqlCommand("AddRoom", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                Scmd.Parameters.AddWithValue("@RoomPrice", room.RoomPrice);

                con.Open();
                int i = Scmd.ExecuteNonQuery();
                con.Close();

                if(i > 0)
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

        // PUT api/values/5
        public string Put(int id, ROOMViewModel room)
        {
            string message = "";
            if (room != null)
            {
                SqlCommand Scmd = new SqlCommand("UpdateRoom", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@RoomID", room.RoomId);
                Scmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                Scmd.Parameters.AddWithValue("@RoomPrice", room.RoomPrice);

                con.Open();
                int i = Scmd.ExecuteNonQuery();
                con.Close();

                if (i > 0)
                {
                    message = "Data updated succesfully!";
                }
                else
                {
                    message = "No data was updated!";
                }
            }
            return message;
        }

        // DELETE api/values/5
        public string Delete(int id)
        {
            string message = "";
            SqlCommand Scmd = new SqlCommand("Delete_Room", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@RoomID", id);
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
