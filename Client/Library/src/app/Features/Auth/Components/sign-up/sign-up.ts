import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { validate } from '@angular/forms/signals';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../Services/AuthService';
import { RegisterReq } from '../../Models/RegisterReq';

@Component({
  selector: 'app-sign-up',
  imports: [RouterLink,ReactiveFormsModule],
  templateUrl: './sign-up.html',
  styleUrl: './sign-up.css',
})
export class SignUp implements OnInit{
  registerForm?:FormGroup;
   @Output() OnRegister=new EventEmitter<RegisterReq>();
  constructor(private authService:AuthService) {
    console.log("init")
  }
  ngOnInit(): void {
    this.InitForm();
  }

  private InitForm(){
    this.registerForm=new FormGroup({
      'name':new FormControl(null,Validators.required),
      'email':new FormControl(null,Validators.required),
      'userName':new FormControl(null,Validators.required),
      'phone':new FormControl(null,Validators.required),
      'password':new FormControl(null,Validators.required),
    });
  }
  SignUp() {
  // console.table(this.registerForm?.value);
  let req:RegisterReq={
     Name: this.registerForm?.get('name')?.value,
     Email: this.registerForm?.get('email')?.value,
     UserName: this.registerForm?.get('userName')?.value,
     Phone: this.registerForm?.get('phone')?.value,
     Password: this.registerForm?.get('password')?.value
   }
   this.OnRegister.emit(req);
  }
}
