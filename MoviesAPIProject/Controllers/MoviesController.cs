using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Configuration;

namespace MoviesAPIProject.Controllers
{
    public class MoviesController : ApiController
    {
        /// <summary>
        /// serves the Movies to other clients via GET request
        /// </summary>
        /// <returns>
        /// Returns a status code and a List of Movie objects if successful
        /// </returns>
        [HttpGet]
        [ResponseType(typeof(Movie))]
        public IHttpActionResult GetMovies()
        {
            List<Movie> movies = new List<Movie>();

            try
            {
                //Establish a connection to SQL server via connection string indicated in the web.config with ADO.NET objects:
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["moviesDBConnection"].ConnectionString;

                //Pass a SQL query through our connection and set up a reader to parse through the data:
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.CommandText = "SELECT * from tblMovie";
                sqlCmd.Connection = connection;
                connection.Open();
                SqlDataReader reader = sqlCmd.ExecuteReader();

                System.Diagnostics.Debug.WriteLine(Environment.NewLine + "Successfully read data from database." + Environment.NewLine);

                //Create Movie objects with each row of data:
                while (reader.Read())
                {
                    Movie mov = new Movie();
                    mov.ID = Convert.ToInt32(reader.GetValue(0));
                    mov.title = reader.GetValue(1).ToString();
                    mov.rating = reader.GetValue(2).ToString();
                    mov.year = Convert.ToInt32(reader.GetValue(3).ToString());
                    movies.Add(mov);
                }
                connection.Close();
                return Ok(movies);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(Environment.NewLine + "Error connecting to database" + Environment.NewLine);
                return NotFound();
            }
        }
    }
}

