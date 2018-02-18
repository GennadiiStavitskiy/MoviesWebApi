import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError, map, tap } from 'rxjs/operators';
import { take } from 'rxjs/operators';
import { environment } from '../../environments/environment';

import { Movie } from '../app.component';
import { MessageService } from './message.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable()
export class MovieApiService {
  
  private baseUrl = "http://localhost:50590/api/movies";  

  constructor(private http: HttpClient, private messageService: MessageService) {
	  
	  if(environment.apiEndpoint){
		this.baseUrl = environment.apiEndpoint;
	  }	  
  }

  getMovies(searchQuery: string): Observable<Movie[]> {
    
    var url = this.baseUrl + searchQuery;

    return this.http.get<Movie[]>(url)
      .pipe(catchError(this.handleError('getMovies', []))
    );
  }

  updateMovie(movie: Movie): Observable<any> {
    
    return this.http.put(this.baseUrl, movie, httpOptions)
      .pipe(catchError(this.handleError('updateMovie'))
      );
  }

  addMovie(movie: Movie): Observable<Movie> {    
    return this.http.post<Movie>(this.baseUrl, movie, httpOptions)
      .pipe(catchError(this.handleError<Movie>('addMovie'))
      );
  }

  /**
 * Handle Http operation that failed.
 * Let the app continue.
 * @param operation - name of the operation that failed
 * @param result - optional value to return as the observable result
 */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      //this.log(`${operation} failed: ${error.message}`);

      switch (error.status) {
          case 404: {
              this.messageService.add("Not found.")
              break;
          }
          case 500: {
              this.messageService.add("Server error.")
              break;
          }
          case 400: {
              this.messageService.add("Invalid data.")
              break;
          } 
          default:  {
           	  this.messageService.add("Unknown Error.");
		  }	
      }

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
