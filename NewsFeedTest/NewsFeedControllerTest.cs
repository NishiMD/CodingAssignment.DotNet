using NewsFeedAPI;
using NewsFeedAPI.Controllers;

namespace NewsFeedTest
{
    public class NewsFeedControllerTest
    {
        NewsFeedController controller;
        INewsFeedService service;

        public NewsFeedControllerTest()
        {
            service=new NewsFeedService();
            controller = new NewsFeedController(service);
        }

        [Fact]
        public void GetStoriesCount()
        {
            var result = controller.GetStories();
            Assert.NotEqual(0,result.Result);

        }
        [Fact]
        public void GetPagedStories()
        {
            var result = controller.GetPagedStories();
            Assert.IsType<List<StoryDetails>>(result.Result);

            var data=result.Result;
            Assert.NotEqual(0,data.Count());
        }
    }
}