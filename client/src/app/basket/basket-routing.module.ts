import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { BasketComponent } from './basket.component';

const routes: Routes = [
  {path: '', component: BasketComponent}
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    // gotta do this for the routes to work.
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class BasketRoutingModule { }
