import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EmissionComponent } from './invoice/emission/emission.component';
import { FilteringComponent } from './invoice/filtering/filtering.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'invoice-emission', component: EmissionComponent},
  {path: 'invoice-filter', component: FilteringComponent},
  {path: '**', redirectTo: 'home'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
