import {Component, HostBinding, OnInit} from '@angular/core';

@Component({
  moduleId: module.id,
  selector: 'tgpr-side-navbar',
  templateUrl: 'side-navbar.component.html',
  styleUrls: ['side-navbar.component.scss'],
})
export class SideNavbarComponent implements OnInit {
  @HostBinding('class') verticalNavbar = 'vertical-navbar';

  constructor() {}

  ngOnInit() {}
}
