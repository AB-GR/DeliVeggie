import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ProductDetailsComponent } from './products/product-details.component';
import { ProductListComponent } from './products/product-list.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductDetailsComponent,
    ProductListComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', component: ProductListComponent },
      { path: 'products/:productId', component: ProductDetailsComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

declare global {

  export interface GetProductsQueueResponse {
    isQueued: boolean;
    transactionId: string;
  }

  export interface GetProductsResponse {
    isDone: boolean;
    records: Product[];
  }

  export interface GetProductDetailsQueueResponse {
    isQueued: boolean;
    transactionId: string;
  }

  export interface GetProductDetailsResponse {
    isDone: boolean;
    record: Product;
  }

  export interface Product {
    id: string;
    name: string;
    price: number;
    entryDate: string;
  }
}
