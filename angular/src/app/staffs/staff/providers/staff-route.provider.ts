import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const STAFFS_STAFF_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/staffs',
        iconClass: 'fas fa-user',
        name: '::Menu:Staffs',
        layout: eLayoutType.application,
        requiredPolicy: 'FaceAPI.Staffs',
      },
    ]);
  };
}
