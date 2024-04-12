import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const TIMESHEETS_TIMESHEET_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/timesheets',
        iconClass: 'fas fa-table',
        name: '::Menu:Timesheets',
        layout: eLayoutType.application,
        requiredPolicy: 'FaceAPI.Timesheets',
      },
    ]);
  };
}
