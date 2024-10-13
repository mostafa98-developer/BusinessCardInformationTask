import { ChangeDetectorRef, Component } from '@angular/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterOutlet } from '@angular/router';
import { PagesModule } from './pages/pages.module';
import { SharedModule } from './shared/shared.module';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoaderService } from './shared/loader/loader.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,MatSnackBarModule,PagesModule,SharedModule ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Business-Card-Information-Web-App';

  constructor(public loadrServices: LoaderService,
    private spinner: NgxSpinnerService,private cdRef:ChangeDetectorRef) {

  }
  ngOnInit(): void {
    this.loadrServices.isLoading.subscribe(res => {
      if (res) {
        this.spinner.show();
        this.cdRef.detectChanges();
      } else {
        this.spinner.hide();
      }
    });
  }
}
