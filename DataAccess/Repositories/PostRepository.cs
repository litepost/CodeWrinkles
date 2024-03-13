using Application;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class PostRepository(SocialDbContext context) : IPostRepository
{
    private readonly SocialDbContext _context = context;

    public async Task<Post> CreatePost(Post toCreate)
    {
        DateTime now = DateTime.Now;
        toCreate.DateCreated = now;
        toCreate.LastModified = now;

        _context.Posts.Add(toCreate);
        await _context.SaveChangesAsync();

        return toCreate;
    }

    public async Task DeletePost(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
        if (post is null) return;

        _context.Remove(post);

        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Post>> GetAllPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    // This might be a way to stream the data back whereas ToListAsync() loads all data into memory
    // public Task<IEnumerable<Post>> GetAllPosts()
    // {
    //     return _context.Posts;
    // }

    public async Task<Post> GetPostById(int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId) ?? throw new Exception("Not found");
        return post;
    }

    public async Task<Post> UpdatePost(string updatedContent, int postId)
    {
        var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId) ?? throw new Exception("Not found");
        post.Content = updatedContent;
        post.LastModified = DateTime.Now;
        await _context.SaveChangesAsync();

        return post;
    }
}
