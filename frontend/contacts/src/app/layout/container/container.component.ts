import { Component } from '@angular/core';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-container',
  template: `
    <div class="container">
      <p-card class="max-w-sm w-full">
        <ng-content />
      </p-card>
    </div>
  `,
  styles: [`
    .container {
      max-width: 1280px;
      width: 100%;
      margin: 2rem auto;
      padding: 0 2rem;
    }
  `],
  imports: [CardModule]
})
export class ContainerComponent {}