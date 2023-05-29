$(document).ready(() => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7260/chatHub")
        .withAutomaticReconnect()
        .build();

    async function start() {
        try {
            connection.start();
        }
        catch(error) {
            setTimeout(()=> start(), 2000)
        }
    }

    start()

    const status = $("#status")
    connection.onreconnecting(error => {
        status.css("background-color", "blue")
        status.css("color", "white")
        status.html("Connecting...")
        status.fadeIn(2000, () => {
            setTimeout(() => {
                status.fadeOut(2000)
            })
        })
    })

    connection.onreconnected(connectionId => {
        status.css("background-color", "green")
        status.css("color", "white")
        status.html("Connected!")
        status.fadeIn(2000, () => {
            setTimeout(() => {
                status.fadeOut(2000)
            })
        })
    })

    connection.onclose(connectionId => {
        status.css("background-color", "red")
        status.css("color", "white")
        status.html("Not connected!")
        status.fadeIn(2000, () => {
            setTimeout(() => {
                status.fadeOut(2000)
            })
        })
    })

    $("#send").click(() => {
        let message = $("#textMessage").val();
        connection.invoke("SendMessage", message);
    });

    connection.on("receiveMessage", message => {

        $("#messages").append(message + "</br>")
    })

});
