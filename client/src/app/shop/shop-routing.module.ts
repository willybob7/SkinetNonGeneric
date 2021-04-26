import { NgModule } from '@angular/core';
// import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ShopComponent } from './shop.component';


const routes: Routes = [
  {path: '', component: ShopComponent},
  // I guess you would only use 'shop/:id' for the path if it was 
  // eager loaded. just use ':id' if it is part of a child route
  // {path: 'shop/:id', component: ProductDetailsComponent},
  {path: ':id', component: ProductDetailsComponent, data: {breadcrumb: {alias: 'productDetails'}}}
];
@NgModule({
  declarations: [],
  imports: [
    // CommonModule
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
