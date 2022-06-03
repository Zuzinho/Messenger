function scroll() {
    var div = document.querySelector('.msg_history');
    div.scrollTo(0, div.scrollHeight);
}

window.onload = scroll();