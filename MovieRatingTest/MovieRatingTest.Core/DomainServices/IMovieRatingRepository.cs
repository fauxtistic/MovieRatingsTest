﻿using MovieRatingTest.Core.BE;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRatingTest.Core.DomainServices
{
    public interface IMovieRatingRepository
    {
        public List<MovieRating> GetAllMovieRatings();        
    }
}
