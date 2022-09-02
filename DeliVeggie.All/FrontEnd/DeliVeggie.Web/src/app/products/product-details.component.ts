import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { timer, Observable } from 'rxjs';
import { timeout, switchMap, filter, take } from 'rxjs/operators';


@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})

export class ProductDetailsComponent implements OnInit {
  private http: HttpClient;
  public product?: Product;

  constructor(http: HttpClient, private route: ActivatedRoute) {
    this.http = http;
  }

  ngOnInit(): void {
    // First get the product id from the current route.
    const routeParams = this.route.snapshot.paramMap;
    const productId = routeParams.get('productId');

    this.http.get<GetProductDetailsQueueResponse>(`http://localhost:8001/gateway/products/${productId}`).subscribe(result => {
      if (!result.isQueued)
        return;

      this.longPoll(result.transactionId)
        .pipe(timeout(2000))
        .subscribe({
          next: (result) => {
            this.product = result.record;
          },
          error: (e) => console.error(e),
          complete: () => console.info('complete')
        });
    }, error => console.error(error));
  }

  longPoll(transactionId: string): Observable<any> {
    return timer(0, 500).pipe(
      switchMap(() => this.checkIfFinished(transactionId)),
      filter((result) => !!result.isDone),
      take(1)
    );
  }

  checkIfFinished(transactionId: string): Promise<GetProductDetailsResponse> {
    return new Promise((resolve) => {
      this.http.get<GetProductDetailsResponse>(`http://localhost:8001/gateway/productdetailresults/${transactionId}`).subscribe(data => {
        resolve(data as GetProductDetailsResponse);
      });
    });
  }
}
