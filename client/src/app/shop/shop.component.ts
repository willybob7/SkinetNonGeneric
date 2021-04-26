import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  //static: false means it will appear after the products are available
  @ViewChild('search', {static: false}) searchTerm: ElementRef;
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  shopParams = new ShopParams();
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: Hight to Low', value: 'priceDesc'}
  ];

  constructor(private ShopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(): void{
    this.ShopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageSize = response.pageSize;
      this.shopParams.totalCount = response.count;

      if (this.shopParams.pageSize > this.shopParams.totalCount){
        this.shopParams.pageSize = this.shopParams.totalCount;
      }

    }, error => {
      console.log(error);
    });
  }

  getBrands(): void{
    this.ShopService.getBrands().subscribe(response => {
      this.brands = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  getTypes(): void{
    this.ShopService.getTypes().subscribe(response => {
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error);
    });
  }

  onBrandsSelected(brandId: number): void{
    this.shopParams.brandId = brandId;
    this.shopParams.page = 1;
    this.getProducts();
  }

  onTypesSelected(typeId: number): void{
    this.shopParams.typeId = typeId;
    this.shopParams.page = 1;
    this.getProducts();
  }

  onSortSelected(sort: string): void{
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageSelected(page: any): void{
    if (page !== this.shopParams.page){
      this.shopParams.page = page;
      this.getProducts();
    }
  }

  onSearch(): void{
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.page = 1;
    this.getProducts();
  }

  onReset(): void{
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
