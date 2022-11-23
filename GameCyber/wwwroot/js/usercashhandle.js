
$(document).ready(function () {

    function Logout() {
        window.location.href = "/Index?Handler=Logout";
    }
    function UpdateCash() {
        $.ajax({
            url: '/Home?Handler=UpdateCash',
            type: "GET",
            data: {
            },
            dataType: 'JSON',
            success: function (result) {
                console.log('Cur cash: ' + result.curCash + ' time: ' + result.timeRemain + ' Continue' + result.isCanPlay);
                if (result.curCash == 500) {
                    alert('Tai khoan cua ban sap het, vui long nap them de tiep tuc su dung !');
                    $("#userCash").text(result.curCash);
                    $("#userTimeRemain").text(result.timeRemain);
                }
                else if (!result.isCanPlay) {
                    alert('Tai khoan cua ban da het, vui long nap them de tiep tuc su dung !');
                    Logout();
                }
                else {
                    $("#userCash").text(result.curCash);
                    $("#userTimeRemain").text(result.timeRemain);
                }
            },
            error: function (result) { alert('Co loi xay ra khi update cash'); },
        });
    }
    //$("#test").click(UpdateCash);
    let intervalid = window.setInterval(UpdateCash, 60 * 1000);
    
});