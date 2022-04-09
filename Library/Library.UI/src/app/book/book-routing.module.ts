import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [
  { 
    path: 'add-book', 
    canActivate: [AuthGuard],
    loadChildren: './add-book/add-book.module#AddBookModule'
  },
  { 
    path: 'list-book', 
    canActivate: [AuthGuard],
    loadChildren: './list-book/list-book.module#ListBookModule'
  },
  { 
    path: 'edit-book', 
    canActivate: [AuthGuard],
    loadChildren: './edit-book/edit-book.module#EditBookModule'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookRoutingModule { }
