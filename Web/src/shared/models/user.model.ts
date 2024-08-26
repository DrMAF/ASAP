export class User {
  id: number = 0;
  firstName: string = "";
  lastName: string = "";
  email: string = "";
  phoneNumber: string = "";
}

export class PaginatedResult<T> {
  items: T[] = [];
  pageIndex: number = 1;
  totalPages: number = 1;
  hasPreviousPage: boolean = false;
  hasNextPage: boolean = true;
}

