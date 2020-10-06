using MovieRatingTest.Core.BE;
using MovieRatingTest.Core.DomainServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MovieRatingTest.Infrastructure
{
    public class JsonFileRepository : IMovieRatingRepository
    {        
        public List<MovieRating> Ratings { get; private set; }

        public JsonFileRepository(string filename)        {
            
            Ratings = ReadAllMovieRatings(filename);
        }

        public List<MovieRating> ReadAllMovieRatings(string filename)
        {
            List<MovieRating> ratings = new List<MovieRating>();
            //JsonConvert.DeserializeObject<MovieRating[]>(File.ReadAllText(filename)).ToList(); //slower as it tries to figure out how movierating is configured in runtime, but on the other hand can be made generic
            using (StreamReader sr = new StreamReader(filename))
            using (JsonTextReader reader = new JsonTextReader(sr))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        MovieRating m = GetOneMovieRating(reader);
                        ratings.Add(m);
                    }
                }
            }

            return ratings;
        }

        private MovieRating GetOneMovieRating(JsonReader reader)
        {
            MovieRating rating = new MovieRating();
            reader.Read();
            rating.Reviewer = (int)reader.ReadAsInt32();
            reader.Read();
            rating.Movie = (int)reader.ReadAsInt32();
            reader.Read();
            rating.Grade = (int)reader.ReadAsInt32();

            return rating;

        }
    }
}
