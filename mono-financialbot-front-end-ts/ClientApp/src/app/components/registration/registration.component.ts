import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Registration } from '../../shared/models/registration';
import { User } from '../../shared/models/user';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  public registerForm!: FormGroup;
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.buildForm();
  }
  private buildForm() {
    this.registerForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      confirm: new FormControl('', [Validators.required])
    });
  }

  validateControl(controlName: string) {
    return this.registerForm.controls[controlName].invalid && this.registerForm.controls[controlName].touched
  }

  hasError(controlName: string, errorName: string) {
    return this.registerForm.controls[controlName].hasError(errorName)
  }


  registerUser(registerFormValue: any) {

    const formValues = { ...registerFormValue };
    const user: Registration = {
      username: formValues.userName,
      password: formValues.password,
      confirmpassword: formValues.confirm
    };
    this.authService.register(user).then(() => {
      alert('Registered successfully!');
      this.router.navigateByUrl('/');
    }).catch((error: HttpErrorResponse) => {

      if (error.error.errors.ConfirmPassword) {
        alert(error.error.errors.ConfirmPassword[0]);
      } else {
        alert(error.error.errors);
      }
    });
  }

}
