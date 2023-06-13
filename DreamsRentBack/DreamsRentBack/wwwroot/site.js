//$(document).ready(() => {
//    let connection = new signalR.HubConnectionBuilder()
//        .withUrl("/chatHub")
//        .withAutomaticReconnect()
//        .build();

//    connection.start();

//    console.log(connection)

//    let chatId = window.location.href.split('=')[1];
//    let chatContainer = $("#partialContainer");

//    $("#messageForm").submit(async function (e) {
//        e.preventDefault();
//        let messageContent = $('#messageInput').val();

//        console.log(messageContent)

//        if (!messageContent.trim()) {
//            return;
//        }

//        let formData = new FormData(this);

//        await fetch(`/Chat/SendMessage?chatId=${chatId}&message=${messageContent}`, {
//            method: 'POST',
//            body: formData
//        })
//            .then(res => res.text())
//            .then(data => {
//                console.log(data)
//                chatContainer.append(data)
//                $('#messageInput').val("")
//            })
//    })

//    connection.on("ReceiveMessage", (message) => {
//        let chatcontainers = document.querySelectorAll("#chatme")
//        chatcontainers.forEach(Element => {

//            Element.append(message)
//        })
//    })

//});
$(document).ready(() => {
    let connection = new signalR.HubConnectionBuilder()
        .withUrl("/chatHub")
        .withAutomaticReconnect()
        .build();

    connection.start();

    console.log(connection);

    let chatId = window.location.href.split('=')[1];
    let chatContainer = $("#partialContainer");

    $("#messageForm").submit(async function (e) {
        e.preventDefault();
        let messageContent = $('#messageInput').val();

        console.log(messageContent);

        if (!messageContent.trim()) {
            return;
        }

        let formData = new FormData(this);

        await fetch(`/Chat/SendMessage?chatId=${chatId}&message=${messageContent}`, {
            method: 'POST',
            body: formData
        })
            .then(res => res.text())
            .then(data => {
                console.log(data);
                chatContainer.append(data);
                $('#messageInput').val("");
            });

                connection.on("ReceiveMessage", (message) => {
        let chatcontainers = document.querySelectorAll("#chatme")
        chatcontainers.forEach(Element => {

            Element.append(message)
        })
    })

        scrollToBottom(); // Ekranı en alta kaydır
    });
});

function scrollToBottom() {
    var chatingBody = document.querySelector(".chating-body");
    chatingBody.scrollTop = chatingBody.scrollHeight;
}

window.onload = scrollToBottom;