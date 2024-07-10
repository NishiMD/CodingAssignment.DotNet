namespace NewsFeedAPI
{
    public interface INewsFeedService
    {
        /// <summary>
        /// Get count of new stories
        /// </summary>
        /// <returns></returns>
        Task<int> GetNewStoriesCountAsync();
        /// <summary>
        /// Get Paged stories with details
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<StoryDetails>> GetNewPagedStoriesAsync(string sortOrder, int pageNumber, int pageSize);
        /// <summary>
        /// Get Story details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<StoryDetails> GetStoryDetailsAsync(int id);
    }
}
