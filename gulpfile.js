// Gulp Modules
var del = require('del');
var argv = require('yargs').argv;
var gulp = require('gulp');
var exec = require('sync-exec');
var nunit = require('gulp-nunit-runner');
var assemblyInfo = require('gulp-dotnet-assembly-info');

// Gulp Variables
var buildPath = '%CD%\\build';
var msBuildConfiguration = 'Release';
var version = '0.0.1'; //using package.json
var nunitConsole = 'packages\\NUnit.ConsoleRunner.3.6.1\\tools\\nunit3-console.exe';

// Gulp Default

gulp.task('default', ['test']);

// Gulp Tasks

gulp.task('compile', ['assemblyInfo', 'msbuild']);

gulp.task('assemblyInfo', function() {
    var package = require('./package.json');
    version = package.version;
    console.log('Version Number: ' + version);

    gulp.src('**/AssemblyInfo.cs')
        .pipe(assemblyInfo({
            title: 'Octobot',
            description: 'An automation tool for OctopusDeploy', 
            configuration: 'Release', 
            company: 'Dahood.io', 
            product: 'Octobot', 
            copyright: 'Copyright © Jonathan McCracken, Greg Cook, and Richard Hurst 2016', 
            trademark: 'Dahood.io', 
            version: '0.' + version,
            fileVersion: '0.' + version}))
        .pipe(gulp.dest('.'));
});

gulp.task('msbuild', function () {
  console.log('MSBuild Release Configuration: ' + msBuildConfiguration);
  var cmd = '"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Professional\\MSBuild\\15.0\\Bin\\msbuild.exe\" Octobot.sln /t:Rebuild ' +
    '/p:OutDir=' + buildPath + ';Configuration=' + msBuildConfiguration + ' /maxcpucount';
  console.log(exec(cmd).stdout);
});

gulp.task('test', ['compile'], function () {
    gulp.src(['build\\*.Tests.dll'], {read: false})
        .pipe(nunit({
        	noresult: true, //TODO: Fix this
            result: 'build\\Foo.xml',
            err: 'build\\NUnitErrors.txt',
            teamcity: false,
            nologo: true,
            executable: nunitConsole
        }));
});

// Usage: gulp dist -m "patch notes"
// Usage: gulp dist (test mode)
gulp.task('dist',['version', 'compile', 'package'], function() {

    console.log('Please wait while npm trys to install your release candidate...');
    console.log(exec('npm install . -g').stdout);
    
    if (argv.m)
    {
        console.log("Publishing to npm...");
        console.log(exec('npm publish').stdout);
        console.log("Pushing to GitHub...");
        console.log(exec('git commit -a -m \"' + argv.m + '\"').stdout);
        console.log(exec('git push origin master').stdout);
    }
});

// Usage: gulp version -m "patch notes"
gulp.task('version', function() {
    if (argv.m)
    {
        console.log('Versioning...');
        console.log(exec('npm version patch').stdout);
    }
});

// Dist depends on both metropolis binaries, Collection Settings (e.g. checkstyle xml config), 
// Collection Binaries (e.g. checkstyle .jar) for eslint, checkstyle, fxcop, etc that parsers 
// use to automate the collection of metrics 

gulp.task('package', ['package-clean'], function() {   
	gulp.src(['build\\*.dll', 'build\\*.exe', 'build\\*.config',
        // exclude all these test files
        '!build\\Octobot.Tests.dll',
        '!build\\FluentAssertions.Core.dll', 
        '!build\\FluentAssertions.dll', 
        '!build\\nunit.framework.dll', 
        '!build\\Moq.dll'])
        .pipe(gulp.dest('dist'));
});

gulp.task('package-clean'), function(){
  del(['dist']);
}
