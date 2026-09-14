import { Routes } from '@angular/router';
import { DashboardPage } from './Features/Dashboard/dashboard-page/dashboard-page';
import { BookPage } from './Features/Books/Pages/book-page/book-page';
import { BookListComponent } from './Features/Books/Components/book-list-component/book-list-component';
import { BookDetailComponent } from './Features/Books/Components/book-detail-component/book-detail-component';
import { BookCreatePage } from './Features/Books/Pages/book-create-page/book-create-page';
import { BookListPage } from './Features/Books/Pages/book-list-page/book-list-page';
import { BookDetailPage } from './Features/Books/Pages/book-detail-page/book-detail-page';

export const routes: Routes = [
    {path:"",component:DashboardPage},
    {path:"BookPage",
        children:[
            {path:"BookListPage",component:BookListPage},
            {path:"BookCreatePage/:id",component:BookCreatePage},
            {path:"BookDetailPage/:id",component:BookDetailPage},
        ]}
];
