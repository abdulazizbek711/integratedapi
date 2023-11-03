using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
namespace Integration
{
    internal class Program
    {
        public static void Main(string[] args)
        {
             HttpClient clien = new HttpClient();
            Console.WriteLine("Calling WebAPI...");
            var responseTask = clien.GetAsync(  "https://the-cocktail-db.p.rapidapi.com/search.php?s=vodka");
            responseTask.Wait();
            if (responseTask.IsCompleted)
            {
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var messageTask = result.Content.ReadAsStringAsync();
                    messageTask.Wait();
                    Console.WriteLine("Message from WebAPI : " + messageTask.Result);
                    Console.ReadLine();
                }
            }
            var postData = new PostData()
            {
              User_ID = 7,
              UserName = "Abdulaziz",
              PhoneNumber = 977119717,
              Email = "panjiyevabdulaziz77@gmail.com"
            };
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7237/api/User");
            var json = JsonSerializer.Serialize(postData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync("posts", content).Result;
            if (response.IsSuccessStatusCode) {
              var responseContent = response.Content.ReadAsStringAsync().Result;\
              var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
              };
              var postResponse = System.Text.Json.JsonSerializer.Deserialize<PostResponse>(responseContent, options);
              Console.WriteLine(responseContent);
            } else {
              Console.WriteLine("Error: " + response.StatusCode);
            } 
        }
    }

    
}