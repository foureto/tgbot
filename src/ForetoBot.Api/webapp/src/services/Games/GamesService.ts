import { IPagedResult, IDataResult } from "@services/commons";
import { ServiceBase } from "@services/ServiceBase";
import { DomanCategory } from "./models";

class GamesService extends ServiceBase {
  protected static BASE_URL = "games/";

  // doman's cards
  public static getDomanCategories(): Promise<IPagedResult<DomanCategory>> {
    return this.get("doman/categories");
  }

  public static createCategory(name: string): Promise<IDataResult<number>> {
    return this.post("doman/categories", { name });
  }
}

export default GamesService;
