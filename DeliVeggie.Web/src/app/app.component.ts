import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { timer, Observable } from 'rxjs';
import { timeout, switchMap, filter, take } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  private http: HttpClient;
  public products?: Product[];

  constructor(http: HttpClient) {
    this.http = http;
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

  longPoll(transactionId :string): Observable<any> {
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
