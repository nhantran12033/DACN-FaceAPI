import { Component, ChangeDetectionStrategy, Input } from '@angular/core';
import { NgIf } from '@angular/common';
import { QRCodeModule } from 'angularx-qrcode';
import { QRCodeErrorCorrectionLevel } from 'qrcode';
import { CoreModule } from '@abp/ng.core';

@Component({
  standalone: true,
  selector: 'abp-qr-code',
  template: `
    <ng-container *ngIf="qrData as data; else notFound">
      <qrcode
        [qrdata]="data"
        [width]="width || 256"
        [errorCorrectionLevel]="errorCorrectionLevel || 'M'"
      ></qrcode>
    </ng-container>

    <ng-template #notFound>
      <div class="text-center text-muted">
        <i class="fa fa-qrcode fa-5x"></i>
      </div>
    </ng-template>
  `,
  imports: [NgIf, QRCodeModule, CoreModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class QrCodeComponent {
  @Input() qrData: string | undefined;
  @Input() width: number | undefined;
  @Input() errorCorrectionLevel: QRCodeErrorCorrectionLevel | undefined;
}
