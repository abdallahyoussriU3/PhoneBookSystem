import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BASE_URL } from "../constants/constant";
import {
  IGetAllContactsResponse,
  IGetContactsResponse,
} from "../interfaces/index.interfaces";
import { Observable, of } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ContactService {
  constructor(private http: HttpClient) {}

  getAllContacts(): Observable<IGetAllContactsResponse> {
    return this.http.get<IGetAllContactsResponse>(
      `${BASE_URL}/Contacts/GetAll`
    );
  }

  getContactById(id: string): Observable<IGetContactsResponse> {
    return this.http.get<IGetContactsResponse>(
      `${BASE_URL}/Contacts/GetById/${id}`
    );
  }

  addContact(
    name: string,
    phoneNumber: string,
    email: string
  ): Observable<any> {
    return this.http.post(`${BASE_URL}/Contacts/AddContact`, {
      name,
      phoneNumber,
      email,
    });
  }

  updateContact(id: string, data: any): Observable<any> {
    return this.http.put(`${BASE_URL}/Contacts/${id}`, data);
  }

  deleteContact(id: number): Observable<any> {
    return this.http.delete(`${BASE_URL}/Contacts/${id}`);
  }

  searchContacts(query: string): Observable<IGetAllContactsResponse> {
    return this.http.get<IGetAllContactsResponse>(
      `${BASE_URL}/Contacts/search/?query=${query}`
    );
  }
}
