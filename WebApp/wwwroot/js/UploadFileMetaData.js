
// show metadata of uploaded file
function showMetaData(id) {
    var trs = document.getElementsByTagName("tr");
    for (let i = 0; i < trs.length; i++) {
        trs[i].style.display = "none";
    }

    var tr = document.getElementsByClassName(id);
    for (let i = 0; i < tr.length; i++) {
        tr[i].style.display = "block";
    }
}