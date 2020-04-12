import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {Observable} from 'rxjs';
import {switchMap} from 'rxjs/operators';
import {DataService} from '../data.service';
import {Dataset} from '../model/dataset';

@Component({
  selector: 'app-datasets',
  templateUrl: './datasets.component.html',
  styleUrls: ['./datasets.component.css']
})
export class DatasetsComponent implements OnInit {

  datasets$: Observable<Dataset[]>;

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) {
  }

  ngOnInit(): void {
    this.datasets$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.dataService.getDatasets(params.get('region-code'))
      )
    );
  }

}
