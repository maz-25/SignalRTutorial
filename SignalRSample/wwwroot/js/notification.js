//establishing connection
var connectionNotification = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notification").build();

document.getElementById("sendButton").disabled = true;

//prikazivanje svih notifikacija 
connectionNotification.on("LoadNotification", function (message, counter) {
    document.getElementById("messageList").innerHTML = "";
    var notificationCounter = document.getElementById("notificationCounter");
    notificationCounter.innerHTML = "<span>(" + counter + ")</span>";
    for (let i = message.length - 1; i >= 0; i--) {
        var li = document.createElement("li");
        li.textContent = "Notification - " + message[i];
        document.getElementById("messageList").appendChild(li);
    }
});

// na click pozovi SendMessage
document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("notificationInput").value;
    connectionNotification.send("SendMessage", message).then(function () {
        document.getElementById("notificationInput").value = "";
    });
    event.preventDefault();
});

//ako je komunikacija uspostavljena enablaj sendButton
connectionNotification.start().then(function () {
    connectionNotification.send("LoadMessages")
    document.getElementById("sendButton").disabled = false;
});
