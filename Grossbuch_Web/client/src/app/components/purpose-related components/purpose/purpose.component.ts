import { Component, OnInit, Input } from '@angular/core';
import { Purpose } from 'src/app/classes/purpose';
import { PurposeService } from 'src/app/services/PurposeService/purpose.service';

@Component({
  selector: 'app-purpose',
  templateUrl: './purpose.component.html',
  styleUrls: ['./purpose.component.css']
})
export class PurposeComponent implements OnInit {

  @Input() purpose: Purpose;

  constructor(private purposeService: PurposeService) { }

  ngOnInit() {
  }

  changePurpose() {
    this.purposeService.changePurposeForChange(this.purpose);
  }

}
