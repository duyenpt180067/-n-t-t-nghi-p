<template>
<!-- toast-message -->

<div class="toastMessage">
    <div class="toastMgs">
        
    </div>
</div>
</template>

<style>

.toasted-container .toasted .action.icon{
    border: none;
    background-color: white;
}
.toast {
    height: 48px !important;
    font-size: 13px !important;
    border-radius: 4px !important;
    box-shadow: 0 3px 6px rgb(0 0 0 / 16%) !important;
    width: fit-content !important;
    z-index: 1;
    background-color: white !important;
    color: black !important;
}

.toasted-container .toasted{
    font-family: 'GoogleSans-REGULAR';
    font-size: 13px !important;
}

.success .material-icons{
    color: #2FBE8E!important;
}

.error .material-icons{
    color: #F65454!important;
}

.info .material-icons{
    color: #f7a000!important;
}
</style>

<script>
import {ToastMessageIcon} from '../../base/Resources'
export default {
    name: 'ToastMessage',
    props: {
        titleMgs: String,
        iconToast:String
    },
    created() {
        this.showToast();
        setTimeout(this.deleteToast,3000);
    },
    methods: {
        showToast() {
            if(this.iconToast == ToastMessageIcon.success){
                this.$toasted.success(this.titleMgs, {
                    icon: {
                        name: this.iconToast,
                    },
                    position: "top-center",
                    duration: 2000,
                    action: {
                        icon: 'close',
                        onClick: (e, toastObject) => {
                            toastObject.goAway(0);
                            this.$emit('deleteToast');
                        }
                    },
                    className: ['toast']
                });
            }
            else if(this.iconToast == ToastMessageIcon.error){
                this.$toasted.error(this.titleMgs, {
                    icon: {
                        name: this.iconToast,
                    },
                    position: "top-center",
                    duration: 2000,
                    action: {
                        icon: 'close',
                        onClick: (e, toastObject) => {
                            toastObject.goAway(0);
                            this.$emit('deleteToast');
                        }
                    },
                    className: ['toast']
                });
            }
            else if(this.iconToast == ToastMessageIcon.info || this.iconToast == ToastMessageIcon.error_outline){
                this.$toasted.info(this.titleMgs, {
                    icon: {
                        name: this.iconToast,
                    },
                    position: "top-center",
                    duration: 2000,
                    action: {
                        icon: 'close',
                        onClick: (e, toastObject) => {
                            toastObject.goAway(0);
                            this.$emit('deleteToast');
                        }
                    },
                    className: ['toast']
                });
            }
        },
        deleteToast(){
            this.$emit('deleteToast');
        }
    }
}
</script>
