import { Component, OnInit } from '@angular/core';
import { FinesListComponent } from '../../Components/fines-list-component/fines-list-component';
import { FinesSearchComponent } from '../../Components/fines-search-component/fines-search-component';
import { Observable } from 'rxjs';
import { PaginatedList } from '../../../../Global/PaginatedList';
import { FineRes } from '../../Models/FineRes';
import { FineService } from '../../Services/FineService';
import { FineSearch } from '../../Models/FineSearch';
import { DefaultSettings } from '../../../../Global/Consts/DefaultConsts';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-fines-list-page',
  imports: [FinesListComponent, FinesSearchComponent,AsyncPipe],
  templateUrl: './fines-list-page.html',
  styleUrl: './fines-list-page.css',
})
export class FinesListPage implements OnInit{
  paginatedFines?:Observable<PaginatedList<FineRes>>;

  constructor(private fineService:FineService) { }
  ngOnInit(): void {
    this.GetFines();
  }
  GetFines() {
    let search:FineSearch={
      pageNumber: 1,
      pageSize: DefaultSettings.PageSize
    }
    this.paginatedFines=this.fineService.GetFines(search);
  }
  Paginate(search: FineSearch) {
  this.paginatedFines=this.fineService.GetFines(search);
  }
  Search(search: FineSearch) {
    debugger;
  this.paginatedFines=this.fineService.GetFines(search);
  }
}
