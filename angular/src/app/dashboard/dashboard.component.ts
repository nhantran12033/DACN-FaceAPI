import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  template: `
    <app-host-dashboard *abpPermission="'FaceAPI.Dashboard.Host'"></app-host-dashboard>
    <app-tenant-dashboard *abpPermission="'FaceAPI.Dashboard.Tenant'"></app-tenant-dashboard>
  `,
})
export class DashboardComponent {}
