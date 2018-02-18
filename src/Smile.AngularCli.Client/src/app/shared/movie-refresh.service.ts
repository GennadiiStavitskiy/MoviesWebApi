import { Injectable, Output, EventEmitter, ViewChild } from '@angular/core';


@Injectable()
export class MovieRefreshService {

    @Output()
    refreshMovies: EventEmitter<any> = new EventEmitter<any>();

    onRefreshMovies() {
        this.refreshMovies.emit();
    }
}
 
