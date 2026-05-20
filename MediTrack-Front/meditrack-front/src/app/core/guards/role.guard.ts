import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const auth = inject(AuthService);
  const router = inject(Router);
  const rolesPermitidos: string[] = route.data['roles'] ?? [];

  if (auth.tieneRol(...rolesPermitidos)) return true;

  router.navigate(['/dashboard']);
  return false;
};