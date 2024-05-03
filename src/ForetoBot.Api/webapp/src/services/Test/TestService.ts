import { IDataResult, ServiceBase } from "../ServiceBase";
import { DataItem } from "./models";

class TestService extends ServiceBase {
  protected static BASE_URL = "data/";

  public static getData(): Promise<IDataResult<DataItem[]>> {
    return this.get("");
  }
}

export default TestService;
