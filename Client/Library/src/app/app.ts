import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBar } from './Features/nav-bar/nav-bar';
import { AuthPage } from './Features/Auth/Pages/auth-page/auth-page';
import { AuthStateService } from './Features/Auth/Services/AuthStateService';
import { CommonModule } from '@angular/common';
import { SignIn } from './Features/Auth/Components/sign-in/sign-in';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavBar, AuthPage, CommonModule, SignIn],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Library');
  /**
   *
   */
  // isAuthenticated:boolean=false;
  constructor(public authState:AuthStateService) {
    debugger;
    console.log(authState.isAuthenticated)
    // this.isAuthenticated=authState.isAuthenticated();
    // console.log(this.isAuthenticated?"authenticated":"not authencticated")
  }

}
