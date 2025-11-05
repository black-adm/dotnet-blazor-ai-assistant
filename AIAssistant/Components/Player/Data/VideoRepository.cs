using AIAssistant.Data;
using Microsoft.EntityFrameworkCore;

namespace AIAssistant.Components.Player.Data;

public class VideoRepository(AppDbContext context) : IVideoRepository
{
    public async Task<IEnumerable<Video>> GetAllAsync()
    {
        return await context.Videos.ToListAsync();
    }

    public async Task<Video> GetByIdAsync(Guid id)
    {
        var video = await context.Videos.FindAsync(id);
        return video ?? new Video();
    }

    public async Task<Video> GetBySlugAsync(string slug)
    {
        var video = await context.Videos.Where(v => v.Slug == slug).FirstOrDefaultAsync();
        return video ?? new Video();
    }

    public async Task AddAsync(Video video)
    {
       await context.Videos.AddAsync(video);
       await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Video video)
    {
        context.Videos.Update(video);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var video = await context.Videos.FindAsync(id);

        if (video != null)
        {
            context.Videos.Remove(video);
            await context.SaveChangesAsync();
        }
    }
}