import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
    {path: '', redirectTo: 'login', pathMatch:'full'},
    {
        path:'login',
        loadComponent: () => import('./pages/auth/login/login.component')
        .then(m => m.LoginComponent)
    },
    {
    path: 'dashboard',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/dashboard/dashboard.component')
      .then(m => m.DashboardComponent)
  },
  {
    path: 'inventario',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Administrador', 'Recepcionista'] },
    loadComponent: () => import('./pages/inventario/inventario.component')
      .then(m => m.InventarioComponent)
  },
  {
    path: 'citas',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/citas/citas.component')
      .then(m => m.CitasComponent)
  },
  {
    path: 'ventas',
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Administrador', 'Recepcionista'] },
    loadComponent: () => import('./pages/ventas/ventas.component')
      .then(m => m.VentasComponent)
  },
  { path: '**', redirectTo: 'dashboard' }

];
