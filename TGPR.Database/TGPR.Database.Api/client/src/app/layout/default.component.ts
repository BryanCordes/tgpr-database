import {SharedService} from './shared.service';
import {Component, Input, OnInit} from '@angular/core';


@Component({
  moduleId: module.id,
  selector: 'tgpr-layout',
  templateUrl: 'default.component.html',
  styleUrls: ['default.component.scss'],
  providers: [ SharedService ]
})
export class DefaultLayoutComponent implements OnInit {
  pageTitle: any;
  boxed: boolean;
  compress: boolean;
  menuStyle: string;
  rtl: boolean;
  @Input() openedSidebar: boolean;

  constructor( private _sharedService: SharedService ) {
    this.openedSidebar = false;
    this.boxed = false;
    this.compress = false;
    this.menuStyle = 'style-3';
    this.rtl = false;

    _sharedService.changeEmitted$.subscribe(
      title => {
        this.pageTitle = title;
      }
    );
  }

  ngOnInit() { }

  getClasses() {
    const menu: string = (this.menuStyle);

    return {
      ['menu-' + menu]: menu,
      'boxed': this.boxed,
      'compress-vertical-navbar': this.compress,
      'open-sidebar': this.openedSidebar,
      'rtl': this.rtl
    };
  }

  sidebarState() {
    this.openedSidebar = !this.openedSidebar;
  }
}
