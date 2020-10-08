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
    public class MovieRatingServicePerformanceTest : IClassFixture<TestFixture>
    {        
        private static IMovieRatingRepository _repo;
        private int _reviewerMostReviews;
        private int _movieMostReviews;
        const int MAX_SECONDS = 4;
        

        public MovieRatingServicePerformanceTest(TestFixture data)
        {
            _repo = data.Repository;
            _reviewerMostReviews = data.ReviewerMostReviews;
            _movieMostReviews = data.MovieMostReviews;
        }

        private double TimeInSeconds(Action ac)
        {
            Stopwatch sw = Stopwatch.StartNew();
            ac.Invoke();
            sw.Stop();
            return sw.ElapsedMilliseconds / 1000d;
        }

        [Fact]
        public void GetNumberOfReviewsFromReviewer()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                int result = mrs.GetNumberOfReviewsFromReviewer(_reviewerMostReviews);
            });            
            
            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetNumberOfRatesByReviewer()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);
            int rate = 5;

            double seconds = TimeInSeconds(() =>
            {
                int result = mrs.GetNumberOfRatesByReviewer(_reviewerMostReviews, rate);
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetAverageRateFromReviewer()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                double result = mrs.GetAverageRateFromReviewer(_reviewerMostReviews);
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetNumberOfReviews()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                double result = mrs.GetNumberOfReviews(_reviewerMostReviews);
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetAverageRateOfMovie()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                double result = mrs.GetAverageRateOfMovie(_movieMostReviews);
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetNumberOfRates()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);
            int rate = 1;

            double seconds = TimeInSeconds(() =>
            {
                int result = mrs.GetNumberOfRates(_movieMostReviews, rate);
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                List<int> result = mrs.GetMoviesWithHighestNumberOfTopRates();
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetMostProductiveReviewers()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                List<int> result = mrs.GetMostProductiveReviewers();
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        public void GetTopRatedMovies(int amount)
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                List<int> result = mrs.GetTopRatedMovies(amount);
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetTopMoviesByReviewer()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                List<int> result = mrs.GetTopMoviesByReviewer(_reviewerMostReviews);
            });

            Assert.True(seconds <= MAX_SECONDS);
        }

        [Fact]
        public void GetReviewersByMovie()
        {
            MovieRatingService mrs = new MovieRatingService(_repo);

            double seconds = TimeInSeconds(() =>
            {
                List<int> result = mrs.GetReviewersByMovie(_movieMostReviews);
            });

            Assert.True(seconds <= MAX_SECONDS);
        }
    }
}
