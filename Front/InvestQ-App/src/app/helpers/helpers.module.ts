import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DateTimeFormatPipe } from './DateTimeFormat.pipe';
import { CnpjFormatPipe } from './CnpjFormat.pipe';
import { CpfFormatPipe } from './CpfFormat.pipe';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [
    DateTimeFormatPipe,
    CnpjFormatPipe,
    CpfFormatPipe,
  ],
  exports: [
    DateTimeFormatPipe,
    CnpjFormatPipe,
    CpfFormatPipe,
  ]
})
export class HelpersModule { }
