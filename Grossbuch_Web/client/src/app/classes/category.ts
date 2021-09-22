export class Category {
    id = -1;
    title = "";
    totalSum = 0;
    checked: boolean = false;

    constructor(_id: number, _title: string, _totalSum: number) {
        this.id = _id;
        this.title = _title;
        this.totalSum = _totalSum
        this.checked = false;
    }
}
