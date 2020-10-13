import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./account/login/login.component";
import { AuthGuard } from "./authentication/auth.guard";
import { ChatRoomComponent } from "./chat-room/chat-room.component";

const routes: Routes = [
  { path: "login", component: LoginComponent },
  {
    path: "chat",
    component: ChatRoomComponent,
    canActivate: [AuthGuard],
  },
  { path: "", redirectTo: "chat", pathMatch: "full" },
  { path: "**", redirectTo: "", pathMatch: "full" },
  { path: "**/**", redirectTo: "", pathMatch: "full" },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
