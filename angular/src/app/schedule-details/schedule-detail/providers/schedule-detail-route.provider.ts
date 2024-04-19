import { eLayoutType, RoutesService } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const SCHEDULE_DETAILS_SCHEDULE_DETAIL_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routes: RoutesService) {
  return () => {
    routes.add([
      {
        path: '/schedule-details',
        iconClass: 'fas fa-file-alt',
        name: '::Menu:ScheduleDetails',
        layout: eLayoutType.application,
        requiredPolicy: 'FaceAPI.ScheduleDetails',
      },
    ]);
  };
}
