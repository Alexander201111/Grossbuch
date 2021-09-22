import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Operation } from 'src/app/classes/operation';
import { Router } from '@angular/router';
import { OperationService } from 'src/app/services/OperationService/operation.service';

@Component({
  selector: 'app-operation',
  templateUrl: './operation.component.html',
  styleUrls: ['./operation.component.css']
})
export class OperationComponent implements OnInit {

  @Input() operation: Operation;
  @Output() deletedOperation = new EventEmitter<number>();

  constructor(private router: Router, private operationService: OperationService) { }

  ngOnInit() {

  }

  changeOperation() {
    this.operationService.operationForChange = this.operation;
    this.router.navigate(['newoperation']);
  }

  deleteOperation() {
    console.log("deleted operation: ", this.operation);
    this.operationService.deleteOperation(this.operation.id).subscribe((data) => { /* console.log(data); */ });
    this.deletedOperation.emit(this.operation.id);
  }

}