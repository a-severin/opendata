import {Component, OnInit, OnDestroy} from '@angular/core';
import {DataService} from '../data.service';
import {Region} from '../model/region';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-regions',
  templateUrl: './regions.component.html',
  styleUrls: ['./regions.component.css']
})
export class RegionsComponent implements OnInit, OnDestroy {

  regions$: Observable<Region[]>;

  constructor(private dataService: DataService) {
  }

  ngOnDestroy(): void {
  }

  ngOnInit(): void {
    this.regions$ = this.dataService.getRegions();
  }

}
