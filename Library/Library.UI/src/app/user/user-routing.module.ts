import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../_guards/auth.guard';

const routes: Routes = [
  { 
    path: 'list-user', 
    canActivate: [AuthGuard],
    loadChildren: './list-user/list-user.module#ListUserModule'
  },
  // { 
  //   path: 'add-user', 
  //   canActivate: [AuthGuard],
  //   loadChildren: './add-user/add-user.module#AddUserModule'
  // },
  // { 
  //   path: 'edit-user', 
  //   canActivate: [AuthGuard],
  //   loadChildren: './edit-user/edit-user.module#EditUserModule'
  // }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
