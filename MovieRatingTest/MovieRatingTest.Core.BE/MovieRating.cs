using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRatingTest.Core.BE
{
    public class MovieRating
    {
        public int Reviewer { get; }

        public int Movie { get; }

        public int Grade { get; }

        public DateTime Date { get; }

        public MovieRating(int reviewer, int movie, int grade, DateTime date)
        {
            Reviewer = reviewer;
            Movie = movie;
            Grade = grade;
            Date = date;
        }
    }
}
