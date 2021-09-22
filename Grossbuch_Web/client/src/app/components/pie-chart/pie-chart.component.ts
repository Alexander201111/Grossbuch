import { Component, OnInit } from '@angular/core';
import { ChartType, ChartOptions } from 'chart.js';
import { Label } from 'ng2-charts';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { PurposeService } from 'src/app/services/PurposeService/purpose.service';
import { AccountService } from 'src/app/services/AccountService/account.service';
import { CategoryService } from 'src/app/services/CategoryService/category.service';

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css']
})

export class PieChartComponent implements OnInit {

  accounts = [];
  categories = [];
  purposes = [];

  accountChecked = true;
  categoryChecked = false;
  purposeChecked = false;

  // Pie
  public pieChartOptions: ChartOptions = {
    responsive: true,
    legend: {
      position: 'top',
    },
    plugins: {
      datalabels: {
        formatter: (value, ctx) => {
          const label = ctx.chart.data.labels[ctx.dataIndex];
          return label;
        },
      },
    }
  };

  public pieChartLabels: Label[] = [];
  public pieChartData: number[] = [];
  public pieChartType: ChartType = 'pie';
  public pieChartLegend = true;
  public pieChartPlugins = [pluginDataLabels];
  public pieChartColors = [
    {
      backgroundColor: [
        'rgba(255,0,0,0.3)',
        'rgba(0,255,0,0.3)',
        'rgba(0,0,255,0.3)',
        'rgba(255,255,0,0.3)',
        'rgba(127,255,0,0.3)',
        'rgba(0,255,212,0.3)',
        'rgba(0,242,255,0.3)',
        'rgba(0,157,255,0.3)',
        'rgba(131,0,255,0.3)',
        'rgba(255,0,255,0.3)',
        'rgba(255,0,144,0.3)'
      ],
    },
  ];

  constructor(private accountService: AccountService,
    private categoryService: CategoryService,
    private purposeService: PurposeService) { }

  ngOnInit() {
    this.getAccounts();
    this.getCategories();
    this.getPurposes();
  }

  getAccounts(): void {
    this.accountService.getAccounts()
    .subscribe(prodResp => {
      this.accounts = prodResp.results;
      this.addToChart(this.accounts);
    });
  }

  getCategories(): void {
    this.categoryService.getCategories()
    .subscribe(prodResp => {
      this.categories = prodResp.results;
      this.addToChart(this.categories);
    });
  }

  getPurposes(): void {
    this.purposeService.getPurposes()
    .subscribe(prodResp => {
      this.purposes = prodResp.results;
    });
  }

  changeRadio(type: number) {
    if(type == 1) {
      this.accountChecked = true;
      this.categoryChecked = false;
      this.purposeChecked = false;
      this.addToChart(this.accounts);
    }
    if(type == 2) {
      this.accountChecked = false;
      this.categoryChecked = true;
      this.purposeChecked = false;
      this.addToChart(this.categories);
    }
    if(type == 3) {
      this.accountChecked = false;
      this.categoryChecked = false;
      this.purposeChecked = true;
      this.addToChart(this.purposes);
    }
  }

  addToChart(obj: any) {
    console.log("added to chart: ", obj);
    this.pieChartLabels = [];
    this.pieChartData = [];
    for(let i=0; i<obj.length; i++) {
      this.pieChartLabels.push(obj[i].title);
      this.pieChartData.push(obj[i].totalSum);
    }
  }

}
