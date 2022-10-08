import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TOKEN } from '../../shared/constants/token';
import { Login } from '../../shared/models/login';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-access',
  templateUrl: './access.component.html',
  styleUrls: ['./access.component.css']
})
export class AccessComponent implements OnInit {
  loginForm!: FormGroup;
  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.buildForm();
  }
  private buildForm() {
    this.loginForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    });
  }

  validateControl(controlName: string) {
    return this.loginForm.controls[controlName].invalid && this.loginForm.controls[controlName].touched
  }

  hasError(controlName: string, errorName: string) {
    return this.loginForm.controls[controlName].hasError(errorName)
  }


  login(imputData:any) {
    // this.notificationService.showLoading();
    const login = { ...imputData };
    const userData: Login = {
      username: login.userName,
      password: login.password
    };

    this.authService.login(userData).then((response: any) => {
      localStorage.setItem(TOKEN, response.token);

      this.router.navigateByUrl('/chat');
    }).catch((error: HttpErrorResponse) => {
      console.log(error);
      alert(error.error);
    });
  }

}
