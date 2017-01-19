/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    sass = require('gulp-sass'),
    sourcemaps = require('gulp-sourcemaps'),
    prefix = require('gulp-autoprefixer'),
    uglify = require("gulp-uglify");

var webroot = "./wwwroot/";

var paths = {
    css: webroot + "css/",
    sass: webroot + "sass/",
};

var options = {
    sass: {
        parameters: {
            outputStyle: "expanded",
			includePaths: ["wwwroot/lib/", "wwwroot/sass/"]
        },
        error: function (err) {
            console.error('Error!', err.message);
        }
    },
};

gulp.task("clean:css", function (cb) {
    rimraf(paths.css, cb);
});

gulp.task("clean", ["clean:css"]);

gulp.task('build:sass', ['clean:css'], function () {
	return gulp.src(paths.sass + "**/*.scss")
		.pipe(sourcemaps.init())
		.pipe(sass(options.sass.parameters).on('error', options.sass.error))
		//.pipe(prefix())
        .pipe(cssmin())
		.pipe(sourcemaps.write())
		.pipe(gulp.dest(paths.css));
});

gulp.task('build', ['build:sass'], function () {});

gulp.task('watch', function () {
    gulp.watch(paths.sass + "**/*.scss", ['build:sass']);
});


