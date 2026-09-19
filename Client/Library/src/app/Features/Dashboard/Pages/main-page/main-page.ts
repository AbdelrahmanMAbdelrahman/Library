import { Component } from '@angular/core';
import { NavBar } from '../../../nav-bar/nav-bar';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-main-page',
  imports: [NavBar, RouterOutlet],
  templateUrl: './main-page.html',
  styleUrl: './main-page.css',
})
export class MainPage {}
