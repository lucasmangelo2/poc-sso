import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';

export const MATERIAL_MODUELS = [
  MatSidenavModule,
  MatToolbarModule,
  MatIconModule
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ...MATERIAL_MODUELS
  ],
  exports: [
    ...MATERIAL_MODUELS
  ]
})
export class SharedModule { }
