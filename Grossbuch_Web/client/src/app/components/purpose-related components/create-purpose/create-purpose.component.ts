import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Purpose } from 'src/app/classes/purpose';
import { PurposeService } from 'src/app/services/PurposeService/purpose.service';

@Component({
  selector: 'app-create-purpose',
  templateUrl: './create-purpose.component.html',
  styleUrls: ['./create-purpose.component.css']
})
export class CreatePurposeComponent implements OnInit {

  creating: boolean = true;

  @Input() newPurpose: Purpose;

  constructor(private purposeService: PurposeService,
    private router: Router) { }

  ngOnInit() {
    this.newPurpose = new Purpose(-1, "");
    this.purposeService.changingPurposeForChange.subscribe((result: Purpose) => {
      this.newPurpose = result;
      this.creating = (this.newPurpose.id != -1) ? false : true;
    });
  }

  clickSave() {
    if(this.creating == true) {
      console.log("added operation: ", this.newPurpose);
      this.purposeService.createPurpose(this.newPurpose);
      this.purposeService.postPurpose(this.newPurpose).subscribe();
    } else {
      console.log("updated operation: ", this.newPurpose);
      this.purposeService.updatePurpose(this.newPurpose);
      this.purposeService.putPurpose(this.newPurpose).subscribe();
    }
    this.purposeService.changePurposeForChange(null);
  }

  clickDelete() {
    console.log("deleted purpose: ", this.newPurpose);
    this.purposeService.deletePurpose(this.newPurpose.id).subscribe();
    this.purposeService.changePurposeForChange(null);
  }

  clickCancel() {
    this.purposeService.changePurposeForChange(null);
  }
}
