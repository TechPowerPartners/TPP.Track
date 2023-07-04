import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './layout/layout.component';
import { ToastrModule } from '@node_modules/ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HeaderComponent } from './layout/header/header.component';
import { ServiceProxyModule } from 'src/service-proxies/service-proxies.module';
import { FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { AuthLayoutComponent } from '@routings/auth/auth-layout/auth-layout.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

@NgModule({
  declarations: [AppComponent, LayoutComponent, HeaderComponent, AuthLayoutComponent],
  imports: [
    NgSelectModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ServiceProxyModule,
    ToastrModule.forRoot({
      timeOut: 2000,
      preventDuplicates: true,
      positionClass: 'toast-bottom-right',
    }),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
