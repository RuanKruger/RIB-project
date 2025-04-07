import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Employee } from '../../models/employee';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatSnackBarModule
  ],
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {
  dataSource = new MatTableDataSource<Employee>();
  displayedColumns: string[] = ['employeeId', 'employeeNum', 'firstName', 'lastName', 'birthDate', 'employeeDate', 'terminated', 'actions'];

  constructor(
    private employeeService: EmployeeService,
    private router: Router,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: (employees) => {
        this.dataSource.data = employees;
      },
      error: (error) => {
        console.error('Error loading employees', error);
        this.snackBar.open('Error loading employees', 'Close', { duration: 3000 });
      }
    });
  }

  viewEmployee(id: number): void {
    this.router.navigate(['/employees', id]);
  }

  editEmployee(id: number): void {
    this.router.navigate(['/employees', id, 'edit']);
  }

  deleteEmployee(id: number): void {
    if (confirm('Are you sure you want to delete this employee?')) {
      this.employeeService.deleteEmployee(id).subscribe({
        next: () => {
          this.loadEmployees();
          this.snackBar.open('Employee deleted successfully', 'Close', { duration: 3000 });
        },
        error: (error) => {
          console.error('Error deleting employee', error);
          this.snackBar.open('Error deleting employee', 'Close', { duration: 3000 });
        }
      });
    }
  }

  addNewEmployee(): void {
    this.router.navigate(['/employees/new']);
  }
}