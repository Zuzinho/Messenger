function GetMMDD(now) {
    var months = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"];
    var month = months[now.getMonth()];
    var day = now.getDate();
    return month.slice(0, 3) + ' ' + day;
}