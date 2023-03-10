import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ConverterService {
  url = 'http://localhost:4545/Converter/';

  constructor(private http: HttpClient) {}

  getCvsFromJson(json: string): Observable<any> {
    let params = new HttpParams();
    params = params.append('jsonString', json);

    let httpHeaders = new HttpHeaders();

    return this.http.get<any>(`${this.url}json-to-csv`, {
      headers: httpHeaders,
      params: params,
    });
  }
}
