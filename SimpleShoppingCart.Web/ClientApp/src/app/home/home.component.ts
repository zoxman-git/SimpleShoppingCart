import { HttpClient } from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { UserLoginViewModel } from '../models/user-login.model';
import { ProductViewModel } from '../models/product.model';
import { Observable } from 'rxjs';
import { CartViewModel } from '../models/cart.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public username: string = 'sam_smith';
  public password: string = 'PASSword!234';
  public signInSuccess?: boolean;
  public products: ProductViewModel[] = [];
  public cart: CartViewModel = { cartItems: [] } as CartViewModel;
  public cartTotal = 0;

  constructor(private readonly http: HttpClient, @Inject('BASE_URL') private readonly baseUrl: string) {

  }

  public login() {
    const userLoginModel: UserLoginViewModel = {
      username: this.username,
      password: this.password
    };

    this.http.post<boolean>(this.baseUrl + 'user/login', userLoginModel).subscribe(result => {
      this.signInSuccess = result;

      if (this.signInSuccess) {
        this.getProducts().subscribe(result => {
          this.products = result;
        }, error => console.error(error));
      }
    }, error => console.error(error));
  }

  public addToCart(product: ProductViewModel) {
    this.cart.cartItems.push({
      productId: product.id,
      name: product.name,
      code: product.code,
      price: product.price,
      volumeDiscountQuantity: product.volumeDiscountQuantity,
      volumeDiscountPrice: product.volumeDiscountPrice
    });

    this.cartTotal = 0;
  }

  public addToCartByProductCodes(productCodes: string) {
    this.cart.cartItems = [];

    for (let i = 0; i < productCodes.length; i++) {
      let code = productCodes.charAt(i);

      let product = this.products.find(x => x.code === code) as ProductViewModel;

      this.cart.cartItems.push({
        productId: product.id,
        name: product.name,
        code: product.code,
        price: product.price,
        volumeDiscountQuantity: product.volumeDiscountQuantity,
        volumeDiscountPrice: product.volumeDiscountPrice
      });
    }

    this.cartTotal = 0;
  }

  public removeFromCart(idx: number) {
    this.cart.cartItems.splice(idx, 1);

    this.cartTotal = 0;
  }

  public clearCart() {
    this.cart.cartItems = [];

    this.cartTotal = 0;
  }

  public calculateTotal() {
    this.http.post<number>(this.baseUrl + 'cart/calculateTotal', this.cart).subscribe(result => {
      this.cartTotal = result;
    }, error => console.error(error));
  }

  private getProducts(): Observable<ProductViewModel[]> {
    return this.http.get<ProductViewModel[]>(this.baseUrl + 'product');
  }
}
