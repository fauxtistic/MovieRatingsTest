using Moq;
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
    }
}
