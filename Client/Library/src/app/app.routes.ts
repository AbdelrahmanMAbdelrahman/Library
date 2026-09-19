import { Routes } from '@angular/router';
import { BookCreatePage } from './Features/Books/Pages/book-create-page/book-create-page';
import { BookListPage } from './Features/Books/Pages/book-list-page/book-list-page';
import { BookDetailPage } from './Features/Books/Pages/book-detail-page/book-detail-page';

import { LoginPage } from './Features/Auth/Pages/login-page/login-page';
import { RegisterPage } from './Features/Auth/Pages/register-page/register-page';
import { AuthGuard } from './Global/AuthGuard';
import { App } from './app';
import { DashboardPage } from './Features/Dashboard/Pages/dashboard-page/dashboard-page';
import { MainPage } from './Features/Dashboard/Pages/main-page/main-page';


export const routes: Routes = [
    {path:"",component:MainPage,canActivate:[AuthGuard]},
    {path:"Dashboard",component:DashboardPage,canActivate:[AuthGuard]},
    {path:"BookPage",
        children:[
            {path:"BookListPage",component:BookListPage,canActivate:[AuthGuard]},
            {path:"BookCreatePage/:id",component:BookCreatePage,canActivate:[AuthGuard]},
            {path:"BookDetailPage/:id",component:BookDetailPage,canActivate:[AuthGuard]},
        ]},
        {path:"AuthPage",children:[
            {path:"SignUp",component:RegisterPage},
            {path:"SignIn",component:LoginPage},
        ]
        }
];
