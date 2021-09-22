import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { PurposeService } from 'src/app/services/PurposeService/purpose.service'

@Component({
  selector: 'app-purpose-list',
  templateUrl: './purpose-list.component.html',
  styleUrls: ['./purpose-list.component.css']
})
export class PurposeListComponent implements OnInit {

  purposes = [];

  constructor(private purposeService: PurposeService,
    private router: Router) { }

  ngOnInit() {
    this.purposeService.createdPurpose.subscribe((result) => {
      this.purposes.push(result);
    });

    this.purposeService.updatedPurpose.subscribe((result) => {
      for(let i=0; i<this.purposes.length; i++) {
        if(this.purposes[i].id == result.id) {
          this.purposes[i] = result;
        }
      }
    });

    this.getPurposes();
  }

  getPurposes(): void {
    this.purposeService.getPurposes()
    .subscribe(prodResp => {
      this.purposes = prodResp.results;
      console.log("Purposes: ", this.purposes);
    });
  }
}
