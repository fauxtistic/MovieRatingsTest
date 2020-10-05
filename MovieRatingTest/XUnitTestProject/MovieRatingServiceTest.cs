﻿using Moq;
using MovieRatingTest.Core.ApplicationServices;
using MovieRatingTest.Core.ApplicationServices.Impl;
using MovieRatingTest.Core.BE;
using MovieRatingTest.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class MovieRatingServiceTest
    {
        private List<MovieRating> ratings;
        private Mock<IMovieRatingRepository> repoMock;

        public MovieRatingServiceTest()
        {
            repoMock = new Mock<IMovieRatingRepository>();
            repoMock.Setup(x => x.GetAllMovieRatings()).Returns(() => ratings);
        }
        //1.On input N, what are the number of reviews from reviewer N?

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        public void GetNumberOfReviewsFromReviewer(int reviewer, int expected)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 3, DateTime.Now),
                new MovieRating(3, 2, 3, DateTime.Now),
                new MovieRating(4, 2, 3, DateTime.Now)
            };

            IMovieRatingService mrs = new MovieRatingService(repoMock.Object);

            // act
            int result = mrs.GetNumberOfReviewsFromReviewer(reviewer);

            // assert
            Assert.Equal(result, expected);
            repoMock.Verify(r => r.GetAllMovieRatings(), Times.Once); //sikrer at repository bruges (bliver omdirigeret til repomock)
        }

        [Theory]
        [InlineData(1, 0, 1)]
        [InlineData(2, 1, 2)]
        [InlineData(3, 2, 3)]
        public void GetNumberOfRatesByReviewer(int reviewer, int expected, int rate)
        {
            // arrange
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 2, DateTime.Now),
                new MovieRating(2, 1, 2, DateTime.Now),
                new MovieRating(3, 2, 3, DateTime.Now),
                new MovieRating(3, 2, 3, DateTime.Now),
            };
            IMovieRatingService mrs = new MovieRatingService(repoMock.Object);
            Assert.Equal(expected, mrs.GetNumberOfRatesByReviewer(reviewer, rate));

        }

        [Theory]
        [InlineData(1, 2.0)]
        [InlineData(2, 3.0)]
        [InlineData(3, 4.0)]
        public void GetAverageRateFromReviewer(int reviewer, double expected)
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 1, DateTime.Now),
                new MovieRating(1, 1, 2, DateTime.Now),
                new MovieRating(1, 1, 3, DateTime.Now),

                new MovieRating(2, 1, 2, DateTime.Now),
                new MovieRating(2, 1, 3, DateTime.Now),
                new MovieRating(2, 1, 4, DateTime.Now),

                new MovieRating(3, 1, 3, DateTime.Now),
                new MovieRating(3, 1, 4, DateTime.Now),
                new MovieRating(3, 1, 5, DateTime.Now),

                new MovieRating(4, 1, 5, DateTime.Now)
            };

            IMovieRatingService mrs = new MovieRatingService(repoMock.Object);

            Assert.Equal(expected, mrs.GetAverageRateFromReviewer(reviewer));
        }
        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        [InlineData(3, 4)]
        public void GetNumberOfReviews(int movie, int exspected)

        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 2, 1, DateTime.Now),
                new MovieRating(1, 3, 2, DateTime.Now),
                new MovieRating(1, 3, 1, DateTime.Now),
                new MovieRating(1, 3, 2, DateTime.Now),
                new MovieRating(1, 3, 3, DateTime.Now)
            };
            IMovieRatingService mrs = new MovieRatingService(repoMock.Object);
            Assert.Equal(exspected, mrs.GetNumberOfReviews(movie));
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(2, 1, 1)]
        [InlineData(3, 4, 0)]
        public void GetNumberOfRates(int movie, int rate, int exspected)
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 2, 1, DateTime.Now),
                new MovieRating(1, 3, 2, DateTime.Now),
                new MovieRating(1, 3, 1, DateTime.Now),
                new MovieRating(1, 3, 2, DateTime.Now),
                new MovieRating(1, 3, 3, DateTime.Now)
            };
            IMovieRatingService mrs = new MovieRatingService(repoMock.Object);
            Assert.Equal(exspected, mrs.GetNumberOfRates(movie, rate));
        }




        [Theory]
        [InlineData(1, 2.0)]
        [InlineData(2, 3.0)]
        [InlineData(3, 4.0)]
        public void GetAverageRateOfMovie(int movie, double exspected)
        {
            {
                ratings = new List<MovieRating>()
                {
                    new MovieRating(1, 1, 1, DateTime.Now),
                    new MovieRating(1, 1, 2, DateTime.Now),
                    new MovieRating(1, 1, 3, DateTime.Now),

                    new MovieRating(2, 2, 2, DateTime.Now),
                    new MovieRating(2, 2, 3, DateTime.Now),
                    new MovieRating(2, 2, 4, DateTime.Now),

                    new MovieRating(3, 3, 3, DateTime.Now),
                    new MovieRating(3, 3, 4, DateTime.Now),
                    new MovieRating(3, 3, 5, DateTime.Now)


                };
                IMovieRatingService mrs = new MovieRatingService(repoMock.Object);
                Assert.Equal(exspected, mrs.GetAverageRateOfMovie(movie));
            }
        }

        [Fact]
        public void GetMoviesWithHighestNumberOfTopRates()
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 5, DateTime.Now),
                new MovieRating(1, 2, 5, DateTime.Now),

                new MovieRating(2, 1, 5, DateTime.Now),
                new MovieRating(2, 2, 4, DateTime.Now),

                new MovieRating(2, 3, 5, DateTime.Now),
                new MovieRating(3, 3, 5, DateTime.Now)
            };

            IMovieRatingService mrs = new MovieRatingService(repoMock.Object);

            List<int> expected = new List<int>() { 1, 3 };
            List<int> result = mrs.GetMoviesWithHighestNumberOfTopRates();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetMostProductiveReviewers()
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 1, DateTime.Now),
                new MovieRating(1, 2, 1, DateTime.Now),

                new MovieRating(2, 1, 1, DateTime.Now),
                new MovieRating(2, 2, 1, DateTime.Now),
                new MovieRating(2, 3, 1, DateTime.Now),

                new MovieRating(3, 1, 1, DateTime.Now),
                new MovieRating(3, 2, 1, DateTime.Now),
                new MovieRating(3, 3, 1, DateTime.Now),
            };

            IMovieRatingService mrs = new MovieRatingService(repoMock.Object);

            List<int> expected = new List<int>() { 2, 3 };
            List<int> result = mrs.GetMostProductiveReviewers();

            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(5, 1)]
        [InlineData(4, 2)]
        [InlineData(3, 3)]
        [InlineData(2, 4)]
        [InlineData(1, 5)]
        public void GetTopRatedMovies(int amount, int expectedLowestMovie)
        {
            ratings = new List<MovieRating>()
            {
                new MovieRating(1, 1, 1, DateTime.Now),
                new MovieRating(1, 2, 2, DateTime.Now),
                new MovieRating(1, 3, 3, DateTime.Now),
                new MovieRating(1, 4, 4, DateTime.Now),
                new MovieRating(1, 5, 5, DateTime.Now),

                new MovieRating(2, 1, 1, DateTime.Now),
                new MovieRating(2, 2, 2, DateTime.Now),
                new MovieRating(2, 3, 3, DateTime.Now),
                new MovieRating(2, 4, 4, DateTime.Now),
                new MovieRating(2, 5, 5, DateTime.Now),

                new MovieRating(3, 1, 1, DateTime.Now),
                new MovieRating(3, 2, 2, DateTime.Now),
                new MovieRating(3, 3, 3, DateTime.Now),
                new MovieRating(3, 4, 4, DateTime.Now),
                new MovieRating(3, 5, 5, DateTime.Now),
            };

            IMovieRatingService mrs = new MovieRatingService(repoMock.Object);

            List<int> results = mrs.GetTopRatedMovies(amount);
            Assert.Equal(expectedLowestMovie, results[amount - 1]);
        }

    }
}
