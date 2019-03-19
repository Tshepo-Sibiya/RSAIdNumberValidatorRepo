import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { from } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ValidatorService {

  validatorApiUrl = 'http://localhost:51009/api/ValidateId';
  constructor(private http: HttpClient) { }

  validateIdNumber(idNumberObj) {
    // get user token:
    const header = new HttpHeaders();
    header.append('Content-Type', 'application/x-www-form-urlencoded');

    return this.http.post(this.validatorApiUrl, idNumberObj, { headers: header });
  }

}
