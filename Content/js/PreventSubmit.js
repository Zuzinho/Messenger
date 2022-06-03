var form = document.getElementById("sendMessageForm");
form.addEventListener("submit", function (event) {
    event.preventDefault();
    var input = document.getElementsByTagName("input")[1];
    var message = input.value;
    if (message == '') return;
    var now = new Date();
    AddMessage(input,now);
    ChangeLastMsg(message,now);
    axios.post(
        '/Home/AddMessage',
        { 'message': message}
    ).then(res => {
        console.log(res);
    })
})