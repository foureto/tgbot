export interface UpdateTextRequest {
  textId: string;
  texts: { [key: string]: string };
}

export interface UploadFileRequest {
  fileId: string;
  file: File;
}
