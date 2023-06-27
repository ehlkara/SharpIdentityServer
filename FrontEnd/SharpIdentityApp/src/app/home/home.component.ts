import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private authService:AuthService) {
  }
  status: string | undefined;
  async ngOnInit(): Promise<void> {
    const user = await this.authService.userManager.getUser();
    if (user) {
      console.log(user);
      this.status = 'Welcome';
    }
    else {
      this.status = 'Not authenticate!';
    }
  }
  

  login(): void {
    this.authService.userManager.signinRedirect();
  }

  logout(): void {
    this.authService.userManager.signoutRedirect();
  }
}
