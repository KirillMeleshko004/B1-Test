import { serverApiURL } from "../constants/apiConstatns";

export async function getSingleData<T>(
  endPoint: string
): Promise<ResponseModel<T>> {
  let payload: RequestInit = {
    method: "GET",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  };

  return await sendRequest<T>(serverApiURL + endPoint, payload);
}

export async function getData<T>(
  endPoint: string,
  page: number,
  pageSize: number
): Promise<ResponseModel<T>> {
  let payload: RequestInit = {
    method: "GET",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  };

  const query = `?PageNumber=${page}&PageSize=${pageSize}`;

  return await sendRequest<T>(serverApiURL + endPoint + query, payload);
}

export async function postFormData<T>(
  endPoint: string,
  data: FormData
): Promise<ResponseModel<T>> {
  let payload = {
    method: "POST",
    headers: {
      Accept: "application/json",
    },
    body: data,
  };

  return await sendRequest<T>(serverApiURL + endPoint, payload);
}

async function sendRequest<T>(
  uri: string,
  payload: RequestInit
): Promise<ResponseModel<T>> {
  try {
    const response = await fetch(uri, payload);

    if (!response.ok) {
      throw new Error(response.statusText);
    }

    const jsonRes = await response.json();

    const pagingHeader = response.headers.get("X-Pagination")!;

    return {
      isSuccessful: true,
      errorMessage: null,
      data: jsonRes as T,
      pageData: JSON.parse(pagingHeader),
    };
  } catch (error) {
    var message = (error as Error).message;

    console.error(message);

    return {
      isSuccessful: false,
      errorMessage: message,
      data: null,
      pageData: null,
    };
  }
}

type SuccessResult<T> = {
  isSuccessful: true;
  errorMessage: null;
  data: T;
  pageData: PageMetadata;
};

type ErrorResult = {
  isSuccessful: false;
  errorMessage: string;
  data: null;
  pageData: null;
};

export type ResponseModel<T> = SuccessResult<T> | ErrorResult;

export type PageMetadata = {
  CurrentPage: number;
  TotalPages: number;
  PageSize: number;
  TotalCount: number;
  HasPrevious: boolean;
  HasNext: boolean;
};
