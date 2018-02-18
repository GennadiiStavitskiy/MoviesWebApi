import { Component, Input, Output, EventEmitter, ViewChild, OnChanges, ChangeDetectorRef  } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ViewRef_ } from '@angular/core/src/view';

import { Movie } from '../app.component';
import { MovieRefreshService } from '../shared/movie-refresh.service';


@Component({
  selector: 'app-movie-list',
  templateUrl: './movie-list.component.html',
  styleUrls: ['./movie-list.component.css'],
  animations: [
    trigger('detailExpand', [
      state('void', style({ height: '0px', minHeight: '0', visibility: 'hidden' })),
      state('*', style({ height: '*', visibility: 'visible' })),
      transition('void <=> *', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class MovieListComponent {  
  
  @Input() movies;

  dataSource = new MatTableDataSource(this.movies);

  @Output()
  updateMovieClick: EventEmitter<Movie> = new EventEmitter<Movie>();

  @Output()
  addMovieClicked: EventEmitter<any> = new EventEmitter<any>();

  displayedColumns = ['movieId', 'title', 'genre', 'classification', 'releaseDate', 'rating', 'column1'];   

  isExpansionDetailRow = (index, row) => row.hasOwnProperty('detailRow');

  constructor(private refreshService: MovieRefreshService, private changeDetectorRefs: ChangeDetectorRef) {

      refreshService.refreshMovies.subscribe(() => {
          this.dataSource = new MatTableDataSource(this.movies);
          if (this.changeDetectorRefs != null &&
              this.changeDetectorRefs != undefined &&
              !(this.changeDetectorRefs as ViewRef_).destroyed) {
              this.changeDetectorRefs.detectChanges();
          }  
      });
  }

  ngOnChanges(changes: any) {
      console.log(changes.movies.currentValue);
      this.dataSource = new MatTableDataSource(changes.movies.currentValue);
      if (this.changeDetectorRefs != null &&
          this.changeDetectorRefs != undefined &&
          !(this.changeDetectorRefs as ViewRef_).destroyed) {
          this.changeDetectorRefs.detectChanges();
      }   
  }

  update(movie: Movie) {
    this.updateMovieClick.emit(movie);
  }   

  create() {
      this.addMovieClicked.emit();
  } 
}
