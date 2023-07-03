import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./routings/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'home',
    loadChildren: () =>
      import('./routings/home/home.module').then((m) => m.HomeModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'activities',
    loadChildren: () =>
      import('./routings/activity/activity.module').then(
        (m) => m.ActivityModule
      ),
    canActivate: [AuthGuard],
  },
  {
    path: 'sessions',
    loadChildren: () =>
      import('./routings/session/session.module').then((m) => m.SessionModule),
    canActivate: [AuthGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
