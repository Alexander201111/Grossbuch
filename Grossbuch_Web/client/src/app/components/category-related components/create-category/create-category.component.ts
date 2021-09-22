import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/classes/category';
import { CategoryService } from 'src/app/services/CategoryService/category.service';

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.css']
})
export class CreateCategoryComponent implements OnInit {

  creating: boolean = true;

  @Input() newCategory: Category;

  constructor(private categoryService: CategoryService,
    private router: Router) { }

  ngOnInit() {
    this.newCategory = new Category(-1, "", 0);
    this.categoryService.changingCategoryForChange.subscribe((result: Category) => {
      this.newCategory = result;
      this.creating = (this.newCategory.id != -1) ? false : true;
    });
  }

  clickSave() {
    if(this.creating == true) {
      console.log("added operation: ", this.newCategory);
      this.categoryService.createCategory(this.newCategory);
      this.categoryService.postCategory(this.newCategory).subscribe();
    } else {
      console.log("updated operation: ", this.newCategory);
      this.categoryService.updateCategory(this.newCategory);
      this.categoryService.putCategory(this.newCategory).subscribe();
    }
    this.categoryService.changeCategoryForChange(null);
  }

  clickDelete() {
    console.log("deleted category: ", this.newCategory);
    this.categoryService.deleteCategory(this.newCategory.id).subscribe();
    this.categoryService.changeCategoryForChange(null);
  }

  clickCancel() {
    this.categoryService.changeCategoryForChange(null);
  }
}
