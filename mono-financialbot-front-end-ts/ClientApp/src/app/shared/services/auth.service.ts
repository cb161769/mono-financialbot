import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../../environments/environment';
import { TOKEN } from '../constants/token';
import { Login } from '../models/login';
import { Registration } from '../models/registration';
import { ResponseModel } from '../models/response';
import { Userdata } from '../models/userdata';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl: string = environment.BASE_URL;
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService, private router: Router) { }
  register(request: Registration) {
    return this.http.post<ResponseModel>(`${this.baseUrl}api/users/register`, request).toPromise();
  }
  login(request: Login) {
    return this.http.post<ResponseModel>(`${this.baseUrl}api/users/login`, request).toPromise();
  }
  logout() {
    localStorage.removeItem(TOKEN);
    this.router.navigate(['']);
  }
  getCurrentUser() {
    let token = localStorage.getItem(TOKEN) as any;
    if (!!token ) { token = ""; }
    return this.jwtHelper.decodeToken<Userdata>(token);
  }

  isAuthenticated() {
    let token = localStorage.getItem(TOKEN) as any;

    if (token == null) { token = ""; }
    return !this.jwtHelper.isTokenExpired(token);
  }
}
