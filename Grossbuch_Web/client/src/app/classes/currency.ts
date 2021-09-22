export class Currency {
    id: number = -1;
    title: string = "";
    coefficient: number = 1;
    totalSum = 0.0;

    constructor(_id: number, _title: string, _coefficient: number) {
        this.id = _id;
        this.title = _title;
        this.coefficient = _coefficient;
    }
}
