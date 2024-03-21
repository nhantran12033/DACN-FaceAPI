import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const SALARIES_SALARY_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/salaries',
        iconClass: 'fas fa-money-bill',
        name: '::Menu:Salaries',
        layout: eLayoutType.application,
        requiredPolicy: 'FaceAPI.Salaries',
      },
    ]);
  };
}
