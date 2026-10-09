import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ContainerComponent } from './layout/container/container.component';

@Component({
  imports: [RouterOutlet, ContainerComponent],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  protected readonly title = signal('contacts');
}
