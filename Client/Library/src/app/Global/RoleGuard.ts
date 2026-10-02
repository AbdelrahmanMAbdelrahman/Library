import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthStateService } from '../Features/Auth/Services/AuthStateService';

export const RoleGuard: CanActivateFn = (route) => {

    const authState = inject(AuthStateService);
    const router = inject(Router);

    const requiredRole = route.data['role'];

    const userRole = authState.getRole();

    if (userRole === requiredRole) {
        return true;
    }

    return router.createUrlTree(['/Unauthorized']);
};