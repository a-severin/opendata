import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {of, Observable} from 'rxjs';
import {Dataset} from './model/dataset';
import {Region} from './model/region';
import {DatasetDescription} from './model/dataset-description';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  regionsUrl = 'api/regions';
  private datasets: Map<string, Dataset[]> = new Map<string, Dataset[]>(
    [
      ['34', [
        {id: '1', code: '34', title: 'Главы муниципалитетов'},
        {id: '2', code: '34', title: 'Адреса школ'},
        {id: '3', code: '34', title: 'Адреса больниц'},
      ]]
    ]
  );
  private descriptions: Map<string, DatasetDescription> = new Map<string, DatasetDescription>(
    [
      ['1', {id: '1', description: 'Описание данных Главы муниципалитетов'}],
      ['2', {id: '2', description: 'Описание данных Адреса школ'}],
      ['3', {id: '3', description: 'Описание данных Адреса больниц'}]
    ]
  );

  constructor(private http: HttpClient) {
  }

  getRegions(): Observable<Region[]> {
    return this.http.get<Region[]>(this.regionsUrl);
  }

  getDatasets(regionCode: string): Observable<Dataset[]> {
    return of(this.datasets.get(regionCode));
  }

  getDescription(id: string): Observable<DatasetDescription> {
    return of(this.descriptions.get(id));
  }
}
