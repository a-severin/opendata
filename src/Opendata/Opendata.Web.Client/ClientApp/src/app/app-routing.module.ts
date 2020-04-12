import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegionsComponent } from './regions/regions.component';
import { DatasetsComponent } from './datasets/datasets.component';
import { DatasetDescriptionComponent } from './dataset-description/dataset-description.component';
import { DatasetContentComponent } from './dataset-content/dataset-content.component';


const routes: Routes = [
  { path: 'regions', component: RegionsComponent },
  { path: 'region/:region-code', component: DatasetsComponent },
  { path: 'description/:id', component: DatasetDescriptionComponent },
  { path: 'content/:id', component: DatasetContentComponent },
  { path: '', redirectTo: '/regions', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: true })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
