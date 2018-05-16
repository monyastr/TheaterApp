namespace TheaterApp.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TheaterApp.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TheaterApp.Models.Context context)
        {
            var theaters = new List<Theater>
            {
                new Theater { Name = "Appolo", Address = "Some Address 1" },
                new Theater { Name = "Daffi", Address = "Some Address 2" },
                new Theater { Name = "Pravda", Address = "Some Address 3" },
                new Theater { Name = "Other", Address = "Some Address 4" },

            };
            theaters.ForEach(t => context.Theaters.AddOrUpdate(n => n.Name, t));
            context.SaveChanges();

            var movies = new List<Movie>
            {
                new Movie { MovieName= "Avangers", Description = "SOme Descritption 1", Logo = new byte[0] },
                new Movie { MovieName= "Avangers2", Description = "SOme Descritption 2", Logo = new byte[0] },
                new Movie { MovieName= "Avangers3", Description = "SOme Descritption 3", Logo = new byte[0] },
            };
            movies.ForEach(m => context.Movies.AddOrUpdate(n => n.MovieName, m));
            context.SaveChanges();

            var rates = new List<Rate>
            {
                new Rate {
                    TheaterID = theaters.SingleOrDefault(t => t.Name == "Appolo").TheaterID,
                    MovieID = movies.SingleOrDefault(m => m.MovieName == "Avangers").MovieID,
                    TheaterName = "Appolo",
                    MovieName = "Avangers",
                    MovieRate = 5
                },
                 new Rate {
                    TheaterID = theaters.SingleOrDefault(t => t.Name == "Appolo").TheaterID,
                    MovieID = movies.SingleOrDefault(m => m.MovieName == "Avangers2").MovieID,
                    TheaterName = "Appolo",
                    MovieName = "Avangers2",
                    MovieRate = 8
                },
                 new Rate {
                    TheaterID = theaters.SingleOrDefault(t => t.Name == "Daffi").TheaterID,
                    MovieID = movies.SingleOrDefault(m => m.MovieName == "Avangers").MovieID,
                    TheaterName = "Daffi",
                    MovieName = "Avangers",
                    MovieRate = 1
                },
                  new Rate {
                    TheaterID = theaters.SingleOrDefault(t => t.Name == "Pravda").TheaterID,
                    MovieID = movies.SingleOrDefault(m => m.MovieName == "Avangers3").MovieID,
                    TheaterName = "Pravda",
                    MovieName = "Avangers3",
                    MovieRate = 7
                },
                   new Rate {
                    TheaterID = theaters.SingleOrDefault(t => t.Name == "Other").TheaterID,
                    MovieID = movies.SingleOrDefault(m => m.MovieName == "Avangers2").MovieID,
                    TheaterName = "Other",
                    MovieName = "Avangers2",
                    MovieRate = 2
                },
                    new Rate {
                    TheaterID = theaters.SingleOrDefault(t => t.Name == "Other").TheaterID,
                    MovieID = movies.SingleOrDefault(m => m.MovieName == "Avangers3").MovieID,
                    TheaterName = "Other",
                    MovieName = "Avangers3",
                    MovieRate = 9
                },
            };
            foreach (Rate r in rates)
            {
                var rateInDataBase = context.Rates.Where(
                    t => t.Theater.TheaterID == r.TheaterID && t.Movie.MovieID == r.MovieID).SingleOrDefault();
                if (rateInDataBase == null)
                {
                    context.Rates.Add(r);
                }
            }
            
        }
    }
}
