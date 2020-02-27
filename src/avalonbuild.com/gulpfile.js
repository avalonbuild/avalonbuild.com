/// <binding Clean='clean' />

const { src, dest, watch, series, parallel } = require('gulp');

const sourcemaps = require('gulp-sourcemaps');
const sass = require('gulp-sass');
const postcss = require('gulp-postcss');
const autoprefixer = require('autoprefixer');
const cssnano = require('cssnano');
const del = require('del');

var webroot = "./wwwroot/";

var paths = {
    css: webroot + "css/",
    sass: webroot + "sass/"
};

var files = {
    scss: paths.sass + '**/*.scss'
}

var options = {
    sass: {
        parameters: {
            outputStyle: "expanded",
			includePaths: ["wwwroot/lib/", "wwwroot/sass/"]
        },
        error: function (err) {
            console.error('Error!', err.message);
        }
    }
};

function cleanTask () {

    return del(
        [
            paths.css
        ]
    );

}

function scssTask () {    

    return src(files.scss)
        .pipe(sourcemaps.init())
        .pipe(sass(options.sass.parameters).on('error', options.sass.error))
        .pipe(postcss([ autoprefixer(), cssnano() ]))
        .pipe(sourcemaps.write('.'))
        .pipe(dest(paths.css)
    );

}

function watchTask () {

    watch(
        [files.scss],
        {interval: 1000, usePolling: true}, //Makes docker work
        series(
            scssTask
        )
    );    

}

exports.clean = cleanTask;

exports.build = series(
    cleanTask,
    scssTask
);

exports.default = series(
    cleanTask,
    scssTask,
    watchTask
);

