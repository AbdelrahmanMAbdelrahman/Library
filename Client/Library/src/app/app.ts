import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DashboardPage } from './Features/Dashboard/dashboard-page/dashboard-page';
import { NavBar } from './Features/nav-bar/nav-bar';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet, DashboardPage,NavBar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Library');
}
