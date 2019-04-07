import {Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {RoleModel} from '../../../_models/administration/users/role.model';


@Component({
  selector: 'tgpr-edit-role-dialog',
  templateUrl: './edit-role-dialog.component.html'
})
export class EditRoleDialogComponent {

  role: RoleModel;

  constructor(public dialogRef: MatDialogRef<EditRoleDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: RoleModel) {
    this.role = data;
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}
