using Microsoft.EntityFrameworkCore;
using Rampage.Database;
using Rampage.Database.DomainModels;

namespace Rampage.Services;

public class LayoutService
{
    private readonly AppDbContext _context;

    public LayoutService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategories()
    {
        var categories=await _context.Categories.Where(x=>x.ParentCategoryId==null).ToListAsync();
        return categories;
    }

    public async Task<Dictionary<string,string>> GetSettings()
    {
        var settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);
        return settings;
    }
}
