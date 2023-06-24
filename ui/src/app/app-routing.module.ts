import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./routings/home/home.module').then((m) => m.HomeModule),
  },
  {
    path: 'activities',
    loadChildren: () =>
      import('./routings/activity/activity.module').then(
        (m) => m.ActivityModule
      ),
  },
  {
    path: 'sessions',
    loadChildren: () =>
      import('./routings/session/session.module').then((m) => m.SessionModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
