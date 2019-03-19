using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var api = new CallApi<Todo>("https://jsonplaceholder.typicode.com/"))
            {
                var b = api.Put(new Todo { userId=1,id=1,title="Testing",completed=true});
                Console.WriteLine("Put");
                Console.WriteLine(JsonConvert.SerializeObject(b));
                Console.WriteLine("Post");
                var a = api.Post(new Todo { title = "Hey", userId = 1, completed = false }).GetAwaiter().GetResult();
                Console.WriteLine(JsonConvert.SerializeObject(a));
                Console.WriteLine("Get");
                var list = api.GetAll().GetAwaiter().GetResult();
                Console.WriteLine(JsonConvert.SerializeObject(list));
                Console.ReadLine();
            }            
        }
    }
}
