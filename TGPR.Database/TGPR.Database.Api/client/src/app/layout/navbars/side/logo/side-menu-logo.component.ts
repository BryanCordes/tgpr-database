import {Component, HostBinding, OnInit} from '@angular/core';

@Component({
  moduleId: module.id,
  selector: 'tgpr-logo',
  templateUrl: 'side-menu-logo.component.html',
  styleUrls: ['side-menu-logo.component.scss'],
})
export class SideMenuLogoComponent implements OnInit {
  @HostBinding('class') appLogo = 'app-logo';

  constructor() {}

  ngOnInit() {}
}
