import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmissionComponent } from './emission/emission.component';
import { FilteringComponent } from './filtering/filtering.component';

@NgModule({
  declarations: [
    EmissionComponent, 
    FilteringComponent
  ],
  imports: [
    CommonModule
  ]
})
export class InvoiceModule { }
