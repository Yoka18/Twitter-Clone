function Test() {
    var a = document.getElementsByClassName("dropdown-content");
    if (a.children.style.display === 'none') {
        a.children.style.display = 'block';
    }else{
        a.children.style.display = 'none';
    }
}