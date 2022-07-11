import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Carteira } from '@app/models/Carteira';
import { Cliente } from '@app/models/Cliente';
import { Corretora } from '@app/models/Corretora';
import { CarteiraService } from '@app/services/carteira.service';
import { ClienteService } from '@app/services/cliente.service';
import { CorretoraService } from '@app/services/corretora.service';
import { LancamentoService } from '@app/services/lancamento.service';
import { Guid } from 'guid-typescript';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cliente-carteira-detalhe',
  templateUrl: './cliente-carteira-detalhe.component.html',
  styleUrls: ['./cliente-carteira-detalhe.component.scss']
})
export class ClienteCarteiraDetalheComponent implements OnInit {

  public carteira = {} as Carteira;
  public corretoras: Corretora[] = [];
  public clientes: Cliente[] = [];

  carteiraId: Guid;
  clienteId: Guid;
  clienteNome: string;

  form!: FormGroup;

  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }

  constructor(private fb: FormBuilder,
              private activatedRouter: ActivatedRoute,
              private localeService: BsLocaleService,
              private carteiraService: CarteiraService,
              private lancamentoService: LancamentoService,
              private corretoraService: CorretoraService,
              private clienteService: ClienteService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br');
  }

  public bucarNomeDoCliente(clienteId: Guid): void {
    this.clienteId = clienteId;
    this.clienteService.getClienteById(this.clienteId).subscribe({
      next: (cliente: Cliente) => {
        this.clienteNome = cliente.nomeCompleto;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar o nome do cliente.', 'Erro!');
        console.error(error)
      },
      complete: () => {this.spinner.hide()}
    });
  }

  public carregarCorretoras(): void {
    const observer = {
      next: (_corretoras: Corretora[]) => {
        this.corretoras = _corretoras;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.corretoraService.getAllCorretoras().subscribe(observer);
  }

  public carregarClientesUser(): void {
    const observer = {
      next: (_clientes: Cliente[]) => {
        this.clientes = _clientes;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.clienteService.getAllClientesUser('usuarioLogado').subscribe(observer);
  }

  public carregarCarteira(): void {
    //alert(this.activatedRouter.snapshot.paramMap.get('clienteid'));
    //alert(this.activatedRouter.snapshot.paramMap.get('id'));

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      {
        this.carteiraId = Guid.createEmpty();

        this.bucarNomeDoCliente(Guid.parse(this.activatedRouter.snapshot.paramMap.get('clienteid').toString()));
      }
    else {
      //alert('tesets');
      this.carteiraId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.carteiraId !== null && !this.carteiraId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.carteiraService.getCarteiraById(this.carteiraId).subscribe({
          next: (carteira: Carteira) => {
            this.carteira = {...carteira};
            this.bucarNomeDoCliente(this.carteira.clienteId);
            this.form.patchValue(this.carteira);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar a carteira.', 'Erro!');
            console.error(error)
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }

  ngOnInit(): void {
    this.carregarCarteira();
    this.carregarCorretoras();
    this.carregarClientesUser();
    this.carteiraPossuiLancamento();
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      descricao: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      saldo: [0, [Validators.required]],
      dataDeAtualizadoDoSaldo: [''],
      clienteId: [null, [Validators.required]],
      corretoraId: [null, [Validators.required]]
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public onClicouEm(evento) {
    if (evento.botaoClicado === 'cancelar') {
      this.resetForm();
    } else {
      this.salvarAlteracao();
    }
  }

  public carteiraPossuiLancamento(): void {
    this.lancamentoService.getPossuiLancamentoByCarteiraId(this.carteiraId, true).subscribe({
      next: (possuiLancamento: boolean) => {
        if (possuiLancamento) {
          this.form.controls['clienteId'].disable();
          this.form.controls['corretoraId'].disable();
          this.form.controls['saldo'].disable();
          this.form.controls['dataDeAtualizadoDoSaldo'].disable();
        }
        else {
          this.form.controls['clienteId'].enable();
          this.form.controls['corretoraId'].enable();
          this.form.controls['saldo'].enable();
          this.form.controls['dataDeAtualizadoDoSaldo'].enable();

        }
      },
      error: (error: any) => {
        this.toastr.error('Erro ao verificar lancamentos da carteira.', 'Erro!');
        console.error(error)
      }
    });
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {

      this.carteira = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.carteira.id, ...this.form.value};

      this.carteiraService[this.estadoSalvar](this.carteira).subscribe(
        (_carteira: Carteira) => {
          //this.spinner.hide();
          this.toastr.success('Carteira salva com sucesso!', 'Sucesso');
          this.router.navigate([`clientes/listarcarteiras/${_carteira.clienteId}`]);
          //this.router.navigate([`clientes/carteiradetalhe/${_carteira.clienteId}/${_carteira.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar carteira', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
