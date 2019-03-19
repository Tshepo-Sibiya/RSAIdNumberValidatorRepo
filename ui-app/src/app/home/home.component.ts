import { Component, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormControl, FormBuilder, Validators, FormArray } from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorService } from '../core/services/validator.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  idObj: any;
  failed = [];
  passed = [];
  linesToSubmit: any[];
  NoLoadedItems = false;
  loading = false;
  dataLoad = null;

  public idRows: Array<{ IdentityNumber: number, isValidNumber: boolean }>;
  constructor(private validatorService: ValidatorService, private route: Router, private fb: FormBuilder) { }

  // LoadFile(event) {
  //   // Check for the various File API support.
  //   if (event) {
  //     // FileReader are supported.
  //     this.getAsText(event);
  //   } else {
  //     alert('FileReader are not supported in this browser.');
  //   }
  // }

  // getAsText(event) {
  //   const reader = new FileReader();
  //   // Read file into memory as UTF-8
  //   reader.readAsText(event.target.files[0]);
  //   // Handle errors load
  //   reader.onload = this.loadHandler;
  //   reader.onerror = this.errorHandler;
  // }

  // loadHandler(event) {
  //   const csv = event.target.result;

  //   let i: number;
  //   let j: number;
  //   const allTextLines = csv.split(/\r\n|\n/);
  //   const lines = [];
  //   for (i = 0; i < allTextLines.length; i++) {
  //     const data = allTextLines[i].split(/\r\n|\n/);
  //     const tarr = [];
  //     for (j = 0; j < data.length; j++) {
  //       tarr.push(data[j]);
  //     }
  //     lines.push(tarr);
  //   }
  //   this.linesToSubmit = lines;
  //   // this.linesToSubmit.splice(-1, 1);
  //   this.linesToSubmit.forEach((item, index) => {
  //     if (this.linesToSubmit[index][0] === '') {
  //       this.linesToSubmit.splice(index, 1);
  //     }
  //   });
  //   if (this.linesToSubmit.length > 0) {
  //     this.linesToSubmit.forEach(item => {
  //       this.idRows.push({
  //         IdentityNumber: parseInt(item[0], 16),
  //         isValidNumber: true
  //       });
  //     });

  //   }
  // }

  // errorHandler(evt) {
  //   if (evt.target.error.name === 'NotReadableErro') {
  //     alert('Cannot read file !');
  //   }
  // }


  onSubmit() {
    // const idsFromFile = JSON.parse(localStorage.getItem('ids'));
    // this.loadHandler(event);
    // if (this.linesToSubmit != null) {
    //   console.log(this.linesToSubmit);
    // }
    this.NoLoadedItems = false;
    this.failed = [];
    this.passed = [];

    this.idRows.forEach((item, index) => {
      if (item.IdentityNumber == null && index > 0) {
        this.idRows.splice(index, 1);
      }

      if (isNaN(item.IdentityNumber)) {
        item.isValidNumber = false;
      }
    });
    if (this.idRows.length > 0 && this.idRows[0].IdentityNumber != null) {
      this.loading = true;

      this.validatorService.validateIdNumber(this.idRows).subscribe((data: any) => {
        const response = data;
        response.forEach(element => {
          this.loading = false;
          if (element.failureReason != null) {
            this.failed.push(element);
          } else {
            this.passed.push(element);
          }
          this.NoLoadedItems = false;
        });
      });

    } else {
      this.NoLoadedItems = true;
    }

  }

  addAnother(): void {
    this.idRows.push({ IdentityNumber: null, isValidNumber: true });
  }

  remove(index: number): void {
    this.idRows.splice(index, 1);
  }

  ngOnInit() {
    this.idRows = [
      {
        IdentityNumber: null,
        isValidNumber: true
      }
    ];
  }


}
