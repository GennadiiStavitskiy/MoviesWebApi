import { TestBed, inject } from '@angular/core/testing';

import { MovieApiService } from './movie-api.service';

describe('DataService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
        providers: [MovieApiService]
    });
  });

  it('should be created', inject([MovieApiService], (service: MovieApiService) => {
    expect(service).toBeTruthy();
  }));
});
