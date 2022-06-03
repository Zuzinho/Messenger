function ChangeLastMsg(message, now) {
    document.querySelector(".active_chat").getElementsByTagName('span')[0].textContent = GetMMDD(now);
    var p = document.querySelector(".active_chat").getElementsByTagName('p')[0];
    p.querySelector(".nameSend").textContent = 'You: ';
    p.querySelector(".lastMsgText").textContent = message;
}