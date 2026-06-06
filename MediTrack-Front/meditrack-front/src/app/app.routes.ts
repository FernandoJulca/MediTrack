import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';
import { LayoutPublicoComponent } from './shared/layout-publico/layout-publico.component';
import { LayoutPrivadoComponent } from './shared/layout-privado/layout-privado.component';
import { LayoutRecepcionComponent } from './shared/layout-recepcion/layout-recepcion.component';
import { LayoutDoctorComponent } from './shared/layout-doctor/layout-doctor.component';

export const routes: Routes = [

  // ── Rutas públicas ─────────────────────────────────
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

  // ── Auth ────────────────────────────────────────────
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

  // ── Redirección por rol ─────────────────────────────
  {
    path: 'dashboard',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/dashboard/dashboard.component')
      .then(m => m.DashboardComponent)
  },

  // ── Paciente ────────────────────────────────────────
  {
    path: 'paciente',
    component: LayoutPrivadoComponent,
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Paciente'] },
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadComponent: () => import('./pages/paciente/dashboard/dashboard-paciente/dashboard-paciente.component')
          .then(m => m.DashboardPacienteComponent)
      },
      {
        path: 'citas',
        loadComponent: () => import('./pages/paciente/citas/citas-paciente/citas-paciente.component')
          .then(m => m.CitasPacienteComponent)
      },
      {
        path: 'agendar',
        loadComponent: () => import('./pages/paciente/agendar/agendar-cita/agendar-cita.component')
          .then(m => m.AgendarCitaComponent)
      },
      {
        path: 'informes',
        loadComponent: () => import('./pages/paciente/informes//informes-paciente/informes-paciente.component')
          .then(m => m.InformesPacienteComponent)
      },
      {
        path: 'perfil',
        loadComponent: () => import('./pages/paciente/perfil//perfil-paciente/perfil-paciente.component')
          .then(m => m.PerfilPacienteComponent)
      }
    ]
  },

  // ── Recepcionista ───────────────────────────────────
  {
    path: 'recepcion',
    component: LayoutRecepcionComponent,
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Recepcionista', 'Administrador'] },
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadComponent: () => import('./pages/recepcion/dashboard/dashboard-recepcion.component')
          .then(m => m.DashboardRecepcionComponent)
      },
      {
        path: 'citas',
        loadComponent: () => import('./pages/recepcion/citas/citas-recepcion.component')
          .then(m => m.CitasRecepcionComponent)
      },
      {
        path: 'pacientes',
        loadComponent: () => import('./pages/recepcion/pacientes/pacientes-recepcion.component')
          .then(m => m.PacientesRecepcionComponent)
      }
    ]
  },

  // ── Doctor ──────────────────────────────────────────
  {
    path: 'doctor',
    component: LayoutDoctorComponent,
    canActivate: [authGuard, roleGuard],
    data: { roles: ['Doctor'] },
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadComponent: () => import('./pages/doctor/dashboard/dashboard-doctor.component')
          .then(m => m.DashboardDoctorComponent)
      },
      {
        path: 'citas',
        loadComponent: () => import('./pages/doctor/citas/citas-doctor.component')
          .then(m => m.CitasDoctorComponent)
      },
      {
        path: 'atencion',
        loadComponent: () => import('./pages/doctor/atencion/atencion-doctor.component')
          .then(m => m.AtencionDoctorComponent)
      }
    ]
  },

  { path: '**', redirectTo: '' }
];