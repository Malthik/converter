import { Component } from '@angular/core';
import { catchError } from 'rxjs';
import { ConverterService } from './services/converter.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  csv: string | null = null;
  json: string | null = null;

  msgError: string | null = null;

  constructor(public converterService: ConverterService) {}

  converterJsonToCsv() {
    this.converterService
      .getCvsFromJson(this.json ?? '')
      .pipe<any>(
        catchError((err) => {
          if (typeof err.error === 'string' || err.error instanceof String) {
            this.msgError = err.error;

            throw new Error(err.error);
          } else {
            this.msgError =
              'Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.';
            throw new Error(
              'Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.'
            );
          }
        })
      )
      .subscribe((res) => {
        this.msgError = null;
        this.csv = res.csv;
      });
  }

  verifyJson($event: Event) {
    console.log($event);

    if (this.json === '') this.msgError = null;
  }

  clear() {
    this.json = null;
    this.csv = null;
    this.msgError = null;
  }

  downloadCsv() {
    const source = `data:text/csv;base64,${btoa(this.csv ?? '')}`;
    const link = document.createElement('a');
    link.href = source;
    link.download = 'json2csv.csv';
    link.click();
  }

  shouldShowActionButtons() {
    return this.json != null && this.json != '';
  }

  hasConvertedCsv() {
    return this.csv != null && this.csv != '';
  }

  hasMsgError() {
    return this.msgError != null && this.msgError != '';
  }
}
