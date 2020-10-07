using MovieRatingTest.Core.ApplicationServices.Impl;
using MovieRatingTest.Core.DomainServices;
using MovieRatingTest.Infrastructure;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MovieRatingTest.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filepath = "../../../../ratings.json";            
            Stopwatch sw = Stopwatch.StartNew();
            IMovieRatingRepository repo = new JsonFileRepository(filepath);
            MovieRatingService service = new MovieRatingService(repo);
            sw.Stop();
            Console.WriteLine("Elapsed milliseconds: " + sw.ElapsedMilliseconds);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(repo.Ratings[i]);
            }

        
        }
    }
}


