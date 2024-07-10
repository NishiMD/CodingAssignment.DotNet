using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;

namespace NewsFeedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("*","*","*")]
    public class NewsFeedController:ControllerBase
    {
        private readonly INewsFeedService _newsFeedService;
        public NewsFeedController(INewsFeedService newsFeedService)
        {
            _newsFeedService = newsFeedService;
        }

        [HttpGet]
        public Task<int> GetStories()
        {
            return _newsFeedService.GetNewStoriesCountAsync();
        }
        [HttpGet]
        public Task<IEnumerable<StoryDetails>> GetPagedStories(string sortOrder="asc", int pageNumber=1, int pageSize=2)
        {
            return _newsFeedService.GetNewPagedStoriesAsync(sortOrder,pageNumber,pageSize);
        }
        [HttpGet]
        public async Task<StoryDetails> GetStoryDetailsAsync(int id)
        {
            return await _newsFeedService.GetStoryDetailsAsync(id);
        }
    }
}
