// Establish connection to the hub
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

// Function to handle incoming messages
connection.on("ReceiveMessage", function (role, message) {
    var chatBox = $("#chat-box");
    var messageHtml = '<div class="card mb-2">' +
        '<div class="card-body ' + (role === "user" ? 'bg-light' : 'bg-info text-white') + '">' +
        '<strong>' + (role === "user" ? "You" : "GPT") + ':</strong>' +
        '<p class="mb-0">' + message + '</p>' +
        '</div>' +
        '</div>';
    chatBox.append(messageHtml);
    chatBox.scrollTop(chatBox[0].scrollHeight);
    console.log('1', messageHtml);
});

// Start the connection
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// Function to send messages
$("#sendButton").click(function (event) {
    event.preventDefault();

    var userInput = $("#userInput").val();
    connection.invoke("SendMessage", userInput).catch(function (err) {
        return console.error(err.toString());
    });
    $("#userInput").val("").focus();
    console.log('2', userInput);
});