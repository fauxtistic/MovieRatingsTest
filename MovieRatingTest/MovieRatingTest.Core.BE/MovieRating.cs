using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRatingTest.Core.BE
{
    public class MovieRating
    {
        public int Reviewer { get; set; }

        public int Movie { get; set; }

        public int Grade { get; set; }

        public DateTime Date { get; set; }

        public MovieRating()
        {

        }

        public MovieRating(int reviewer, int movie, int grade, DateTime date)
        {
            Reviewer = reviewer;
            Movie = movie;
            Grade = grade;
            Date = date;
        }

        public override string ToString()
        {
            return $"Reviewer: {Reviewer, 10}, Movie: {Movie, 10}, Grade: {Grade}, Date: {Date.Date}";
        }
    }
}
