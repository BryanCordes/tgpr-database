import {Component} from '@angular/core';
import {RoleModel} from '../../../_models/administration/users/role.model';
import {MatDialogRef} from '@angular/material';

@Component({
  selector: 'tgpr-create-role-dialog',
  templateUrl: './create-role-dialog.component.html'
})
export class CreateRoleDialogComponent {

  role: RoleModel = {
    Name: '',
    SecurityActivities: []
  };

  constructor(public dialogRef: MatDialogRef<CreateRoleDialogComponent>) { }

  cancel(): void {
    this.dialogRef.close();
  }
}
