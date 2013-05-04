using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TTModels;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TT
{
    public class TTRESTService
    {
        readonly string baseUri = "http://localhost:15795/api";
        readonly string basePostUri = "http://localhost:15795";

        public IEnumerable<Posts> GetPosts()
        {
            string uri = baseUri + "/Posts/?start=1&limit=5";

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObjectAsync<List<Posts>>(response.Result).Result;
            }
        }

        public IEnumerable<Posts> GetPosts(int i, int j)
        {
            string uri = baseUri + "/Posts/?start=" + i + "&limit=" + j;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObjectAsync<List<Posts>>(response.Result).Result;
            }
        }

        public IEnumerable<Posts> GetPosts(int i)
        {
            string uri = baseUri + "/Posts/" + i;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObjectAsync<List<Posts>>(response.Result).Result;
            }
        }

        public UserProfile GetProfile(int i)
        {
            string uri = baseUri + "/Profile/" + i;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObjectAsync<UserProfile>(response.Result).Result;
            }
        }

        public PostUserCombined GetPostUser(int i)
        {
            string uri = baseUri + "/PostAndUser/" + i;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObjectAsync<PostUserCombined>(response.Result).Result;
            }
        }

        public IEnumerable<Comments> GetComments(int i)
        {
            string uri = baseUri + "/Comments/" + i;

            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObjectAsync<List<Comments>>(response.Result).Result;
            }
        }

        public void PostPost(Posts p)
        {
            string uri = baseUri + "/Posts/";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(basePostUri);
                httpClient.DefaultRequestHeaders.Accept.Add(
                       new MediaTypeWithQualityHeaderValue("application/json"));

                var response = httpClient.PostAsJsonAsync("api/Posts", p).Result;
            }
        }
    }
}