using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                List<bookes> bookList=new List<bookes>();
                bookList = booklist();
                return View("Index", bookList);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Index(bookes books)
        {
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                con.Open();
                string query = "Insert into bookes (Title, Author,ISBN,Publisher,YearIs) values(@title, @author , @isbn,@piblisher,@year)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@title", books.Title);
                cmd.Parameters.AddWithValue("@author", books.Author);
                cmd.Parameters.AddWithValue("@isbn", books.ISBN);
                cmd.Parameters.AddWithValue("@piblisher", books.Publisher);
                cmd.Parameters.AddWithValue("@year", books.YearIs);
                cmd.ExecuteNonQuery(); 
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult updateDetails(bookes books)
        {
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                con.Open();
                string query = "Update bookes SET Title = @title, Author=@author,ISBN=@isbn,Publisher=@piblisher,YearIs=@year where id = "+books.ID;
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@title", books.Title);
                cmd.Parameters.AddWithValue("@author", books.Author);
                cmd.Parameters.AddWithValue("@isbn", books.ISBN);
                cmd.Parameters.AddWithValue("@piblisher", books.Publisher);
                cmd.Parameters.AddWithValue("@year", books.YearIs);
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
        }
        public ActionResult delte(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                con.Open();
                string query = "Delete from bookes where id = "+id;
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.ExecuteNonQuery();
                List<bookes> bookList = new List<bookes>();
                bookList = booklist();
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult edit(int id)
        {
            List<bookes> books = new List<bookes>();

            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                MySqlCommand cmd = new MySqlCommand("select * from bookes where id="+id, con);
                con.Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    bookes b = new bookes();
                    b.ID = Convert.ToInt32(dataReader["ID"]);
                    b.Title = dataReader["Title"].ToString();
                    b.Author = dataReader["Author"].ToString();
                    b.ISBN = dataReader["ISBN"].ToString();
                    b.YearIs = dataReader["YearIs"].ToString();
                    b.Publisher = dataReader["Publisher"].ToString();

                    books.Add(b);
                }
            }
            return View("edit", books);
        }
        public ActionResult addNewBook()
        {

            return View();
        }
        public List<bookes> booklist()
        {
            List<bookes> books = new List<bookes>();

            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                MySqlCommand cmd = new MySqlCommand("select * from bookes", con);
                con.Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    bookes b = new bookes();
                    b.ID = Convert.ToInt32(dataReader["ID"]);
                    b.Title = dataReader["Title"].ToString();
                    b.Author = dataReader["Author"].ToString();
                    b.ISBN = dataReader["ISBN"].ToString();
                    b.YearIs = dataReader["YearIs"].ToString();
                    b.Publisher = dataReader["Publisher"].ToString();

                    books.Add(b);
                }
            }
            return books;
        }
    }
}