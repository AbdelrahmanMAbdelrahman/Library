import { ChangeDetectorRef, Component } from '@angular/core';
import { SignUp } from '../../Components/sign-up/sign-up';
import { RegisterReq } from '../../Models/RegisterReq';
import { AuthService } from '../../Services/AuthService';
import { error } from 'console';
import { HttpErrorResponse } from '@angular/common/http';
import { Failure } from '../../../../Global/Failure';
import { Router } from '@angular/router';
import { fail } from 'assert';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register-page',
  imports: [SignUp,CommonModule],
  templateUrl: './register-page.html',
  styleUrl: './register-page.css',
})
export class RegisterPage {
errorMessage:string="";
  constructor(private auth:AuthService,private router:Router,private cdr:ChangeDetectorRef) {
     console.log("init");
  }
SignUp(req: RegisterReq) {
  debugger;
this.auth.SignUp(req).subscribe({
next: val=>{
this.router.navigate(['/AuthPage','SignIn'])
},
error:(error:HttpErrorResponse)=>{
  
  const failure=error.error as Failure;
  this.errorMessage=`${failure.status} : ${failure.title}`;
  this.cdr.detectChanges();
}
});
}  
}
