using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new WeatherData { name = name };
                db.WeatherDatas.Add(blog);
                db.SaveChanges();

                // Display all Blogs from the database
                var query = from b in db.WeatherDatas
                            orderby b.name
                            select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }

    public class Coord
    {
        [Key]
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Main
    {
        [Key]
        public double temp { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        [Key]
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Sys
    {
        [Key]
        public string country { get; set; }
    }

    public class Clouds
    {
        [Key]
        public int all { get; set; }
    }

    public class Weather
    {
        [Key]
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

    }

    public class WeatherData
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public Main main { get; set; }
        public double dt { get; set; }
        public Wind wind { get; set; }
        public Sys sys { get; set; }
        public Clouds clouds { get; set; }
        public virtual List<Weather> weather { get; set; }
    }

    public class BloggingContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<WeatherData> WeatherDatas { get; set; }

        public DbSet<Clouds> Clouds { get; set; }

        public DbSet<Sys> Sys { get; set; }

        public DbSet<Wind> Winds { get; set; }

    }
}
