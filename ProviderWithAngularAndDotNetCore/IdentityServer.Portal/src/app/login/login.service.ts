import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Login } from './login.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }

  authenticate(login: Login): Observable<any>{
    return this.http.post<any>(`${environment.url_identity_provider}/api/login/authenticate`, login);
  }
}
