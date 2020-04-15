import {HttpClientModule} from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegionsComponent } from './regions/regions.component';
import { DatasetsComponent } from './datasets/datasets.component';
import { DatasetDescriptionComponent } from './dataset-description/dataset-description.component';
import { DatasetContentComponent } from './dataset-content/dataset-content.component';

@NgModule({
  declarations: [
    AppComponent,
    RegionsComponent,
    DatasetsComponent,
    DatasetDescriptionComponent,
    DatasetContentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
