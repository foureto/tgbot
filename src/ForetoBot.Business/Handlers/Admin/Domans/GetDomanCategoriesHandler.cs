using ForetoBot.Business.Commons.Models;
using ForetoBot.Business.Handlers.Admin._Models;
using ForetoBot.DataAccess;
using Mapster;

namespace ForetoBot.Business.Handlers.Admin.Domans;

public record GetDomanCategories;

public class DomanCategoryDto
{
    public int Id { get; set; }
    public TextRefDto Name { get; set; }
    public TextRefDto Description { get; set; }
    public FileRefDto Label { get; set; }
    public List<DomaCardDto> Cards { get; set; }
}

public class DomaCardDto
{
    public int Id { get; set; }
    public TextRefDto Title { get; set; }
    public FileRefDto Image { get; set; }
    public FileRefDto Sound { get; set; }
    public FileRefDto DescriptionSound { get; set; }
}

public class GetDomanCategoriesHandler(IUnitOfWork unitOfWork)
{
    public async Task<AppResultList<DomanCategoryDto>> Handle(GetDomanCategories request, CancellationToken ct)
    {
        var data = await unitOfWork.Doman.ReadMany(
            e => true,
            e => e.Adapt<DomanCategoryDto>(),
            e => e.Created, true, ct);

        return AppResultList<DomanCategoryDto>.Ok(data);
    }
}