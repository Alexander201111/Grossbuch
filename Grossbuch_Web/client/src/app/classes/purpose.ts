export class Purpose {
    id = -1;
    title = "";
    balance = 0.0;
    totalSum = 0.0;
    isAccount: boolean = false;
    checked: boolean = false;

    constructor(_id: number, _title: string) {
        this.id = _id;
        this.title = _title;
    }
}