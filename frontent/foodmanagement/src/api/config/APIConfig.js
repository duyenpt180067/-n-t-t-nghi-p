var APIConfig = {
    development: 'https://localhost:44345/api/v1/',
    production: 'https://localhost:44345/api/v1/',
    test: 'https://localhost:44345/api/v1/'
}

export default APIConfig[process.env.NODE_ENV];