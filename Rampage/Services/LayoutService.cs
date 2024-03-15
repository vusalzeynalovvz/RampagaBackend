using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using Rampage.Database;
using Rampage.Database.DomainModels;
using System.Security.Claims;

namespace Rampage.Services;

public class LayoutService
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _contextAccessor;

    public LayoutService(AppDbContext context, IHttpContextAccessor contextAccessor)
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public async Task<List<Category>> GetCategories()
    {
        var categories = await _context.Categories.Where(x => x.ParentCategoryId == null).ToListAsync();
        return categories;
    }

    public async Task<Dictionary<string, string>> GetSettings()
    {
        var settings = await _context.Settings.ToDictionaryAsync(x => x.Key, x => x.Value);
        return settings;
    }

    public async Task<List<BasketItem>> GetBasket()
    {
        var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;

        if (userId is null)
            return new();

        var basketItems = (await _context.BasketItems.Where(x => x.AppUserId == userId && x.IsSale == false).Include(x => x.Product).ToListAsync());

        return basketItems;


    }
}
