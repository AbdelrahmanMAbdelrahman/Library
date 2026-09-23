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
import { BorrowingRecordListPage } from './Features/BorrowingRecords/Pages/borrowing-record-list-page/borrowing-record-list-page';
import { BorrowingRecordCreatePage } from './Features/BorrowingRecords/Pages/borrowing-record-create-page/borrowing-record-create-page';
import { BorrowingRecordDetailPage } from './Features/BorrowingRecords/Pages/borrowing-record-detail-page/borrowing-record-detail-page';
import { FinesListPage } from './Features/Fines/Pages/fines-list-page/fines-list-page';
import { FinesDetailPage } from './Features/Fines/Pages/fines-detail-page/fines-detail-page';


export const routes: Routes = [
    {path:"",component:MainPage,canActivate:[AuthGuard],
        children:[
    {path:"Dashboard",component:DashboardPage,canActivate:[AuthGuard]},
    {path:"BookPage",
        children:[
            {path:"BookListPage",component:BookListPage,canActivate:[AuthGuard]},
            {path:"BookCreatePage/:id",component:BookCreatePage,canActivate:[AuthGuard]},
            {path:"BookDetailPage/:id",component:BookDetailPage,canActivate:[AuthGuard]},
        ]},
        {path:"BorrowingRecordPage",
            children:[
                {path:"BorrowingRecordListPage",component:BorrowingRecordListPage},
                {path:"BorrowingRecordCreatePage/:id",component:BorrowingRecordCreatePage},
                {path:"BorrowingRecordDetailPage/:id",component:BorrowingRecordDetailPage},

            ]
        },
        {path:"FinePage",children:[
            {path:"FineListPage",component:FinesListPage},
            {path:"FineDetailPage/:id",component:FinesDetailPage},
        ]}
        ]
    },
    {path:"AuthPage",children:[
            {path:"SignUp",component:RegisterPage},
            {path:"SignIn",component:LoginPage},
        ]
        }
];
