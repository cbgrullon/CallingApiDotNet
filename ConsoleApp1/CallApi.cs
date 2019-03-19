using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace ConsoleApp1
{
    public class CallApi<T> : IDisposable where T : IEntity
    {
        HttpClient client = new HttpClient();
        private string Url;
        public CallApi(string Url)
        {
            client.BaseAddress = new Uri(Url);
            this.Url = Url;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<T>> GetAll()
        {
            List<T> list = new List<T>();
            using (HttpResponseMessage response = await client.GetAsync("todos"))
            {
                if (response.IsSuccessStatusCode)
                {
                    list = await response.Content.ReadAsAsync<List<T>>();
                }
                return list;
            }
        }
        public async Task<T> Post(T model)
        {
            using (HttpResponseMessage response = await client.PostAsJsonAsync("todos", model))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
        }
        public async Task<T> Put(T model)
        {
            using (HttpResponseMessage response = await client.PutAsJsonAsync($"todos/{model.id}", model))
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
        }
        public void Dispose()
        {
            client.Dispose();
        }
    }
}
