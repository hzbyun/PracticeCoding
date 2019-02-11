using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace PracticeCoding.Service
{
    public class WikipediaWordService
    {
        public static int GetTopicCount(string topic)
        {
            if (string.IsNullOrEmpty(topic)) return 0;

            var t = Task.Run(() => GetWikipediaSearchResult(topic));
            t.Wait();

            var textBody = JObject.Parse(t.Result).GetValue("parse").SelectToken("text").ToString();

            return GetCountOfWordInText(textBody, topic);
        }

        private static int GetCountOfWordInText(string wholeText, string word)
        {
            int count = 0;
            int i = 0;
            while ((i = wholeText.IndexOf(word, i, StringComparison.Ordinal)) != -1)
            {
                i += word.Length;
                count++;
            }

            return count;
        }

        private static async Task<string> GetWikipediaSearchResult(string topic)
        {
            var response = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(
                    string.Format("https://en.wikipedia.org/w/api.php?action=parse&section=0&prop=text&format=json&page={0}", topic));

                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }
            return response;
        }
    }
}
