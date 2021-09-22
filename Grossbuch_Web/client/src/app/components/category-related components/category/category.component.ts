import { Component, OnInit, Input } from '@angular/core';
import { Category } from 'src/app/classes/category';
import { CategoryService } from 'src/app/services/CategoryService/category.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.css']
})
export class CategoryComponent implements OnInit {

  @Input() category: Category;
  
  constructor(private categoryService: CategoryService) { }

  ngOnInit() {
  }

  changeCategory() {
    this.categoryService.changeCategoryForChange(this.category);
  }
}
