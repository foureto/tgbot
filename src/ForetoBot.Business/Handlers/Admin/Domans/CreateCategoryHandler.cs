using ForetoBot.Business.Commons.Models;
using ForetoBot.DataAccess;
using ForetoBot.DataAccess.Domain.Games;
using ForetoBot.DataAccess.Domain.References;

namespace ForetoBot.Business.Handlers.Admin.Domans;

public class CreateCategory
{
    public string Name { get; set; }
}

public class CreateCategoryHandler(IUnitOfWork unitOfWork)
{
    public async Task<AppResult<int>> Handle(CreateCategory request)
    {
        var result = await unitOfWork.Doman.AddAndSave(
            new DomanCategory { Name = new StoredText { En = request.Name, Ru = request.Name } });

        return AppResult<int>.Ok(result.Id, "New category stored");
    }
}