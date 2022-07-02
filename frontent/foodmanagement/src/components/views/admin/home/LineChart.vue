<script>
    import { Line } from 'vue-chartjs/legacy'

    export default {
        extends: Line,
        props: ['data', 'options'],
        mounted () {
            this.renderChart(this.data, this.options)
        }
    }
</script>