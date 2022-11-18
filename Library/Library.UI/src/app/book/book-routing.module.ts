import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [
  { 
    path: 'add-book', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./add-book/add-book.module').then(m => m.AddBookModule)
  },
  { 
    path: 'list-book', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./list-book/list-book.module').then(m => m.ListBookModule)
  },
  { 
    path: 'edit-book', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./edit-book/edit-book.module').then(m => m.EditBookModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookRoutingModule { }
