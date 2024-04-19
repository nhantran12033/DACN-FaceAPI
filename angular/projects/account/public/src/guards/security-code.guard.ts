import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { SecurityCodeService } from '../services/security-code.service';

@Injectable()
export class SecurityCodeGuard {
  constructor(private service: SecurityCodeService, private router: Router) {}

  canActivate() {
    const urlTree = this.router.createUrlTree(['/account/login']);
    return !!this.service.data ? true : urlTree;
  }
}
