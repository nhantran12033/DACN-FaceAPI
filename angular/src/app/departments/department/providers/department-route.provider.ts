import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const DEPARTMENTS_DEPARTMENT_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/departments',
        iconClass: 'fas fa-building',
        name: '::Menu:Departments',
        layout: eLayoutType.application,
        requiredPolicy: 'FaceAPI.Departments',
      },
    ]);
  };
}
