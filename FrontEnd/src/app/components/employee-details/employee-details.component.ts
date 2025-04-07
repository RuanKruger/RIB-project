import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Employee } from '../../models/employee';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-details',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatSnackBarModule
  ],
  templateUrl: './employee-details.component.html',
  styleUrls: ['./employee-details.component.css']
})
export class EmployeeDetailsComponent implements OnInit {
  employee: Employee | null = null;
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService,
    private snackBar: MatSnackBar
  ) { }
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.loadEmployee(+id);
      }
    });
  }
  
  loadEmployee(id: number): void {
    this.employeeService.getEmployee(id).subscribe({
      next: (employee) => {
        this.employee = employee;
      },
      error: (error) => {
        console.error('Error loading employee', error);
        this.snackBar.open('Error loading employee details', 'Close', { duration: 3000 });
      }
    });
  }
  
  editEmployee(): void {
    if (this.employee) {
      this.router.navigate(['/employees', this.employee.employeeId, 'edit']);
    }
  }
  
  deleteEmployee(): void {
    if (this.employee && confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(this.employee.employeeId).subscribe({
        next: () => {
          this.snackBar.open('Employee deleted successfully', 'Close', { duration: 3000 });
          this.router.navigate(['/employees']);
        },
        error: (error) => {
          console.error('Error deleting employee', error);
          this.snackBar.open('Error deleting employee', 'Close', { duration: 3000 });
        }
      });
    }
  }
  
  goBack(): void {
    this.router.navigate(['/employees']);
  }
}