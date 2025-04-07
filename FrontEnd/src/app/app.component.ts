// src/app/app.component.ts
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';

@Component({
  selector: 'app-root',
  imports: [RouterModule, MatToolbarModule],
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css']
})
export class AppComponent {}