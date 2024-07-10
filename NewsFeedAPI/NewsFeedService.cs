using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net;
using System.Text.Json;

namespace NewsFeedAPI
{
    public class NewsFeedService : INewsFeedService
    {
        private HttpClient _httpClient;
        public NewsFeedService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/")
            };
        }
        public async Task<int> GetNewStoriesCountAsync()
        {
            try
            {
                var url = string.Format("newstories.json?print=pretty");
                int result = 0;
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<List<int>>(stringResponse).Take(200).ToList().Count;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Stories not found.");
                    }
                    else
                    {
                        throw new Exception("Failed to fetch data from the server. Status code: " + response.StatusCode);
                    }
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("HTTP request failed: " + ex.Message);
            }
            catch (JsonException ex)
            {
                throw new Exception("JSON deserialization failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred: " + ex.Message);
            }
        }

        public async Task<IEnumerable<StoryDetails>> GetNewPagedStoriesAsync(string sortOrder, int pageNumber, int pageSize)
        {
            try
            {
                var url = string.Format("newstories.json?print=pretty");
                var result = new List<int>();
                var stories = new List<StoryDetails>();
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<List<int>>(stringResponse).Take(200).ToList();
                    //Logic to get the paged data
                    if (result?.Count > 0)
                    {
                        var skip = pageSize * (pageNumber - 1);

                        var canPage = skip < (result?.Count);

                        if (canPage)
                        {
                            foreach (var id in result.Skip(skip).Take(pageSize))
                            {
                                stories.Add(await GetStoryDetailsAsync(id));
                            }
                        }
                    }
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Stories not found.");
                    }
                    else
                    {
                        throw new Exception("Failed to fetch data from the server. Status code: " + response.StatusCode);
                    }
                }

                return sortOrder == "asc" ? stories : stories.OrderByDescending(x => x.id);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("HTTP request failed: " + ex.Message);
            }
            catch (JsonException ex)
            {
                throw new Exception("JSON deserialization failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred: " + ex.Message);
            }
        }
        public async Task<StoryDetails> GetStoryDetailsAsync(int id)
        {
            try
            {
                var url = string.Format($"item/{id}.json?print=pretty");
                var result = new StoryDetails();
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    result = JsonSerializer.Deserialize<StoryDetails>(stringResponse, new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                    });
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Stories not found.");
                    }
                    else
                    {
                        throw new Exception("Failed to fetch data from the server. Status code: " + response.StatusCode);
                    }
                }

                return result;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("HTTP request failed: " + ex.Message);
            }
            catch (JsonException ex)
            {
                throw new Exception("JSON deserialization failed: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}
