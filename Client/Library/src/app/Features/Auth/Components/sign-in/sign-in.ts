import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators, ɵInternalFormsSharedModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../Services/AuthService';

import { RegisterReq } from '../../Models/RegisterReq';
import { LoginReq } from '../../Models/LoginReq';

@Component({
  selector: 'app-sign-in',
  imports: [RouterLink, ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './sign-in.html',
  styleUrl: './sign-in.css',
})
export class SignIn implements OnInit{
  loginForm?:FormGroup;
  @Output() OnLogin=new EventEmitter<LoginReq>();
  constructor(private authService:AuthService) {}
  ngOnInit(): void {
    this.loginForm=new FormGroup({
      'email':new FormControl(null,Validators.required),
      'password':new FormControl(null,Validators.required),
    });
  }
  SignIn() {
 let req:LoginReq={
   Email: this.loginForm?.get('email')?.value,
   Password: this.loginForm?.get('password')?.value,

 }
 this.OnLogin.emit(req);
  }
}
