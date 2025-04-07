export interface EmployeeUpdate {
    employeeId: number;
    lastName: string;
    firstName: string;
    birthDate: Date;
    employeeNum: string;
    employeeDate: Date;
    terminated?: Date;
  }