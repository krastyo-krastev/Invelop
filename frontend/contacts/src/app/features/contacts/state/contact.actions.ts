import { SaveContactRequest } from "../dtos/contacts.dto";

export class LoadContacts {
  static readonly type = '[Contact] Load Contacts';

  constructor(
    public page: number,
    public pageSize: number
  ) {}
}

export class LoadContact {
  static readonly type = '[Contact] Load';
  constructor(public readonly id: string) {}
}

export class CreateContact {
  static readonly type = '[Contact] Create';
  constructor(public readonly contact: SaveContactRequest) {}
}

export class UpdateContact {
  static readonly type = '[Contact] Update';
  constructor(public readonly contact: SaveContactRequest) {}
}