<template>
<div>
    <GChart
        type="ColumnChart"
        :data="chartData"
        :options="chartOptions"
    />
    <!-- <line-chart :data="data" :options="options"></line-chart> -->
</div>
</template>
<script>
import { GChart } from 'vue-google-charts/legacy';
// import LineChart from './LineChart.vue'
export default {
    components: {
        GChart,
        // LineChart,
    },
    name: 'HomeAdmin',
    data () {
        return {
            chartData: [
                ['Year', 'Sales', 'Expenses', 'Profit'],
                ['2014', 1000, 400, 200],
                ['2015', 1170, 460, 250],
                ['2016', 660, 1120, 300],
                ['2017', 1030, 540, 350]
            ],
            chartOptions: {
                chart: {
                    title: 'Company Performance',
                    subtitle: 'Sales, Expenses, and Profit: 2014-2017',
                }
            },
        }
    },
}
</script>