using API.Models;
using API.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories
{
    public class UniversityRepository
    {
        private readonly string request;
        private readonly HttpClient httpClient;

        public UniversityRepository(string request = "University/")
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7062/api/")
            };
        }

        public async Task<ResponseDataVM<List<University>>> Get()
        {
            ResponseDataVM<List<University>> entityVM = null;
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<List<University>>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseDataVM<University>> Post(University university)
        {
            ResponseDataVM<University> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(university), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync(request, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<University>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseDataVM<University>> Get(int id)
        {
            ResponseDataVM<University> entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseDataVM<University>>(apiResponse);
            }
            return entity;
        }

        public async Task<ResponseDataVM<University>> Put(int id, University university)
        {
            ResponseDataVM<University> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(university), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync(request, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<University>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseDataVM<University>> Delete(int id)
        {
            ResponseDataVM<University> entityVM = null;

            using (var response = await httpClient.DeleteAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<University>>(apiResponse);
            }
            return entityVM;
        }
    }
}