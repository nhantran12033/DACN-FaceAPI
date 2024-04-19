import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ChangePasswordService } from '../../services/change-password.service';
import { SubscriptionService } from '@abp/ng.core';
import { PasswordComplexityIndicatorService } from '../../services/password-complexity-indicator.service';
import { ProgressBarStats } from '../../models/password-complexity';
import { ChangePasswordFormModel } from '../../models/changePasswordFormModel';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'abp-change-password-form',
  templateUrl: './change-password.component.html',
  exportAs: 'abpChangePasswordForm',
  providers: [SubscriptionService],
})
export class ChangePasswordComponent implements OnInit {
  private readonly service = inject(ChangePasswordService);
  readonly #destroyRef = inject(DestroyRef);

  protected readonly passwordComplexityService = inject(PasswordComplexityIndicatorService);
  protected readonly subscription = inject(SubscriptionService);

  form: FormGroup<ChangePasswordFormModel>;
  inProgress: boolean;
  progressBar: ProgressBarStats;
  public hideCurrentPassword = false;

  mapErrorsFn = this.service.MapErrorsFnFactory();

  ngOnInit(): void {
    this.hideCurrentPassword = !this.service.hasPassword;
    this.form = this.service.buildForm(this.hideCurrentPassword);
  }

  onSuccess() {
    this.service.showSuccessMessage();
    this.hideCurrentPassword = false;
    this.form = this.service.buildForm(this.hideCurrentPassword);
  }

  onSubmit() {
    if (this.form.invalid) return;
    const input = this.form.value;
    this.service
      .changePassword({ currentPassword: input.currentPassword, newPassword: input.newPassword })
      .pipe(takeUntilDestroyed(this.#destroyRef))
      .subscribe({ next: () => this.onSuccess(), error: e => this.service.showErrorMessage(e) });
  }

  get newPassword(): string {
    return this.form.value.newPassword;
  }

  validatePassword() {
    this.progressBar = this.passwordComplexityService.validatePassword(this.newPassword);
  }
}
