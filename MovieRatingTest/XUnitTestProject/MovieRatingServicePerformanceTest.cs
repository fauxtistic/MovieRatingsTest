using MovieRatingTest.Core.ApplicationServices.Impl;
using MovieRatingTest.Core.DomainServices;
using MovieRatingTest.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingServicePerformanceTest
    {        
        private IMovieRatingRepository repo;
        private string filename = "..\\MovieRatingTest.Infrastructure\\ratings.json";

        public MovieRatingServicePerformanceTest()
        {
            repo = new JsonFileRepository(filename);
        }

        [Fact]
        public void GetNumberOfReviewsFromReviewer()
        {
            MovieRatingService mrs = new MovieRatingService(repo);

            Stopwatch sw = Stopwatch.StartNew();
            int result = mrs.GetNumberOfReviewsFromReviewer(1);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds <= 4000);
        }

    }
}
