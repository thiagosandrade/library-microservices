import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [
  { 
    path: 'list-user', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./list-user/list-user.module').then(m => m.ListUserModule)
  },
  { 
    path: 'add-user', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./add-user/add-user.module').then(m => m.AddUserModule)
  },
  { 
    path: 'edit-user', 
    canActivate: [AuthGuard],
    loadChildren: () => import('./edit-user/edit-user.module').then(m => m.EditUserModule)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
