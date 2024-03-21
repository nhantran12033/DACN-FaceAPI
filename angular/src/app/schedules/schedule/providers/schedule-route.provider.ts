import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const SCHEDULES_SCHEDULE_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/schedules',
        iconClass: 'fas fa-calendar-alt',
        name: '::Menu:Schedules',
        layout: eLayoutType.application,
        requiredPolicy: 'FaceAPI.Schedules',
      },
    ]);
  };
}
