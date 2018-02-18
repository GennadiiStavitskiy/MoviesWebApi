import { Component, OnInit, Input  } from '@angular/core';

@Component({
  selector: 'app-cast-list',
  templateUrl: './cast-list.component.html',
  styleUrls: ['./cast-list.component.css']
})
export class CastListComponent implements OnInit {

  @Input() cast;

  constructor() { }

  ngOnInit() {
  }

}
