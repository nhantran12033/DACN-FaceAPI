import { Injectable, inject } from '@angular/core';
import { Router } from '@angular/router';
import { RecoveryCodeService } from '../services';

@Injectable()
export class RecoveryCodeGuard {
  protected readonly router = inject(Router);
  protected readonly recoveryCodeService = inject(RecoveryCodeService);

  canActivate() {
    const urlTree = this.router.createUrlTree(['/account/login']);
    return this.recoveryCodeService.hasData() ? true : urlTree;
  }
}
