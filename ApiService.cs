using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<User>> GetUsersWithPostsAsync(string usersApiUrl, string postsApiUrl)
    {
        try
        {
            HttpResponseMessage usersResponse = await _httpClient.GetAsync(usersApiUrl);
            List<User> users = new List<User>();

            if (usersResponse.IsSuccessStatusCode)
            {
                string usersResponseBody = await usersResponse.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<List<User>>(usersResponseBody);

                foreach (User user in users)
                {
                    HttpResponseMessage postsResponse = await _httpClient.GetAsync($"{postsApiUrl}?userId={user.Id}");

                    if (postsResponse.IsSuccessStatusCode)
                    {
                        string postsResponseBody = await postsResponse.Content.ReadAsStringAsync();
                        List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(postsResponseBody);
                        user.Posts = posts;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Error: {usersResponse.StatusCode} - {usersResponse.ReasonPhrase}");
            }

            return users;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}

