function AddMessage(input,now) {
    var message = input.value;
    document.querySelector(".msg_history").appendChild(CreateChild(message, now));
    scroll();
    ClearInput(input);
}