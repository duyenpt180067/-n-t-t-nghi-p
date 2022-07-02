// import { CRUD } from '../resources'

const action = {
    Post: 'post',
    Put: 'put',
    Delete: 'delete',
    Copy: 'copy',
    Read: 'read',
    StopUse: 'stop',
    Note: 'note',
    DeleteNode: 'deleteNote',
    ChangeAccount: 'changeAccount',
    Merge: 'merge',
    Close: 'close',
    Print: 'print'
}

const FieldNameViGroup = {
    group_code: "Mã nhóm NCC, KH",
    group_name: "Tên nhóm nhà cung cấp",
}

const FieldNameViEmployee = {
    object_code: "Mã nhân viên",
    object_name: "Tên nhân viên",
    tax_code: "Mã số thuế",
    address: "Địa chỉ",
    date_of_birth: "Ngày sinh",
    gender: "Giới tính",
    department_id: "Đơn vị",
    identity_number: "Số CMND",
    identity_date: "Ngày cấp",
    identity_place: "Nơi cấp",
    email: "Email",
    phone_number: "Số điện thoại",
    telephone_number: "Điện thoại cố định",
    employee_position: "Chức danh",
}

const FieldNameViProvider = {
    object_code: "Mã nhà cung cấp",
    object_name: "Tên nhà cung cấp",
    tax_code: "Mã số thuế",
    address: "Địa chỉ",
    identity_number: "Số CMTND/CCCD",
    identity_date: "Ngày cấp CMTND/CCCD",
    identity_place: "Nơi cấp CMTND/CCCD",
    phone_number: "Số điện thoại",
    created_date: "Ngày tạo",
    modifie_date: "Ngày cập nhật",
    created_by: "Người tạo",
    modified_by: "Người cập nhật",
}

const FieldNameViBallot = {
    account_date: "Ngày hoạch toán",
    vote_date: "Ngày chứng từ",
    ballot_code: "Số chúng từ",
    reason: "Diễn tả",
    sum_money: "Số tiền",
    object_code: "Mã đối tượng",
    object_name: "Tên đối tượng",
    ballot_address: "Địa chỉ"
}

const FieldNameViAccount = {
    account_number: "Số tài khoản",
    account_name: "Tên tài khoản",
}

const Pages = [
    { value: 10, text: '10 bản ghi trên 1 trang' },
    { value: 20, text: '20 bản ghi trên 1 trang' },
    { value: 30, text: '30 bản ghi trên 1 trang' },
    { value: 50, text: '50 bản ghi trên 1 trang' },
    { value: 100, text: '100 bản ghi trên 1 trang' }
]

const Actions = [
    { value: action.Copy, text: 'Nhân bản' },
    { text: 'Xóa', value: action.Delete },
]

const ReceiptPaymentActions = [
    { text: 'Ghi sổ', value: action.Note },
    { text: 'Xóa', value: action.Delete },
    { text: 'Nhân bản', value: action.Copy },
]

const OrderActions = [
    { text: 'Chi tiết', value: action.Read },
    { text: 'Hủy', value: action.Delete },
]

const ViewAction = [
    { text: 'Xem', value: action.Read },
]

const AccountActions = [
    { text: 'Nhân bản', value: action.Copy },
    { text: 'Xóa', value: action.Delete },
    { text: 'Chuyển khoản hạch toán', value: action.ChangeAccount },
    { text: 'Ngừng sử dựng', value: action.StopUse }
]

const ActionAll = [
    { text: 'Xóa', value: action.Delete },
    { text: 'Gộp', value: action.Merge },
]

const Operation = [
    { text: 'Lớn hơn', textEnglish: 'More', value: 0 },
    { text: 'Nhỏ hơn', textEnglish: 'Less', value: 1 },
    { text: 'Trong khoảng', textEnglish: 'Range', value: 2 },
    { text: 'Bằng', textEnglish: 'Equal', value: 3 },
]

const ActionMore = [
    { text: 'Cất và thêm', value: action.Post },
    { text: 'Cất và đóng', value: action.Close },
    { text: 'Cất và in', value: action.Print }
]

const ToastMgs = {
    GetSuccess: 'Tải dữ liệu thành công',
    PostSuccess: 'Thêm mới dữ liệu thành công',
    PostError: 'Thêm mới {0} thất bại! Vui lòng thử lại hoặc liên hệ MISA để được trợ giúp.',
    PutSuccess: 'Cập nhật dữ liệu thành công',
    PutError: 'Cập nhật {0} thất bại! Vui lòng thử lại hoặc liên hệ MISA để được trợ giúp.',
    GetError: 'Tải dữ liệu thất bại! Vui lòng thử lại hoặc liên hệ MISA để được trợ giúp.',
    DeleteSuccess: 'Xóa dữ liệu {0} thành công',
    DeleteError: 'Xóa dữ liệu thất bại {0} thất bại! Vui lòng thử lại hoặc liên hệ MISA để được trợ giúp.',
    ExceptionError: 'Có lỗi xảy ra! Vui lòng thử lại hoặc liên hệ MISA để được trợ giúp.'
}

const InvalidMgs = {
    DataNullError: '{0} không được để trống!',
    DateInvalid: '{0} phải nhỏ hơn hiện tại!',
    CodeInvalid: '{0} phải bắt đầu bằng NV-'
}

const TypeProvider = [
    { text: 'Tất cả', value: '' },
    { text: 'Tổ chức', value: true },
    { text: 'Cá nhân', value: false }
]

const StateOwed = [
    { text: 'Tất cả', value: '' },
    { text: 'Còn nợ', value: true },
    { text: 'Hết nợ', value: false }
]

const StateFollow = [
    { text: 'Tất cả', value: '' },
    { text: 'Theo dõi', value: true },
    { text: 'Ngừng theo dõi', value: false }
]

const ReasonTypeBallot = {
    payForProviders: 'payForProviders',
    advanceForEmployee: 'advanceForEmployee',
    outOfBill: 'outOfBill',
    saveOnBank: 'saveOnBank',
    otherPay: 'otherPay',
    receiptForProviders: 'receiptForProviders',
    receiptForEmployee: 'receiptForEmployee',
    receiptOnBank: 'receiptOnBank',
    otherReceipt: 'otherReceipt'
}

const ListPayType = [
    { value: ReasonTypeBallot.payForProviders, text: 'Trả tiền nhà cung cấp (không theo hóa đơn)' },
    { value: ReasonTypeBallot.advanceForEmployee, text: 'Tạm ứng cho nhân viên' },
    { value: ReasonTypeBallot.outOfBill, text: 'Chi mua ngoài hóa đơn' },
    { value: ReasonTypeBallot.saveOnBank, text: 'Gửi tiền vào ngân hàng' },
    { value: ReasonTypeBallot.otherPay, text: 'Chi khác' },
]

const ListRecType = [
    { value: ReasonTypeBallot.receiptForProviders, text: 'Thu tiền khách hàng (không theo hóa đơn)' },
    { value: ReasonTypeBallot.receiptForEmployee, text: 'Thu hoàn ứng nhân viên' },
    { value: ReasonTypeBallot.receiptOnBank, text: 'Rút tiền gửi về nhập quỹ' },
    { value: ReasonTypeBallot.otherReceipt, text: 'Thu khác' },
]

const ListTypePerson = ['Nhà cung cấp', 'Khách hàng', 'Nhân viên']
const ListRequired = ['Chỉ cảnh báo', 'Bắt buộc nhập']
const AccountType = ['Dư nợ', 'Dư có', 'Lưỡng tính', 'Không có số dư']

const ListReasonBallot = [{ value: '', text: 'Tất cả' }].concat(ListPayType.concat(ListRecType))

const OrderStatus = {
    All: { text: 'Tất cả', textEnglish: 'All', value: -1 },
    Processing: { text: 'Chờ xử lý', textEnglish: 'Processing', value: 0 },
    Reject: { text: 'Từ chối', textEnglish: 'Reject', value: 1 },
    Accept: { text: 'Chấp nhận', textEnglish: 'Accept', value: 2 },
    WaitingToTake: { text: 'Tài xế đang đến quán', textEnglish: 'WaitingToTake', value: 3 },
    Delivering: { text: 'Tài xế đang giao hàng', textEnglish: 'Delivering', value: 4 },
    Complete: { text: 'Hoàn thành', textEnglish: 'Complete', value: 5 },
    Delete: { text: 'Đã hủy', textEnglish: 'Delete', value: 6 },
}

const ListOrderStatus = [
    { text: 'Tất cả', textEnglish: 'All', value: -1 },
    { text: 'Chờ xử lý', textEnglish: 'Processing', value: 0 },
    { text: 'Từ chối', textEnglish: 'Reject', value: 1 },
    { text: 'Quán đang chuẩn bị đơn', textEnglish: 'Accept', value: 2 },
    { text: 'Tài xế đang đến quán', textEnglish: 'WaitingToTake', value: 3 },
    { text: 'Tài xế đang giao hàng', textEnglish: 'Delivering', value: 4 },
    { text: 'Hoàn thành', textEnglish: 'Complete', value: 5 },
    { text: 'Hủy', textEnglish: 'Delete', value: 6 },
]

const DiscountType = [
    { text: 'Tất cả', value: null },
    { text: 'Giảm giá trên món', value: 0 },
    { text: 'Mã khuyến mãi cho khách hàng', value: 1 }
]

const ListTimeToFilter = [{
        text: 'Đầu tháng đến hiện tại',
        value: {
            startDate: new Date(new Date().getFullYear(), new Date().getMonth(), 1),
            endDate: new Date()
        }
    },
    {
        text: 'Đầu năm đến hiện tại',
        value: {
            startDate: new Date(new Date().getFullYear(), 0, 1),
            endDate: new Date()
        }
    },
    {
        text: 'Qúy này',
        value: {
            startDate: new Date(new Date().getFullYear(), (Math.floor(new Date().getMonth()) / 3) * 3, 1),
            endDate: new Date()
        }
    },
    {
        text: 'Tuần này',
        value: {
            startDate: new Date(new Date().setDate(new Date().getDate() - (new Date().getDay() - 1))),
            endDate: new Date()
        }
    },
    {
        text: 'Tháng 1',
        value: {
            startDate: new Date(new Date().getFullYear(), 0, 1),
            endDate: new Date(new Date().getFullYear(), 0, 31)
        }
    },
]

// danh sách xưng hô
const Vocatives = ['Anh', 'Chị', 'Ông', 'Bà']


const ListUnitMoney = [
    { code: 'VND', text: 'Việt Nam đồng', value: 1 },
    { code: 'USD', text: 'Đô la Mỹ', value: 20960 },
    { code: 'EUR', text: 'Đông tiền chung châu Âu', value: 27272 }
]

module.exports = {
    FieldNameViEmployee,
    Pages,
    Actions,
    ActionAll,
    ViewAction,
    ToastMgs,
    InvalidMgs,
    ReceiptPaymentActions,
    AccountActions,
    Vocatives,
    FieldNameViGroup,
    FieldNameViProvider,
    FieldNameViBallot,
    ActionMore,
    TypeProvider,
    StateOwed,
    StateFollow,
    ReasonTypeBallot,
    ListReasonBallot,
    ListPayType,
    ListRecType,
    OrderStatus,
    ListOrderStatus,
    ListTimeToFilter,
    ListUnitMoney,
    ListTypePerson,
    ListRequired,
    AccountType,
    FieldNameViAccount,
    Operation,
    OrderActions,
    DiscountType
}