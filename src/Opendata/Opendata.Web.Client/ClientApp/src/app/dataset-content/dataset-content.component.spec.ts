import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DatasetContentComponent } from './dataset-content.component';

describe('DatasetContentComponent', () => {
  let component: DatasetContentComponent;
  let fixture: ComponentFixture<DatasetContentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DatasetContentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DatasetContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
