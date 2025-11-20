using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckNorisJokes.DataAccess
{
    public interface IJokesApiDataReader : IDisposable
    {
        Task<string> ReadAsync(
            int numberOfPages, 
            int jokesPerPage, 
            string category);

        Task<List<string>> ReadAllCategories();
    }
}
