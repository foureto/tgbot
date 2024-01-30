import { ServiceBase } from "../ServiceBase";
import { WeatherItem } from "./models";

class TestService extends ServiceBase {
  protected static BASE_URL = "weatherforecast/";

  public static getWeather(): Promise<WeatherItem[]> {
    return this.get("");
  }
}

export default TestService;
