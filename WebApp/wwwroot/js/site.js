// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function getFileDate() {
    var x = document.getElementById("myFile");
    var lastModified;
    if ('files' in x) {
        if (x.files.length != 0) {
            var file = x.files[0];
            if ('lastModified' in file) {
                lastModified = new Date(file.lastModified);
            }
        }
    }
    console.log(lastModified);
    document.getElementById("metadata").value = lastModified.toISOString().slice(0, 16);
}