// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const userName = localStorage.getItem('loggedUser');
if (userName) {
    document.getElementById('admin-status').innerHTML = `<p>Welcome, ${userName}! <a href="/account/logout">log out</a></p>`

    $(function () {
        var notificationConnection = new signalR.HubConnectionBuilder().withUrl("/notificationHub?role=manager").build();

        notificationConnection.on("notify", function (message) {
            alert(message);
            window.location.href = '/Admin/OngoingOrders';
        });

        notificationConnection.start().catch(function (err) {
            return console.error(err.toString());
        });
    });
}

