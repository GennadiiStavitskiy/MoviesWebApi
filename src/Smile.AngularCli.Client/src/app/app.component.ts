import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError, retry } from 'rxjs/operators';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MovieDialogComponent } from './movie-dialog/movie-dialog.component';

import { MovieApiService } from './shared/movie-api.service';
import { MovieRefreshService } from './shared/movie-refresh.service';
import { MessageService } from './shared/message.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  public movies: Movie[];

  constructor(private dialog: MatDialog, private http: Http, private apiService: MovieApiService,
    private refreshService: MovieRefreshService, private messageService: MessageService) {

  }

  search(searchQuery: string) {
    this.movies = null;
    this.messageService.clear();

    if (searchQuery) {
      this.apiService.getMovies(searchQuery).subscribe(m => {
        if (m && m.length > 0) {
          this.movies = m;
          this.refreshService.onRefreshMovies();
        }
      });
    }
  }

  update(movie: Movie) {
    this.messageService.clear();

    var movieCopy = Object.assign({}, movie);

    movieCopy.castStr = movieCopy.cast.join('\n');

    //show update movie dialog
    let dialogRef = this.dialog.open(MovieDialogComponent, {
      width: '250px',
      data: { movie: movieCopy, dialogTitle: "Update Movie" }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');

      if (result) {

        result = this.adjustCast(result);

        //update movie on the server by api
        this.apiService.updateMovie(result).subscribe(m => {
          if (m) {
            //update movie in collection
            var index = this.movies.findIndex(obj => obj.movieId === m.movieId);

            if (index >= 0) {
              this.movies[index] = m;
              this.refreshService.onRefreshMovies();
            }
          }
        });
      }
    });
  }

  create() {
    var newMovie: Movie = { cast: [], classification: "", genre: "", movieId: 0, rating: 0, releaseDate: 0, title: "", castStr: "" };

    //show update movie dialog
    let dialogRef = this.dialog.open(MovieDialogComponent, {
      width: '250px',
      data: { movie: newMovie, dialogTitle: "Add Movie" }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');

      if (result) {

        result = this.adjustCast(result);

        //create movie by api        
        this.apiService.addMovie(result).subscribe(m => {
          if (m) {
            if (this.movies) {
              this.movies.push(m);
            }
            else {
              this.movies = [m];
            }

            this.refreshService.onRefreshMovies();
          }
        });
      }
    });
  }

  adjustCast(movie: Movie): Movie {

    if (!movie.castStr) {
      movie.cast = [];
    }

    movie.cast = movie.castStr.replace(/\n/g, ",").split(",");

    return movie;
  }

}

export interface Movie {
  classification: string;
  genre: string;
  movieId: number;
  rating: number;
  releaseDate: number;
  title: string;
  castStr: string;
  cast: string[];
}

