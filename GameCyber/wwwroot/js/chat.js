"use strict";

console.log('check chat.js');

const connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
const userInput = document.getElementById("userInput");
const userReceive = document.getElementById("userReceive");
//Disable the send button until connection is established.
document.getElementById("btn_sendMsg").disabled = true;

connection.on("SendMessage", function (message) {
    let messageContainer = document.getElementById("msg_container");
    let messageElement = ` <div class="outgoing_msg">

                                    <div class="sent_msg">
                                        <p>
                                            ${message}
                                        </p>
                                    </div>
                                </div>`;
  
    messageContainer.insertAdjacentHTML("beforeend", messageElement);
});
connection.on("ReceiveMessage", function (message, userSendId) {
    debugger
    let curChatBox = userReceive.value;
    if (curChatBox != userSendId) return;
    var currentdate = new Date();
    var datetime = currentdate.getDate() + "/"
        + (currentdate.getMonth() + 1) + "/"
        + currentdate.getFullYear() + " - "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    let messageContainer = document.getElementById("msg_container");
    let messageElement = `<div class="incoming_msg">
                                    <div class="incoming_msg_img"> <img src="https://cdn-icons-png.flaticon.com/512/219/219983.png" alt="sunil"> </div>
                                    <div class="received_msg">
                                        <div class="received_withd_msg">
                                            <p>
                                               ${message}
                                            </p>
                                        </div>
                                    <span style="font-size:8px"><em>${datetime}</em></span>
                                    </div>
                                </div>`;

    messageContainer.insertAdjacentHTML("beforeend", messageElement);
});

connection.start().then(function () {
    document.getElementById("btn_sendMsg").disabled = false;
    connection.invoke("UpdateConnectionId", userInput.value);
    
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btn_sendMsg").addEventListener("click", function (event) {
    const message = document.getElementById("messageInput");

    connection.invoke("SendMessage", userReceive.value, userInput.value, message.value).catch(function (err) {
        return console.error(err.toString());
    });
    message.value = '';
    event.preventDefault();
});

