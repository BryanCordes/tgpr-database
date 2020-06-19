import {Component, HostBinding, OnInit} from '@angular/core';
import {IMenuItem} from '../../../../_models/layout/menu-item';
import {SecurityActivityEnum} from '../../../../_models/login/security-activity.enum';
import {UserService} from '../../../../_services/authentication/user.service';

@Component({
  moduleId: module.id,
  selector: 'tgpr-side-menu',
  templateUrl: './side-menu.component.html',
  styleUrls: ['./side-menu.component.scss']
})
export class SideMenuComponent implements OnInit {
  @HostBinding('class') appMenu = 'app-menu';

  menuItems: IMenuItem[];

  constructor(private _service: UserService) { }

  getMenuItems(): void {
    this.menuItems = [
      {
        'title': 'Dashboard',
        'icon': {
          'class': 'fa fa-home',
          'bg': '#ea8080',
          'color': 'rgba(0,0,0,.87)'
        },
        'routing': '/dashboard'
      }, {
        'title': 'Applications',
        'icon': {
          'class': 'fa fa-tasks',
          'bg': '#b3e5fc',
          'color': 'rgba(0,0,0,.87)'
        },
        'activities': [
          SecurityActivityEnum.CompanionApplicationRead
        ],
        'sub': [
          {
            'title': 'Companion',
            'routing': '/applications/companion',
            'activities': [
              SecurityActivityEnum.ApplicationTemplateRead
            ]
          }
        ]
      }, {
        'title': 'Administration',
        'icon': {
          'class': 'fa fa-desktop',
          'bg': '#9E9E9E',
          'color': 'rgba(0,0,0,.87)'
        },
        'activities': [
          SecurityActivityEnum.ApplicationTemplateWrite,
          SecurityActivityEnum.UserWrite,
          SecurityActivityEnum.UserRoleWrite
        ],
        'sub': [
          {
            'title': 'Applications',
            'routing': '/administration/applications',
            'nonMenuChildren': [
              '/administration/application/'
            ],
            'activities': [
              SecurityActivityEnum.ApplicationTemplateWrite
            ]
          }, {
            'title': 'Users',
            'routing': '/administration/users',
            'activities': [
              SecurityActivityEnum.UserWrite
            ],
            'nonMenuChildren': [
              '/administration/users/create',
              '/administration/user/'
            ],
          }, {
            'title': 'User Roles',
            'routing': '/administration/roles',
            'activities': [
              SecurityActivityEnum.UserRoleWrite
            ]
          }
        ]
      }
    ];
  }

  getLiClasses(item: any, rla: any) {
    return {
      'has-sub': item.sub,
      'active': item.active || rla.isActive || this.isChild(item, rla) || this.hasActiveChild(item, rla),
      'menu-item-group': item.groupTitle,
      'disabled': item.disabled,
      'collapse': !this.isVisible(item)
    };
  }

  private isVisible(item: any) {
    const activities: SecurityActivityEnum[] = item.activities;

    const isVisible = this._service.hasAnyActivity(activities);

    return isVisible;
  }

  private hasActiveChild(item: any, rla: any) {
    const menuItem: IMenuItem = item;
    if (!menuItem || !menuItem.sub) {
      return false;
    }

    for (const subItem of menuItem.sub) {
      if(this.isChild(subItem, rla)){
        return true;
      }
    }

    return false;
  }

  private isChild(item: any, rla: any) {
    if(!rla || !rla.router || !rla.router.url || !item.nonMenuChildren) {
      return false;
    }

    let url: string = rla.router.url;

    for (let child of item.nonMenuChildren) {
      if(url.indexOf(child) >= 0) {
        return true;
      }
    }

    return false;
  }

  getStyles(item: any) {
    return {
      'background': item.bg,
      'color': item.color
    };
  }

  ngOnInit(): void {
    this.getMenuItems();
  }

  toggle(event: Event, item: any, el: any) {
    event.preventDefault();

    const items: any[] = el.menuItems;

    if (item.active) {
      item.active = false;
    } else {
      for (let i = 0; i < items.length; i++) {
        items[i].active = false;
      }
      item.active = true;
    }
  }
}
