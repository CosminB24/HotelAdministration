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
    public class PersonController : ApiController
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["api_con"].ConnectionString);
        PersonViewModel pers = new PersonViewModel();
        public List<PersonViewModel> Get()
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetPerson", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            List<PersonViewModel> FirstPerson = new List<PersonViewModel>();
            if (Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    PersonViewModel pers = new PersonViewModel();
                    pers.LastName = Dt.Rows[i]["LastName"].ToString();
                    pers.FirstName = Dt.Rows[i]["FirstName"].ToString();
                    pers.Email = Dt.Rows[i]["Email"].ToString();
                    pers.PhoneNumber = Convert.ToInt32(Dt.Rows[i]["PhoneNumber"]);
                    pers.IsAdmin = Convert.ToInt32(Dt.Rows[i]["IsAdmin"]);
                    FirstPerson.Add(pers);
                }

            }
            if (FirstPerson.Count > 0)
            {
                return FirstPerson;
            }
            else
            {
                return null;
            }
        }
        public PersonViewModel Get(int id)
        {
            SqlDataAdapter Da = new SqlDataAdapter("GetOnePerson", con);
            Da.SelectCommand.CommandType = CommandType.StoredProcedure;
            Da.SelectCommand.Parameters.AddWithValue("@PersonID", id);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            PersonViewModel pers = new PersonViewModel();
            if (Dt.Rows.Count > 0)
            {
                pers.PersonId = Convert.ToInt32(Dt.Rows[0]["PersonID"]);
                pers.LastName = Dt.Rows[0]["LastName"].ToString();
                pers.FirstName = Dt.Rows[0]["FirstName"].ToString();
                pers.Email = Dt.Rows[0]["Email"].ToString();
                pers.PhoneNumber = Convert.ToInt32(Dt.Rows[0]["PhoneNumber"]);
                pers.IsAdmin = Convert.ToInt32(Dt.Rows[0]["IsAdmin"]);

            }
            if (pers != null)
            {
                return pers;
            }
            else
            {
                return null;
            }
        }
        public string Post(PersonViewModel pers)
        {
            string message = "";
            if (pers != null)
            {
                SqlCommand Scmd = new SqlCommand("Add_Person", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@LastName", pers.LastName);
                Scmd.Parameters.AddWithValue("@FirstName", pers.FirstName);
                Scmd.Parameters.AddWithValue("@Email", pers.Email);
                Scmd.Parameters.AddWithValue("@PhoneNumber", pers.PhoneNumber);
                Scmd.Parameters.AddWithValue("IsAdmin", pers.IsAdmin);

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
        public string Put(int id, PersonViewModel pers)
        {
            string message = "";
            if (pers != null)
            {
                SqlCommand Scmd = new SqlCommand("Update_Person", con);
                Scmd.CommandType = CommandType.StoredProcedure;
                Scmd.Parameters.AddWithValue("@PersonID", pers.PersonId);
                Scmd.Parameters.AddWithValue("@LastName", pers.LastName);
                Scmd.Parameters.AddWithValue("@FirstName", pers.FirstName);
                Scmd.Parameters.AddWithValue("@Email", pers.Email);
                Scmd.Parameters.AddWithValue("@PhoneNumber", pers.PhoneNumber);
                Scmd.Parameters.AddWithValue("@IsAdmin", pers.IsAdmin);

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
            SqlCommand Scmd = new SqlCommand("DeletePerson", con);
            Scmd.CommandType = CommandType.StoredProcedure;
            Scmd.Parameters.AddWithValue("@PersonID", id);
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