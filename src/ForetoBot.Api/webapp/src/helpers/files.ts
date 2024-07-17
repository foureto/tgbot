export const toUrl = (file: File) => {
  return new Promise<string>((res, rej) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      if (reader.result) {
        res(reader.result.toString());
        return;
      }
      rej("Could not read file");
    };
    reader.onerror = function (error) {
      rej(error);
    };
  });
};
