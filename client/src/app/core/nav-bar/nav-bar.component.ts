import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { Observable } from 'rxjs';
import { IBasket } from 'src/app/shared/models/basket';
import { AccountService } from '../../account/account.service';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  basket$: Observable<IBasket>;
  currentUser$: Observable<IUser>;

  constructor(private basketService: BasketService, private acccountService: AccountService) { }

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
    this.currentUser$ = this.acccountService.currentUser$;
  }

}
