import { ChangeDetectorRef, Component } from '@angular/core';
import { SignIn } from '../../Components/sign-in/sign-in';
import { LoginReq } from '../../Models/LoginReq';
import { AuthService } from '../../Services/AuthService';
import { error } from 'console';
import { HttpErrorResponse } from '@angular/common/http';
import { Failure } from '../../../../Global/Failure';
import { fail } from 'assert';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthStateService } from '../../Services/AuthStateService';
import { AuthRes } from '../../Models/AuthRes';


@Component({
  selector: 'app-login-page',
  imports: [SignIn,CommonModule],
  templateUrl: './login-page.html',
  styleUrl: './login-page.css',
})
export class LoginPage {
  errorMessage:string="";
  constructor(private auth:AuthService,
    private cdr:ChangeDetectorRef,
    private router:Router,
    private authState:AuthStateService
  ) {
    console.log("init login");
  }
SignIn(req: LoginReq) {
console.log("auth function");
this.auth.SignIn(req).subscribe(
  {
    next:(res:AuthRes)=>{
this.authState.setToken(res.accessToken,res.refreshToken);
this.router.navigate(['/']);
console.log(res);
    },
    error:(error:HttpErrorResponse)=>{
      const failure=error.error as Failure;
      this.errorMessage=`${failure.status} : ${failure.title}`
    }
    
  }
);
this.cdr.detectChanges();
}
}
