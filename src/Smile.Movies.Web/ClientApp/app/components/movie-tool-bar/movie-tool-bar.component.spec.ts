import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MovieToolBarComponent } from './movie-tool-bar.component';

describe('MovieToolBarComponent', () => {
  let component: MovieToolBarComponent;
  let fixture: ComponentFixture<MovieToolBarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MovieToolBarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MovieToolBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
