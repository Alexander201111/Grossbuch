import { Account } from './account'
import { Category } from './category'
import { Currency } from './currency';
import { Purpose } from './purpose';
import { LoginModel } from './login-model';

export class Operation {
    id = -1;
    date: Date = new Date();
    account: Account = null;
    category: Category = null;
    currency: Currency = null;
    purpose: Purpose = null;
    summ = 0;
    description = "";

    showed: boolean = false;
    /* user: LoginModel = null; */

    constructor(_id: number, _account: Account, _categoty: Category,
        _currency: Currency, _purpose: Purpose, _description: string, _summ: number) {
        this.id = _id;
        this.account = _account;
        this.category = _categoty;
        this.currency = _currency;
        this.purpose = _purpose;
        this.summ = _summ;
        this.description = _description;
    }

}