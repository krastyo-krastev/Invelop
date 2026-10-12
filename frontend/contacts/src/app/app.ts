import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ContainerComponent } from './layout/container/container.component';
import { Toast } from 'primeng/toast';

@Component({
  imports: [
    RouterOutlet, 
    ContainerComponent,
    Toast
  ],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('contacts');
}
