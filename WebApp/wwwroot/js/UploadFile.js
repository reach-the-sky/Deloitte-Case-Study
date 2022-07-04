
// Get date from upload file
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

