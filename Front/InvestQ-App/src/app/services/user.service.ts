import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, ReplaySubject, take } from 'rxjs';
import { environment } from '@environments/environment';
import { User } from '@app/models/identity/User';
import { UserUpdate } from '@app/models/identity/UserUpdate';
import { TipoDeUsuario } from '@app/models/Enum/TipoDeUsuario.enum';

@Injectable()
export class UserService {

  private currentUserSource = new ReplaySubject<User>(1);

  public currentUser$ = this.currentUserSource.asObservable();

  baseUrl = environment.apiURL + 'api/User/';

  constructor(private http: HttpClient) { }

  public login(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'Login', model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user)
        }
      })
    );
  }

  public logout(): void {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentUserSource.complete();
  }

  public setCurrentUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  getUser(): Observable<UserUpdate> {
    return this.http.get<UserUpdate>(this.baseUrl + 'getUser').pipe(take(1));
  }

  public UpdateUser(model: UserUpdate): Observable<void> {
    return this.http.put<UserUpdate>(this.baseUrl + 'updateUser', model)
              .pipe(
                take(1),
                map((user: UserUpdate) => {
                  this.setCurrentUser(user);
                })
              )
  }

  public register(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'Register', model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user)
        }
      })
    );
  }

  getTipoDeUsuario() {
    return [
      {valor: TipoDeUsuario.NaoInformada, desc: 'NaoInformada' },
      {valor: TipoDeUsuario.Administrador, desc: 'Administrador' },
      {valor: TipoDeUsuario.Usuario, desc: 'Usuario' }
    ];
  }

}
