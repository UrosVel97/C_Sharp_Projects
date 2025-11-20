using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ChuckNorisJokes.DataAccess
{
    public class JokesApiDataReader : IJokesApiDataReader
    {
        private readonly HttpClient _httpClient = new HttpClient();


        public async Task<string> ReadAsync(int page, int quotesPerPage, string category)
        {
            var endpoint = $"https://api.chucknorris.io/jokes/random?category={category}";

            List<Task<HttpResponseMessage>> tasks = new List<Task<HttpResponseMessage>>(page * quotesPerPage);
            List<Task<string>> responses = new List<Task<string>>();

            StringBuilder result = new StringBuilder();


            await Task.Run(() =>
            {
                for (int i = 0; i < (page * quotesPerPage); i++)
                {
                    tasks.Add(_httpClient.GetAsync(endpoint));
                }
            });

            await Task.WhenAll(tasks);

            Console.WriteLine("\nAll requests completed. Processing responses...");

            foreach (var task in tasks)
            {
                responses.Add(task.Result.Content.ReadAsStringAsync());
            }


            await Task.WhenAll(responses);

            Console.WriteLine("\nAll responses read. Compiling results...");

            result.Append($"All {page * quotesPerPage} fetched:" + Environment.NewLine);
            int i = 0;

            foreach (var res in responses)
            {
                i++;

                result.Append($"Chuck Norris joke number {i}: " + Environment.NewLine + ExtractJokeValue(res.Result) + Environment.NewLine + Environment.NewLine);
            }

            Console.WriteLine("\nData compilation completed.");

            return result.ToString();
        }



        public async Task<List<string>> ReadAllCategories()
        {
            var endpoint = $"https://api.chucknorris.io/jokes/categories";

            HttpResponseMessage response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var categories = await response.Content.ReadAsStringAsync();

            return categories.Split(new char[] { '[', ']', '"', ' ', ',' },
                StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        private string ExtractJokeValue(string jsonResponse)
        {
            try
            {
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                if (doc.RootElement.TryGetProperty("value", out JsonElement valueElement))
                {
                    return valueElement.GetString() ?? jsonResponse;
                }
            }
            catch (JsonException)
            {
                // If parsing fails, return the original response
            }
            return jsonResponse;
        }
    }
}
