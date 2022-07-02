import Login from '@/components/views/login/Login'

import PageUser from '@/components/views/user/PageUser'
import Home from '@/components/views/user/home/Home'
import Faq from '@/components/views/user/faq/Faq'
import Shop from '@/components/views/user/shop/Shop'
import Food from '@/components/views/user/shop/Food'
import About from '@/components/views/user/about/About'
import Cart from '@/components/views/user/cart/Cart'
import Account from '@/components/views/user/account/Account'
import BlogList from '@/components/views/user/blog/BlogList'
import BlogDetail from '@/components/views/user/blog/BlogDetail'

import PageAdmin from '@/components/views/admin/PageAdmin'
import HomeAdmin from '@/components/views/admin/home/HomeAdmin'
import CategoryAdmin from '@/components/views/admin/food/CategoryAdmin'
import FoodAdmin from '@/components/views/admin/food/FoodAdmin'
import FoodDetail from '@/components/views/admin/food/FoodDetail'
import OrderAdmin from '@/components/views/admin/order/OrderAdmin'
import OrderUserDetail from '@/components/views/user/account/OrderUserDetail'
import BlogAdmin from '@/components/views/admin/manage/blog/BlogAdmin'
import AccessLog from '@/components/views/admin/manage/AccessLog'
import SliderAdmin from '@/components/views/admin/manage/slider/SliderAdmin'
import FaqAdmin from '@/components/views/admin/manage/faq/FaqAdmin'
import DiscountAdmin from '@/components/views/admin/manage/discount/DiscountAdmin'
import DiscountConditionAdmin from '@/components/views/admin/manage/discount/DiscountConditionAdmin'


export const routes = [
    { path: '/', redirect: '/user/home', name: 'Home', component: Home },
    { path: '/login', name: 'Login', component: Login },
    {
        path: '/user',
        redirect: 'user/home',
        name: 'PageUser',
        component: PageUser,
        children: [
            { path: 'home', name: 'Home', component: Home },
            { path: 'faq', name: 'Faq', component: Faq },
            { path: 'about', name: 'About', component: About },
            { path: 'cart', name: 'Cart', component: Cart },
            { path: 'account', name: 'Account', component: Account },
            { path: 'shop/:categoryId?', name: 'Shop', component: Shop, props: true },
            { path: 'menu/:foodCode', name: 'Food', component: Food, props: true },
            { path: 'blog', name: 'BlogList', component: BlogList },
            { path: 'blog/:blogCode', name: 'BlogDetail', component: BlogDetail, props: true },
            { path: 'menu', redirect: 'shop' },
        ]
    },
    {
        path: '/app',
        redirect: 'app/home',
        name: 'PageAdmin',
        component: PageAdmin,
        children: [
            { path: 'home', name: 'HomeAdmin', component: HomeAdmin },
            { path: 'category', name: 'CategoryAdmin', component: CategoryAdmin },
            { path: 'food', name: 'FoodAdmin', component: FoodAdmin },
            { path: 'food-detail/:code?', name: 'FoodDetail', component: FoodDetail, props: true },
            { path: 'food-detail/copy/:copyCode', name: 'CopyFoodDetail', component: FoodDetail, props: true },
            { path: 'order', name: 'OrderAdmin', component: OrderAdmin },
            { path: 'order-detail/:code?', name: 'OrderDetail', component: OrderUserDetail, props: true },
            { path: 'order-detail/copy/:copyCode', name: 'CopyOrderDetail', component: OrderUserDetail, props: true },
            { path: 'blog', name: 'BlogAdmin', component: BlogAdmin },
            { path: 'access-log', name: 'AccessLog', component: AccessLog },
            { path: 'slider', name: 'SliderAdmin', component: SliderAdmin },
            { path: 'faq', name: 'FaqAdmin', component: FaqAdmin },
            { path: 'discount', name: 'DiscountAdmin', component: DiscountAdmin },
            { path: 'discount-condition', name: 'DiscountConditionAdmin', component: DiscountConditionAdmin },
        ]
    },
]