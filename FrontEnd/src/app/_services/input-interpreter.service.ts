import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { shapeInput } from '../_models/shapeInput.model';

const AUTH_API = 'https://localhost:44334/api/InputInterpreter/';
const httpOptions = {headers: new HttpHeaders({ 'Content-Type': 'application/json', }) };

@Injectable({
  providedIn: 'root'
})
export class InputInterpreterService {

  constructor(private http: HttpClient) { }

  InterpretInput(shapeInput: shapeInput) : Observable<any>{
    return this.http.post(AUTH_API + 'InterpretShapeInput', shapeInput, httpOptions);
  }
}