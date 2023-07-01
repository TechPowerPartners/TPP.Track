import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AuthInterceptor } from '@shared/interceptors/auth.interceptor';

@NgModule({
  declarations: [],
  imports: [HttpClientModule],
  exports: [],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ],
})
export class ServiceProxyModule {}
