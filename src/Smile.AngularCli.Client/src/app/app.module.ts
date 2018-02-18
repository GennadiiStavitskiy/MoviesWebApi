import { BrowserModule } from '@angular/platform-browser';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { MatDividerModule, MatFormFieldModule, MatDialogModule, MatInputModule } from '@angular/material';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';

import { AppComponent } from './app.component';
import { CastListComponent } from './cast-list/cast-list.component';
import { MovieListComponent } from './movie-list/movie-list.component';
import { MovieDialogComponent } from './movie-dialog/movie-dialog.component';
import { MovieToolBarComponent } from './movie-tool-bar/movie-tool-bar.component';

import { MovieApiService } from './shared/movie-api.service';
import { MovieRefreshService } from './shared/movie-refresh.service';
import { MessageService } from './shared/message.service';


import { CdkDetailRowDirective } from './movie-list/cdk-detail-row.directive';


@NgModule({
  declarations: [
    AppComponent,
    CastListComponent,    
    MovieListComponent,
    MovieDialogComponent,
    MovieToolBarComponent,
    CdkDetailRowDirective
  ],
  imports: [
    BrowserModule,
    FormsModule,
	ReactiveFormsModule,
    HttpModule,
    HttpClientModule,
    MatDividerModule,
    MatFormFieldModule,
    MatDialogModule,
    MatInputModule,
    MatExpansionModule,
    MatListModule,
    MatTableModule,
    MatSelectModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatCheckboxModule,
    MatIconModule,
    BrowserAnimationsModule
  ], 
  providers: [MovieApiService, MovieRefreshService, MessageService],
  bootstrap: [AppComponent],
  entryComponents: [
    MovieDialogComponent
  ]
})
export class AppModule { }
