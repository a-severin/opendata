import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {DataService} from '../data.service';

@Component({
  selector: 'app-dataset-content',
  templateUrl: './dataset-content.component.html',
  styleUrls: ['./dataset-content.component.css']
})
export class DatasetContentComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) {
  }

  ngOnInit(): void {
  }

}
