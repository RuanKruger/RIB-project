export interface Employee {
    employeeId: number;
    personId: number;
    lastName: string;
    firstName: string;
    birthDate: Date;
    employeeNum: string;
    employeeDate: Date;
    terminated?: Date;
  }