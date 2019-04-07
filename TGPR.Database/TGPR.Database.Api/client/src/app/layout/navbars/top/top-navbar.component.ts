import {Component, EventEmitter, HostBinding, Input, OnInit, Output} from '@angular/core';
import {AuthenticationService} from '../../../_services/authentication/authentication.service';
import {UserService} from '../../../_services/authentication/user.service';


@Component({
  moduleId: module.id,
  selector: 'tgpr-top-navbar',
  templateUrl: 'top-navbar.component.html',
  styleUrls: ['top-navbar.component.scss']
})
export class TopNavbarComponent implements OnInit {
  @HostBinding('class.app-navbar') appNavClass = true;
  @HostBinding('class.show-overlay') showOverlayClass = 'showOverlay';

  @Input() title: string;
  @Input() openedSidebar: boolean;
  @Output() sidebarState = new EventEmitter();
  showOverlay: boolean;
  email = '';

  constructor(private _userService: UserService, private _authService: AuthenticationService) {
    this.openedSidebar = false;
    this.showOverlay = false;
  }

  ngOnInit() {
    const user = this._userService.getUser();
    this.email = user.email;
  }

  open(event) {
    const clickedComponent = event.target.closest('.nav-item');
    const items = clickedComponent.parentElement.children;

    event.preventDefault();

    for (let i = 0; i < items.length; i++) {
      items[i].classList.remove('opened');
    }
    clickedComponent.classList.add('opened');

    // Add class 'show-overlay'
    this.showOverlay = true;
  }

  close(event) {
    const clickedComponent = event.target;
    const items = clickedComponent.parentElement.children;

    event.preventDefault();

    for (let i = 0; i < items.length; i++) {
      items[i].classList.remove('opened');
    }

    // Remove class 'show-overlay'
    this.showOverlay = false;
  }

  logout() {
    this._authService.logout();
  }

  openSidebar() {
    this.openedSidebar = !this.openedSidebar;
    this.sidebarState.emit();
  }
}
