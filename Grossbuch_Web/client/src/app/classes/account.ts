export class Account {
    id = -1;
    title = "";
    balance = 0.0;
    totalSum = 0.0;
    isAccount: boolean = true;
    checked: boolean = false;

    constructor(_id: number, _title: string, _balance: any) {
        this.id = _id;
        this.title = _title;
        this.balance = _balance;
    }
}
