<template>
<div class="introduction">
    <v-sheet class="mx-auto" elevation="8" max-width="800">
        <v-slide-group active-class="success" show-arrows>
            <v-slide-item cycle v-for="item in sliderList" :key="item.SilderCode">
                <v-card class="find">
                    <div class="day-offer flex w-full find-element">
                        <div class="w-1/2 content-day-offer content-find">
                            <h3>{{item.SliderName}}</h3>
                            <p style="font-size: 13px">{{item.SliderTitle}}</p>
                            <p style="white-space: pre-line;">{{item.SliderContent}}</p>
                            <a :href="item.SliderLink" class="find-conclude">Order Now</a>
                        </div>
                        <div class="w-1/2 img-burger">
                            <i class="fas fa-circle" style="color: rgb(255,194,34);"></i>
                            <img :src="item.SliderImage" alt="">
                        </div>
                    </div>
                </v-card>
            </v-slide-item>
        </v-slide-group>
    </v-sheet>
</div>
</template>
<style>
.introduction {
    margin: 50px 0;
    background-color: #f7f2e2;
    padding: 80px 0;
    text-align: center;
}
.introduction>.theme--light.v-sheet{
    background-color: inherit !important;
    box-shadow: none !important;
    border-style: none !important;
}
.v-item-group .v-icon.mdi:hover {
    background-color: #e5e5e5;
    border-radius: 50%;
}

.introduction .find{
    display: inline-flex;
    min-width: 460px;
    min-height: 260px;
    margin: 10px 111.5px;
}

.introduction .find .find-element{
    padding: 30px;
}

.introduction .find .find-element .content-find{
    text-align: start;
}

.introduction .find .img-burger{
    position: relative;
}

.introduction .find .find-element .content-find h3{
    font-size: 28px;
    line-height: 30px;
    margin-bottom: 10px;
    color: #1e1d23;
    font-family: 'GoogleSans-Bold';
}

.introduction .find .find-element .content-find .find-conclude{
    color: var(--primary-color);
    font-family: 'GoogleSans-Bold';
    font-size: 17px;
    margin-top: 25px;
}

.introduction .find .img-burger img{
    position: absolute;
    top: 10px;
    right: 20px;
    width: 184px;
    height: 138px;
}

.introduction .find i{
    font-size: 165px;
    position: absolute;
    right: 10px;
    transition: 500ms;
}

.find:hover i {
    transform: scale(1.1);
}
</style>
<script>
import SliderAPI from '../../../../api/component/bussiness-item/SliderAPI';
export default {
    name:'Introduce',
    async created() {
        try {
            this.sliderList = (await SliderAPI.getFilterPaging({SliderFilter:null,PageSize:null,PageNumber:null})).data;
        } catch (error) {
            console.log(error);
        }
    },
    data () {
        return {
            sliderList:[]
        }
    },
}
</script>