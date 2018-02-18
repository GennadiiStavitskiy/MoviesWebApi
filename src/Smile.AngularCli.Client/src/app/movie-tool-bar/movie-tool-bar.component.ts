import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-movie-tool-bar',
  templateUrl: './movie-tool-bar.component.html',
  styleUrls: ['./movie-tool-bar.component.css']
})
export class MovieToolBarComponent  {
  
  sortByArray: SortField[] = [
    { name: "None", value: "none" },
    { name: "Title", value: "title" },
    { name: "Classification", value: "classification" },
    { name: "Genre", value: "genre" },
    { name: "Movie ID", value: "movieId" },
    { name: "Release Date", value: "releaseDate" }
  ];  

  sortBy: SortField = this.sortByArray[0];
  searchquery: string;
  isDescending: boolean;

  @Output()
  searchChanged: EventEmitter<string> = new EventEmitter<string>();

  @Output()
  addMovieClicked: EventEmitter<any> = new EventEmitter<any>();

  constructor() { }

  onChanged() {
      if (this.searchquery) {
          var searchQuery = this.getSearchQueryStr();
          this.searchChanged.emit(searchQuery);
      }
      else {
          this.searchChanged.emit();
      }    
  } 

  addMovieClick() {
    this.addMovieClicked.emit();
  }

  onSearchChange(searchquery: string) {
    console.log(searchquery);
    this.searchquery = searchquery;

    this.onChanged();
  }

  onChangeSortBy() {
    this.onChanged();
  }

  onChangeSortDirection() {
    this.onChanged();
  }

  getSearchQueryStr(): string {

    var result: string = "";
    var sortByStr = null;

    if (this.sortBy != null && this.sortBy.value != "none") {
      sortByStr = 'sort=' + (this.isDescending ? '-' : '') + this.sortBy.value;
    }

    var searchByStr = null;

    if (this.searchquery) {
      searchByStr = 'search=' + this.searchquery;
    }

    if (searchByStr || sortByStr) {
      result += '?';

      if (searchByStr) {
        result += searchByStr + (sortByStr ? '&' + sortByStr : '');
      }
      else {
        result += sortByStr
      }
    }

    return result;
  }

}

interface SortField {
  name: string;
  value: string;
}
