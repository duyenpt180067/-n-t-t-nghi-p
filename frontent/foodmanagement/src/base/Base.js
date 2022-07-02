import { CRUD } from '../base/Resources';
class Base {
    constructor() {}

    /**
     * Ghép chuỗi vào vị trí muốn để
     * Create: PTDuyen(23/08/2021)
     * @param {chuỗi ban đầu} str 
     * @param {list chuỗi truyền vào} listArg 
     * @returns 
     */
    stringFormat(str, listArg) {
        if (listArg.length) {
            // type của chuỗi truyền vào đầu tiên của mảng
            var t = typeof(listArg[0]);
            var key;
            var args = ("string" === t || "number" === t) ?
                Array.prototype.slice.call(listArg) :
                listArg[0];

            for (key in args) {
                str = str.replace(new RegExp("\\{" + key + "\\}", "gi"), args[key]);
            }
        }
        return str;
    }

    /**
     * Check xem du lieu co bi thay đổi không
     * Create: PTDuyen(10/10/2021)
     */
    checkChangeData(obj, oldObj, crud, objectCode) {
        for (let key in obj) {
            // nếu key là objectCode
            if (key == objectCode) {
                // nếu là cập nhật thông tin thì so sánh
                if (crud == CRUD.Put) {
                    if (obj[key] != oldObj[key]) {
                        console.log(key);
                        return false;
                    }
                }
                // nếu không thì tiếp tục
                else {
                    continue;
                }
            } else if (key.includes('Number')) {
                continue;
            }
            // nếu key không phải ngày cập nhật thì so sánh
            else if (key != 'ModifiedDate') {
                // nếu thuộc tính của emp không phải dạng ngày
                if (key.toString().toLowerCase().includes('date') == false) {
                    if (obj[key] != oldObj[key]) {
                        console.log(key);
                        return false;
                    }
                }
                // nếu là dạng ngày
                else {
                    if (obj[key] == null && oldObj[key] != null) {
                        return false;
                    } else if (obj[key] == null && oldObj[key] == null) {
                        continue;
                    } else {
                        if (typeof(obj[key]) != 'string') {
                            if (obj[key] == 'Invalid Date') {
                                if (oldObj[key] == '') {
                                    continue;
                                } else return false;
                            } else {
                                if (obj[key].toLocaleDateString('en-GB') != oldObj[key]) {
                                    // console.log(obj[key], oldObj[key]);
                                    return false;
                                } else continue;
                            }
                        } else {
                            if (obj[key] != oldObj[key]) {
                                return false;
                            } else continue;
                        }
                    }
                }
            }
            // nếu là ngày cập nhật thì bỏ qua
            else {
                continue;
            }
        }
        // console.log('true');
        return true;
    }

    /**
     * Fomat date tu string sang date
     * Create: PTDuyen(17/08/2021)
     * @param {*} date 
     * @returns 
     */
    formatDate(date) {
        if ((date + '').split("/")) {
            let day = (date + '').split("/");
            let dateFormat = day[2] + '-' + day[1] + '-' + day[0];
            return dateFormat;
        } else return date;
    }

    getFile(imgSelector, event) {
        let input = event.target;
        if (input.files && input.files[0]) {
            let reader = new FileReader();
            //let imageData = '';
            reader.onload = (e) => {
                document.querySelector(imgSelector).setAttribute('src', e.target.result);
                //this.category.CategoryImage = e.target.result;
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    /**
     * Chuyển từ dạng số sang dạng có dấu . 
     * Created: PTDuyen(10/10/2021)
     * @param {*} number 
     * @returns 
     */
    formatNumber(number) {
        number = this.formatToNumber(number);
        let salaryFormatted;
        // = new Intl.NumberFormat('de-DE', { style: 'currency', currency: 'EUR' }).format(number);
        if ((number == '') || (number == 'NaN')) {
            // console.log(number);
            salaryFormatted = null;
        } else {
            salaryFormatted = parseInt(number + '').toFixed(0).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1.")
            if (isNaN(salaryFormatted.toString()[salaryFormatted.toString().length - 1])) {

                salaryFormatted.toString().slice(salaryFormatted.toString().length - 1, 1);
            }
        }

        return salaryFormatted;
    }

    /**
     * Fomat money tu string sang number(không có dấu .)
     * Create: PTDuyen(10/10/2021)
     * @param {*} param 
     * @returns 
     */
    formatToNumber(param) {
        if (param) {
            param = (param + "").split(",")[0];
            let salary = (param + "").split(".");
            let res = "";
            for (let i = 0; i < salary.length; i++) {
                res += salary[i];
            }
            return parseInt(res);
        } else return 0;
    }

    logout() {
        localStorage.removeItem('user');
    }

    uniqueArray(arr) {
        var a = arr.concat();
        for (var i = 0; i < a.length; ++i) {
            for (var j = i + 1; j < a.length; ++j) {
                if (a[i] === a[j])
                    a.splice(j--, 1);
            }
        }

        return a;
    }

    /**
     * Fomat date tu date sang string dd/mm/yyyy
     * Create: PTDuyen(17/08/2021)
     * @param {*} date 
     * @returns 
     */
    formatDateToString(date) {
        date = new Date(date);
        let month = '' + (date.getMonth() + 1);
        let day = '' + date.getDate();
        let year = date.getFullYear();
        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;
        let dateFormat = [day, month, year].join('/');
        return dateFormat.toString();
    }

    /**
     * Fomat date tu date sang string dd/mm/yyyy
     * Create: PTDuyen(17/08/2021)
     * @param {*} date 
     * @returns 
     */
    formatDateTimeToString(date) {
        date = new Date(date);
        let month = '' + (date.getMonth() + 1);
        let day = '' + date.getDate();
        let year = date.getFullYear();
        if (month.length < 2)
            month = '0' + month;
        if (day.length < 2)
            day = '0' + day;
        let dateFormat = [day, month, year].join('/');
        return dateFormat.toString() + " " + date.toLocaleTimeString();
    }

    justNumberInput(event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    }
}

export default new Base();