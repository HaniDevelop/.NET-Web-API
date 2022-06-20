using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPIProject
{
    //Model for an entry in the SQL database
    public class Movie
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string rating { get; set; }
        public int year { get; set; }
    }
}