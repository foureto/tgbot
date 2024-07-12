using ForetoBot.Business.Commons.Models;
using Marten.Services;

namespace ForetoBot.Business.Handlers.Admin.Domans;

public record GetDomanCategories;

public class DomanCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Label { get; set; }
}

public class GetDomanCategoriesHandler(IUnitOfWork unitOfWork)
{
    public async Task<AppResultList<DomanCategoryDto>> Handle(GetDomanCategories request)
    {
        await Task.Yield();
        return AppResultList<DomanCategoryDto>.Ok([]);
    }
}