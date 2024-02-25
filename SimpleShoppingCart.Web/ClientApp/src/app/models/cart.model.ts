export interface CartViewModel {
  cartItems: CartItem[];
}

interface CartItem {
  productId: number;

  name: string;

  code: string;

  price: number;

  volumeDiscountQuantity?: number;

  volumeDiscountPrice?: number;
}
