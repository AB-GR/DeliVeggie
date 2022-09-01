import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { timer, Observable } from 'rxjs';
import { timeout, switchMap, filter, take } from 'rxjs/operators';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  private http: HttpClient;
  public products?: Product[];

  constructor(http: HttpClient) {
    this.http = http;
  }

  longPoll(transactionId: string): Observable<any> {
    return timer(0, 500).pipe(
      switchMap(() => this.checkIfFinished(transactionId)),
      filter((result) => !!result.isDone),
      take(1)
    );
  }

  checkIfFinished(transactionId: string): Promise<GetProductsResponse> {
    return new Promise((resolve) => {
      this.http.get<GetProductsResponse>(`http://localhost:8001/gateway/productresults/${transactionId}`).subscribe(data => {
        resolve(data as GetProductsResponse);
      });
    });
  }

  title = 'DeliVeggie Products';

  ngOnInit(): void {
    this.http.get<GetProductsQueueResponse>('http://localhost:8001/gateway/products').subscribe(result => {
      if (!result.isQueued)
        return;

      this.longPoll(result.transactionId)
        .pipe(timeout(2000))
        .subscribe({
          next: (result) => {
            this.products = result.records;
          },
          error: (e) => console.error(e),
          complete: () => console.info('complete')
        });
    }, error => console.error(error));
  }

}

interface GetProductsQueueResponse {
  isQueued: boolean;
  transactionId: string;
}

interface GetProductsResponse {
  isDone: boolean;
  records: Product[];
}

interface Product {
  id: string;
  name: string;
  price: number;
}
