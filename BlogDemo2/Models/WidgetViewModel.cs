using System.Collections.Generic;
using BlogDemo2.Core;
using BlogDemo2.Core.Objects;

namespace BlogDemo2.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel(IBlogRepository blogRepository)
        {
            Categories = blogRepository.Categories();
            Tags = blogRepository.Tags();
            LatestPosts = blogRepository.Posts(1, 10);
        }

        public IList<Category> Categories { get; }
        public IList<Tag> Tags { get; }
        public IList<Post> LatestPosts { get; }
    }
}