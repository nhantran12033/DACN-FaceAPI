import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const SCHEDULE_FORMATS_SCHEDULE_FORMAT_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/schedule-formats',
        iconClass: 'fas fa-file-alt',
        name: '::Menu:ScheduleFormats',
        layout: eLayoutType.application,
        requiredPolicy: 'FaceAPI.ScheduleFormats',
      },
    ]);
  };
}
