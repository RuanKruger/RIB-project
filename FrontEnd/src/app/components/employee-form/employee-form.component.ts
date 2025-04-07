import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { EmployeeService } from '../../services/employee.service';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatCardModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule,
    FormsModule
  ],
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent implements OnInit {
  employeeForm: FormGroup;
  isEditMode = false;
  employeeId: number | null = null;
  
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService,
    private snackBar: MatSnackBar
  ) {
    this.employeeForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(128)]],
      lastName: ['', [Validators.required, Validators.maxLength(128)]],
      birthDate: ['', Validators.required],
      employeeNum: ['', [Validators.required, Validators.maxLength(16)]],
      employeeDate: ['', Validators.required],
      terminated: [null]
    });
  }
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.employeeId = +id;
        this.loadEmployee(+id);
      }
    });
  }
  
  loadEmployee(id: number): void {
    this.employeeService.getEmployee(id).subscribe({
      next: (employee) => {
        this.employeeForm.patchValue({
          firstName: employee.firstName,
          lastName: employee.lastName,
          birthDate: employee.birthDate,
          employeeNum: employee.employeeNum,
          employeeDate: employee.employeeDate,
          terminated: employee.terminated
        });
      },
      error: (error) => {
        console.error('Error loading employee', error);
        this.snackBar.open('Error loading employee details', 'Close', { duration: 3000 });
      }
    });
  }
  
  onSubmit(): void {
    if (this.employeeForm.invalid) {
      this.snackBar.open('Please correct the form errors', 'Close', { duration: 3000 });
      return;
    }
    
    const formValues = this.employeeForm.value;
    
    if (this.isEditMode && this.employeeId) {
      // Update existing employee
      const updateData = {
        employeeId: this.employeeId,
        ...formValues
      };
      
      this.employeeService.updateEmployee(this.employeeId, updateData).subscribe({
        next: () => {
          this.snackBar.open('Employee updated successfully', 'Close', { duration: 3000 });
          this.router.navigate(['/employees']);
        },
        error: (error) => {
          console.error('Error updating employee', error);
          this.snackBar.open('Error updating employee', 'Close', { duration: 3000 });
        }
      });
    } else {
      // Create new employee
      this.employeeService.createEmployee(formValues).subscribe({
        next: () => {
          this.snackBar.open('Employee created successfully', 'Close', { duration: 3000 });
          this.router.navigate(['/employees']);
        },
        error: (error) => {
          console.error('Error creating employee', error);
          this.snackBar.open('Error creating employee', 'Close', { duration: 3000 });
        }
      });
    }
  }
  
  goBack(): void {
    this.router.navigate(['/employees']);
  }
}