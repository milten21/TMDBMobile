using System;
using System.Collections.Generic;
using System.Text;

namespace TMDBMobile.Core.Services
{
    public interface IConfigurationService
    {
        string ApiKey { get; }
        string ApiUrl { get; }
        string ImagesPath { get; }
    }
}
