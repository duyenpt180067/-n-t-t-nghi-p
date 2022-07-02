<template>
<!-- popup -->
<div class="general-popup">
    <div class="cover-background"></div>
    <div class="pop">
        <div class="popup">
            <div class="content-pop">
                <div class="icon-pop" id="icon-pop"></div>
                <div class="notification-pop">
                    {{popup.message}}
                </div>
            </div>
            
            <div class="btn-pop" :class="{move:move}">
                <div id="btn-cancel" class="btn-pop-init" tabindex="0" 
                    @mouseover="hover = true" 
                    @mouseleave="hover = false" 
                    @focus="hover=true"
                    :class="{'cancel-btn':move , 'hover':hover&&move}" 
                    @click="closePop()"
                    @keyup.enter="closePop()"
                    >
                    <!-- Hủy -->
                    {{popup.btn_cancel}}
                </div>
                <div class="btn-pop-left" id="btn-pop-right">
                    <button class="btn-pop-init btn1-pop" tabindex="0" id="btn1" 
                            @click="closeForm(), closePop()"
                            @keyup.enter="closeForm(), closePop()">
                        <!-- Không -->
                        {{popup.btn1}}
                    </button>
                    <button class="btn-pop-init btn2-pop" tabindex="0" id="btn2" 
                            @click="closePop(), crudObject()" 
                            @keyup.enter="closePop(), crudObject()"
                            v-bind:class="color">
                        <!-- Có -->
                        {{popup.btn2}}
                    </button>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<style scoped>
.end{
    justify-content: flex-end;
}
.cancel-btn {
    color: white;
    background-color: var(--primary-admin-color);
}

.btn-pop-init:hover,
.btn-pop-init:focus{
    background-color: #d2d3d6;
    outline: none;
}

.hover {
    color: white;
    background-color: var(--gray-color) !important;
}
</style>

<script>
import { PopupState } from '../../base/Resources.js'
export default {
    name: 'VPopup',
    data: function () {
        return {
            popup: {
                message: "",
                btn_cancel:"",
                btn1: "",
                btn2: ""
            },
            color: "",
            // btn-cancel chuyển về giữa hoặc cuối
            move: false,
            // hover btn-cancel
            hover:false
        }
    },
    props: {
        formShow: Boolean,
        popShow: Boolean,
        action: String,
        crud: String,
        empDelete: Object,
        message: String,
        displayName: String,
        idInvalid:String
    },
    mounted(){
        this.showInfoPopup(this.action);
    },
    created() {
        //this.showInfoPopup(this.action);
    },
    methods: {
        /**
         * Show popup
         * Author: PTDuyen
         * Create 17/8/2021
         */
        showInfoPopup(action) {
            // hiển thị pop khi muốn hủy form
            if (action == PopupState.Cancel) {
                this.popup.message = "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?";
                this.popup.btn_cancel = "Hủy"
                this.popup.btn1 = "Không";
                this.popup.btn2 = "Có";
                this.color = "primary-pop";
                document.getElementById("icon-pop").style.backgroundPosition = "-826px -456px";
            } 
            // hiển thị pop khi muốn thực hiện hành động xóa
            else if (action == PopupState.Delete) {
                this.popup.message = "Bạn có chắc muốn Xóa " + this.displayName.toLowerCase() + " <"+ this.message +"> không?";
                this.popup.btn_cancel = "Không";
                this.popup.btn2 = "Có";
                this.color = 'primary-pop';
                document.getElementById("btn1").style.display = "none";
                document.getElementById("icon-pop").style.backgroundPosition = "-593px -456px";
            } 
            // hiển thị pop khi dữ liệu không hợp lệ khi request đã được thực hiện
            else if(action == PopupState.Duplicate){
                this.move = true;
                this.popup.message = this.message;
                this.popup.btn_cancel = "Đồng ý";
                this.color = 'primary-pop';
                document.getElementById("btn-pop-right").style.display = "none";
                document.getElementById("icon-pop").style.backgroundPosition = "-593px -456px";
                document.getElementById("btn-cancel").style.marginLeft = "calc(100% - 78px)";
                // document.getElementById("btn-cancel").addEventListener("click", function(){
                //     location.reload();
                // })
            } 
            // hiển thị pop khi dữ liệu không hợp lệ khi validate ngay trên form
            else if(action == PopupState.Invalid){
                // this.popup.message = document.getElementById(this.idInvalid).nextSibling.firstChild.textContent;
                this.popup.message = document.querySelector("#"+this.idInvalid+" ~ div > p").textContent;
                console.log(this.$el);
                this.popup.btn_cancel = "Đóng";
                this.color = 'primary-pop';
                this.move = true;
                document.getElementById("btn-pop-right").style.display = "none";
                document.getElementById("btn-cancel").style.margin = "auto";
                document.getElementById("icon-pop").style.backgroundPosition = "-747px -456px";
            }
        },

        /**
         * Đóng form
         * Create: PTDuyen(17/08/2021)
         */
        closeForm() {
            this.$emit('closeForm');
        },

        /**
         * Đóng popup
         * Create: PTDuyen(17/08/2021)
         */
        closePop() {
            this.$emit('closePop', this.action);
        },

        /**
         * Gửi yêu cầu thêm/sửa/xóa employee/customer
         * Create: PTDuyen(17/08/2021)
         */
        async crudObject() {
            await this.$emit('crudObject');
        },
    }
}
</script>
