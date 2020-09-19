// Note this only includes basic configuration for development mode.
// For a more comprehensive configuration check:
// https://github.com/fable-compiler/webpack-config-template

var path = require("path");
var webpack = require("webpack")
var CopyWebpackPlugin = require('copy-webpack-plugin');
var HtmlWebpackPlugin = require('html-webpack-plugin');
var isProduction = !process.argv.find(v => v.indexOf('webpack-dev-server') !== -1);

if (isProduction)
    console.log("Building for PRODUCTION");
else
    console.log("Building for DEBUG");

function resolve(filePath) {
    return path.isAbsolute(filePath) ? filePath : path.join(__dirname, filePath);
}

var CONFIG = {
    outputDir: "./deploy",
    debugDir: "./debug",
    assetsDir: "./public",
    indexTemplate: "./src/index.html"
}

var commonPlugins = [
    new HtmlWebpackPlugin({
        filename: 'index.html',
        template: resolve(CONFIG.indexTemplate)
    })
];

module.exports = {
    mode: "development",
    devtool: 'eval-source-map',
    entry: "./src/App.fsproj",
    output: {
        path: path.join(__dirname, isProduction ? CONFIG.outputDir : CONFIG.debugDir),
        filename: isProduction ? 'bundle.[hash].js' : 'bundle.js'
    },
    plugins:
        commonPlugins.concat(
            isProduction ?
                [
                    new CopyWebpackPlugin({ patterns: [ {from: resolve(CONFIG.assetsDir) }]})
                ] :
                [
                    new CopyWebpackPlugin({ patterns: [ {from: resolve(CONFIG.assetsDir) }]}),
                    new webpack.HotModuleReplacementPlugin()
                ]
        ),
    devServer: {
        publicPath: "/",
        contentBase: resolve(CONFIG.debugDir),
        port: 8080,
    },
    module: {
        rules: [{
            test: /\.fs(x|proj)?$/,
            use: "fable-loader"
        },
        {
            test: /\.styl$/,
            // compiles Styl to CSS
            use: [
                'style-loader',
                'css-loader',
                'stylus-loader'
            ],
        },
        {
            test: /\.css$/,
            use: [
                'style-loader',
                'css-loader'
            ],
        }]
    }
}