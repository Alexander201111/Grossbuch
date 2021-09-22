import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

import { OperationService } from '../../../services/OperationService/operation.service'

@Component({
  selector: 'app-operation-list',
  templateUrl: './operation-list.component.html',
  styleUrls: ['./operation-list.component.css']
})
export class OperationListComponent implements OnInit {

  operations = [];

  subscription: Subscription;

  constructor(private operationService: OperationService) { }

  ngOnInit() {
    this.getOperations();

    this.operationService.createdOperation.subscribe((result) => {
      this.operations.push(result);
    });

    this.operationService.updatedOperation.subscribe((result) => {
      for (let i = 0; i < this.operations.length; i++) {
        if (this.operations[i].id == result.id) {
          this.operations[i] = result.id;
        }
      }
    });
  }

  getOperations(): void {
    this.operationService.getOperations()
      .subscribe(prodResp => {
        if (prodResp.results && prodResp.results.length != 0) {
          for (let i = 0; i < prodResp.results.length; i++) {
            if (i < 10) {
              this.operations.push(prodResp.results[i]);
            }
            else {
              break;
            }
          }
        }
        console.log("List: ", this.operations);
      });
  }

  deleteOperation(id) {
    for (let i = 0; i < this.operations.length; i++) {
      if (this.operations[i].id == id) {
        this.operations.splice(i, 1);
      }
    }
  }

}
