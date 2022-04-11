import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {EditBookComponent} from "./edit-book.component";


const routes: Routes = [
  { path: '', component: EditBookComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EditBookRoutingModule { }
