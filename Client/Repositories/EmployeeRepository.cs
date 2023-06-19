using API.Models;
using API.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories
{
    public class EmployeeRepository
    {
        private readonly string request;
        private readonly HttpClient httpClient;

        public EmployeeRepository(string request = "Employee/")
        {
            this.request = request;
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7062/api/")
            };
        }
        public async Task<ResponseDataVM<List<Employee>>> Get()
        {
            ResponseDataVM<List<Employee>> entityVM = null;
            using (var response = await httpClient.GetAsync(request))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<List<Employee>>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseDataVM<Employee>> Get(string id)
        {
            ResponseDataVM<Employee> entity = null;

            using (var response = await httpClient.GetAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entity = JsonConvert.DeserializeObject<ResponseDataVM<Employee>>(apiResponse);
            }
            return entity;
        }

        public async Task<ResponseDataVM<Employee>> Post(Employee employee)
        {
            ResponseDataVM<Employee> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PostAsync(request, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<Employee>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseDataVM<Employee>> Put(Employee employee)
        {
            ResponseDataVM<Employee> entityVM = null;
            StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync(request, content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<Employee>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseDataVM<Employee>> Delete(string id)
        {
            ResponseDataVM<Employee> entityVM = null;

            using (var response = await httpClient.DeleteAsync(request + id))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseDataVM<Employee>>(apiResponse);
            }
            return entityVM;
        }
    }
}
