import { Component, OnInit } from "@angular/core";
import { switchMap } from "rxjs/operators";
import { AuthService } from "../../authentication/auth.service";

@Component({
  selector: "login",
  template: "",
})
export class LoginComponent implements OnInit {
  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.checkAuth().subscribe((isAuthorized) => {
      if (!isAuthorized) {
        this.authService.startAuthentication();
      }
    });
  }
}
