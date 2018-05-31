using System;
using System.Collections.Generic;
using System.Text;
using TMDBMobile.Core.Model;

namespace TMDBMobile.Core.PageModels
{
    public class MovieDetailsPageModel : PageModel
    {
        public string Title { get; set; }

        public Movie Movie { get; set; }

        public override void Init(object initData)
        {
            if (initData is Movie movie)
            {
                Movie = movie;

                Title = Movie.Title;
            }
        }
    }
}
