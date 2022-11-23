


$(document).ready(function () {
    console.log('check chat event .js');
    $(".chat_list").click(function () {
        /*        alert("Handler for .click() called." + " 3" + $(this).data('value'));*/

        $(this).siblings().not(this).removeClass('active_chat');
        $(this).toggleClass('active_chat');
        window.location.href = '/Chat?Handler=LoadChat&userReceiveId=' + $(this).data('value');
    });
});