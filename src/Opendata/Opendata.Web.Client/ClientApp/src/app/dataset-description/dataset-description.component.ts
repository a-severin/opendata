import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {Observable} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import {DataService} from '../data.service';
import {DatasetDescription} from '../model/dataset-description';

@Component({
  selector: 'app-dataset-description',
  templateUrl: './dataset-description.component.html',
  styleUrls: ['./dataset-description.component.css']
})
export class DatasetDescriptionComponent implements OnInit {

  description$: Observable<DatasetDescription>;

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService) {
  }

  ngOnInit(): void {
    this.description$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.dataService.getDescription(params.get('id')))
    );
  }

}
