function GetHHMM(now) {
    var hours = now.getHours();
    var minutes = now.getMinutes();
    return hours + ':' + minutes;
}