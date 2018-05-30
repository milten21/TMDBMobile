using System;
using System.Collections.Generic;
using System.Text;

namespace TMDBMobile.Core.Services
{
    public class ReleaseConfigurationService: IConfigurationService
    {
        public string ApiKey => "176e4083ff5a86620b723c2db527bada";
        public string ApiUrl => "https://api.themoviedb.org/3";
        public string ImagesPath => "https://image.tmdb.org/t/p/w500";
    }
}
