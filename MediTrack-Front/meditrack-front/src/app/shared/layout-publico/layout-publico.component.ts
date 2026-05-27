import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarPublicoComponent } from '../navbar-publico/navbar-publico.component';
import { FooterComponent } from '../footer/footer.component';

@Component({
  selector: 'app-layout-publico',
  standalone: true,
  imports: [RouterOutlet, NavbarPublicoComponent, FooterComponent],
  template: `
    <app-navbar-publico></app-navbar-publico>
    <main>
      <router-outlet></router-outlet>
    </main>
    <app-footer></app-footer>
  `
})
export class LayoutPublicoComponent {}