import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AddAuthorComponent} from "./add-author.component";

const routes: Routes = [
  { path: '', component: AddAuthorComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AddAuthorRoutingModule { }
