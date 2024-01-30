import { redirect } from "react-router-dom";
import { notification } from "antd";

const openNotificationSuccess = (message: string) => {
  notification.success({
    message: "Success",
    description: message,
  });
};

const openNotificationError = (message: string) => {
  notification.error({
    message: "Error",
    description: message,
  });
};

export abstract class ServiceBase {
  protected static BASE_URL = "";

  protected static getUrl(url: string): string {
    return `/api/${this.BASE_URL}${url}`;
  }

  protected static async http(
    method: string,
    url: string,
    data?: any,
    // eslint-disable-next-line prettier/prettier
    otherHeaders?: any
  ): Promise<any> {
    const headers = {
      Accept: "application/json",
      "Content-Type": "application/json",
      "X-CSRF": "bo",
      ...otherHeaders,
    };

    if (otherHeaders && otherHeaders["Content-Type"] === null) {
      delete otherHeaders["Content-Type"];
    }
    const response = await fetch(this.getUrl(url), {
      method,
      body:
        method === "GET"
          ? null
          : otherHeaders["Content-Type"]
            ? JSON.stringify(data)
            : data,
      credentials: "include",
      headers,
      redirect: "follow",
    });

    return new Promise(async (resolve, reject) => {
      if (
        response.headers.get("content-type") === "application/pdf" ||
        !!response.headers.get("content-disposition")
      ) {
        const res = response.blob();
        return resolve(res);
      }
      if (response.status === 401) {
        openNotificationError("You are not authorized");
        redirect("/login");
        return reject("You are not authorized");
      }

      try {
        const result = await response.json();
        if (result.success === false) {
          if (result?.message) openNotificationError(result.message);
          return reject(`${result.message} (${result.errorCode})`);
        }

        if (result?.message) openNotificationSuccess(result.message);
        return resolve(result);
      } catch {
        return reject("Bad response");
      }
    });
  }

  protected static get(url: string, data?: any, options?: any): any {
    if (data) {
      [...Object.getOwnPropertyNames(data)].forEach((e) => {
        if (data[e] === undefined) delete data[e];
      });
    }

    const query = data ? `?${new URLSearchParams(data).toString()}` : "";
    return this.http("GET", url + query, data, options);
  }

  protected static post(url: string, data?: any, options?: any): any {
    return this.http("POST", url, data, options);
  }

  protected static put(url: string, data?: any, options?: any): any {
    return this.http("PUT", url, data, options);
  }

  protected static delete(url: string, data?: any, options?: any): any {
    return this.http("DELETE", url, data, options);
  }
}
