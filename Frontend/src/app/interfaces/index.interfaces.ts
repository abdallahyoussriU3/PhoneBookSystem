import { IContact } from "../models/contact.model";

export interface IResponse {
  statusCode: number;
  isSuccess: boolean;
  errorMessages: string[];
}

export interface IGetAllContactsResponse extends IResponse {
  result: IContact[];
}

export interface IGetContactsResponse extends IResponse {
  result: IContact;
}
