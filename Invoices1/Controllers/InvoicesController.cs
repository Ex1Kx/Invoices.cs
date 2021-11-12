using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Invoices1.Models;

namespace Invoices1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public InvoicesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select Id,Namei,Last_Name,Document_Id from 
                        Clients
                        select Id,Id_Client, Cod from Invoice
                        select Id, Id_Invoice, Descriptioni, Valuei from InvoicesDetails
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("InvoicesAppCon");
            MySqlDataReader myreader;
            using(MySqlConnection mycon=new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(MySqlCommand mySqlCommand=new MySqlCommand(query,mycon))
                {
                    myreader = mySqlCommand.ExecuteReader();
                    table.Load(myreader);

                    myreader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Clients clients)
        {
            string query = @"
                        insert into Clients 
                        (Id,Namei,Last_Name,Document_Id) 
                        values
                         (@Id,@Namei,@Last_Name,@Document_Id) ;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Id", clients.Id);
                    myCommand.Parameters.AddWithValue("@Namei", clients.Namei);
                    myCommand.Parameters.AddWithValue("@Last_Name", clients.Last_Name);
                    myCommand.Parameters.AddWithValue("@Document_Id", clients.Document_Id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
    }
}
