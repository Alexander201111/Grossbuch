import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { CategoryService } from 'src/app/services/CategoryService/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {

  categories = [];

  constructor(private categoryService: CategoryService,
    public dialog: MatDialog,
    private router: Router) { }

  ngOnInit() {
    this.categoryService.createdCategory.subscribe((result) => {
      this.categories.push(result);
    });

    this.categoryService.updatedCategory.subscribe((result) => {
      for(let i=0; i<this.categories.length; i++) {
        if(this.categories[i].id == result.id) {
          this.categories[i] = result;
        }
      }
    });

    this.getCategories();
  }

  getCategories(): void {
    this.categoryService.getCategories()
    .subscribe(prodResp => 
      this.categories = prodResp.results
    );
  }
}
