import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ChatWidgetComponent } from './chat-widget';

@Component({
  selector: 'app-layout',
  imports: [RouterOutlet, RouterLink, ChatWidgetComponent],
  templateUrl: './layout.html',
  styleUrl: './layout.css',
})
export class LayoutComponent {

}
