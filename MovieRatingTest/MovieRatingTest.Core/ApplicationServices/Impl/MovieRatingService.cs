using MovieRatingTest.Core.DomainServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MovieRatingTest.Core.ApplicationServices.Impl
{
    public class MovieRatingService : IMovieRatingService
    {
        private IMovieRatingRepository _repo;

        public MovieRatingService(IMovieRatingRepository repo)
        {
            _repo = repo;
        }

        public int GetNumberOfReviewsFromReviewer(int reviewer)
        {
            return _repo.GetAllMovieRatings().Where(m => m.Reviewer == reviewer).Count();
        }

        public double GetAverageRateFromReviewer(int reviewer)
        {
            var reviews = _repo.GetAllMovieRatings().Where(m => m.Reviewer == reviewer);
            double total = 0;
            foreach (var item in reviews)
            {
                total += item.Grade;
            }
            
            return total / reviews.Count();                     
        }

        public int GetNumberOfRatesByReviewer(int reviewer, int rate)
        {
            if (rate < 1 || rate > 5)
            {
                throw new InvalidDataException("Rate must be in the range 1-5");
            }

            return _repo.GetAllMovieRatings().Where(m => m.Reviewer == reviewer)
                .Where(m => m.Grade == rate).Count();
            
        }

        public int GetNumberOfReviews(int movie)
        {
            return _repo.GetAllMovieRatings().Where(m => m.Movie == movie).Count();
        }

        public double GetAverageRateOfMovie(int movie)
        {
            var movies = _repo.GetAllMovieRatings().Where(m => m.Movie == movie);
            double total = 0;
            foreach (var item in movies)
            {
                total += item.Grade;
            }
            return total / movies.Count();
        }

        public int GetNumberOfRates(int movie, int rate)
        {
            if (rate < 1 || rate > 5)
            {
                throw new InvalidDataException("Rate must be in the range 1-5");
            }

            return _repo.GetAllMovieRatings().Where(m => m.Movie == movie)
                .Where(m => m.Grade == rate).Count();
        }

        public List<int> GetMoviesWithHighestNumberOfTopRates()
        {
            var movie5 = _repo.GetAllMovieRatings()
                .Where(m => m.Grade == 5)
                .GroupBy(m => m.Movie)
                .Select(group => new
                {
                    Movie = group.Key,
                    MovieGrade5 = group.Count()
                });

            int max5 = movie5.Max(grp => grp.MovieGrade5);

            return movie5.Where(grp => grp.MovieGrade5 == max5)
                .Select(grp => grp.Movie)
                .ToList();
        }

        public List<int> GetMostProductiveReviewers()
        {
            var reviewers = _repo.GetAllMovieRatings()
                .GroupBy(m => m.Reviewer)
                .Select(group => new
                {
                    Reviewer = group.Key,
                    Reviews = group.Count()
                });

            int maxReviews = reviewers.Max(group => group.Reviews);

            return reviewers.Where(group => group.Reviews == maxReviews)
                .Select(group => group.Reviewer)
                .ToList();
        }

        public List<int> GetTopRatedMovies(int amount)
        {
            return _repo.GetAllMovieRatings()
                .GroupBy(m => m.Movie)
                .Select(group => new
                {
                    Movie = group.Key,
                    Rate = group.Average(group => group.Grade)
                })
                .OrderByDescending(group => group.Rate)
                .Take(amount)
                .Select(group => group.Movie)
                .ToList();
        }

        public List<int> GetTopMoviesByReviewer(int reviewer)
        {
            return _repo.GetAllMovieRatings()
                .Where(m => m.Reviewer == reviewer)
                .OrderByDescending(m => m.Grade)
                .ThenByDescending(m => m.Date)
                .Select(m => m.Movie)
                .ToList();
        }

        public List<int> GetReviewersByMovie(int movie)
        {
            return _repo.GetAllMovieRatings()
               .Where(m => m.Movie == movie)
               .OrderByDescending(m => m.Grade)
               .ThenByDescending(m => m.Date)
               .Select(m => m.Reviewer)
               .ToList();
        }

    }
}
