export class LoadContacts {
  static readonly type = '[Contact] Load Contacts';

  constructor(
    public page: number,
    public pageSize: number
  ) {}
}