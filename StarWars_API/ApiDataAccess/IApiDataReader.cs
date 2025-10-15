using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StarWars_API.ApiDataAccess
{
    public interface IApiDataReader
    {
        Task<string> Read(string baseAddress, string requestUri);

    }
}
