using MovieRatingTest.Core.DomainServices;
using MovieRatingTest.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XUnitTestProject
{
    public class TestFixture : IDisposable
    {
        const string JSON_FILE_NAME = "../../../../ratings.json";

        public IMovieRatingRepository Repository { get; set; }
        public int ReviewerMostReviews { get; set; }
        public int MovieMostReviews { get; set; }

        public TestFixture()
        {
            Repository = new JsonFileRepository(JSON_FILE_NAME);

            ReviewerMostReviews = Repository.Ratings
                .GroupBy(r => r.Reviewer)
                .Select(grp => new
                {
                    reviewer = grp.Key,
                    reviews = grp.Count()
                })
                .OrderByDescending(grp => grp.reviews)
                .Select(grp => grp.reviewer)
                .FirstOrDefault();

            MovieMostReviews = Repository.Ratings
                .GroupBy(r => r.Movie)
                .Select(grp => new
                {
                    movie = grp.Key,
                    reviews = grp.Count()
                })
                .OrderByDescending(grp => grp.reviews)
                .Select(grp => grp.movie)
                .FirstOrDefault();
        }


        public void Dispose()
        {
            //necessary
        }
    }
}
