function CreateChild(message, now) {
    var divOutG = document.createElement('div');
    divOutG.className = "outgoing_msg";
    var divSentM = document.createElement('div');
    divSentM.className = "sent_msg";
    var p = document.createElement('div');
    p.className = 'sent_msg_text';
    p.textContent = message;
    var span = document.createElement('span');
    span.className = "time_date";
    span.textContent = GetMMDD(now) + ' ' + GetHHMM(now);
    divSentM.appendChild(p);
    divSentM.appendChild(span);
    divOutG.appendChild(divSentM);
    return divOutG;
}