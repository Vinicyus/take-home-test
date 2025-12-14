import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoanListComponent } from './components/loan-list/loan-list.component'; 

@Component({
  selector: 'app-root',
  standalone: true, // <-- Add this for clarity and consistency
  imports: [RouterOutlet, LoanListComponent], 
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'loan-app';
}