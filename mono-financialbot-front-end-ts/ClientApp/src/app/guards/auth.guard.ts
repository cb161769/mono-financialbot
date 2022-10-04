import { Injectable } from '@angular/core';
import {  CanActivate, Router  } from '@angular/router';
import { AuthService } from '../shared/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private readonly authService:AuthService,private readonly router:Router) {

  }
  canActivate(): boolean {
    const isAuthenticated = this.authService.isAuthenticated();
    if (!isAuthenticated) {
      this.router.navigateByUrl('/');
    } else {
      return true
        ;
    }

  }
  
}
