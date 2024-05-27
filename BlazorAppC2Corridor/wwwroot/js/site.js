function loadCssFile(filename) {
    var file = document.createElement("link");
    file.setAttribute("rel", "stylesheet");
    file.setAttribute("type", "text/css");
    file.setAttribute("href", filename);
    file.setAttribute("id", filename);
    document.head.appendChild(file);
}

function unloadCssFile(filename) {
    var file = document.getElementById(filename);
    if (file) {
        file.parentNode.removeChild(file);
    }
}
