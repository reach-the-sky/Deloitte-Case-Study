﻿
// show metadata of uploaded file
var showMetadata = function (e, fileId) {
    var url = "/Home/FileMetadata?file=" + fileId;
    $("a.active").removeClass("active");
    e.classList.add("active");
    $("#metadata").load(url);

}