import { Component } from '@angular/core';
import { FinesListComponent } from '../../Components/fines-list-component/fines-list-component';
import { FinesSearchComponent } from '../../Components/fines-search-component/fines-search-component';

@Component({
  selector: 'app-fines-list-page',
  imports: [FinesListComponent, FinesSearchComponent],
  templateUrl: './fines-list-page.html',
  styleUrl: './fines-list-page.css',
})
export class FinesListPage {}
