import { ServiceBase } from "@services/ServiceBase";
import { IResult } from "@services/commons";
import { UpdateTextRequest, UploadFileRequest } from "./models";

class ContentService extends ServiceBase {
  protected static BASE_URL = "content/";

  public static updateText(props: UpdateTextRequest): Promise<IResult> {
    return this.put("text", props);
  }

  public static uploadFile(props: UploadFileRequest): Promise<IResult> {
    const params = new FormData();
    params.append("fileId", props.fileId);
    params.append("file", props.file);
    return this.put("file", params, {
      "Content-Type": null,
      Accept: "text/plain",
    });
  }
}

export default ContentService;
