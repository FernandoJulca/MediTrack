import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { LayoutPublicoComponent } from './shared/layout-publico/layout-publico.component';

export const routes: Routes = [
  // Rutas públicas con layout
  {
    path: '',
    component: LayoutPublicoComponent,
    children: [
      {
        path: '',
        loadComponent: () => import('./pages/publico/inicio/inicio.component')
          .then(m => m.InicioComponent)
      },
      {
        path: 'sedes',
        loadComponent: () => import('./pages/publico/sedes/sedes.component')
          .then(m => m.SedesComponent)
      },
      {
        path: 'especialidades',
        loadComponent: () => import('./pages/publico/especialidades/especialidades.component')
          .then(m => m.EspecialidadesComponent)
      },
      {
        path: 'doctores',
        loadComponent: () => import('./pages/publico/doctores/doctores.component')
          .then(m => m.DoctoresComponent)
      }
    ]
  },

  // Rutas sin layout
  {
    path: 'login',
    loadComponent: () => import('./pages/auth/login/login.component')
      .then(m => m.LoginComponent)
  },
  {
    path: 'registro',
    loadComponent: () => import('./pages/auth/registro/registro.component')
      .then(m => m.RegistroComponent)
  },

  // Rutas protegidas (las construimos después)
  {
    path: 'dashboard',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/dashboard/dashboard.component')
      .then(m => m.DashboardComponent)
  },
  {
    path: 'citas',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/citas/citas.component')
      .then(m => m.CitasComponent)
  },

  { path: '**', redirectTo: '' }
];