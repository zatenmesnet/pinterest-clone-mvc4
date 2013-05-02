using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TTModels;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TT
{
    public class TTRESTService
    {
        readonly string baseUri = "http://localhost:14667/api";

        public IEnumerable<Posts> GetPosts()
        {
            string uri = baseUri + "/Posts";

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
    }
}