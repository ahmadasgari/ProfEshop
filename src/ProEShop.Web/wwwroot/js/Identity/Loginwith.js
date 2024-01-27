var countDownTimerInterval;
$(document).ready(function () {
    var countDownTimerInterval = setInterval(countDown, 1000);
    countDown();
});



var minute = parseInt($('div[count-down-timer-minute]').attr('count-down-timer-minute'));
var second = parseInt($('div[count-down-timer-second]').attr('count-down-timer-second'));
    
function setCountDownTimeBox() {
        $('#count-down-timer-box').html(`${minute}:${second < 10 ? '0' + second : second}`);
}
function countDown() {
        if (second == 0) {
            if (second == 0 && minute == 0) {

                $('#count-down-timer-box').parent().addClass('d-none');
                $('#send-user-activation-sms-box').removeClass('d-none');
                clearInterval(countDownTimerInterval);
            }
            else {
                
                minute--;
                second = 59;
            }
        }
        else {
            second--;
        }
        setCountDownTimeBox();
}


function reSendActivationCode(phoneNumber, e, reSendUrl) {
    showLoading();
    var objectToSend = {
        phoneNumber: phoneNumber,
        __RequestVerificationToken: getRVT(e)
    }
    $.post(reSendUrl, objectToSend, function (data, status) {
        hideLoading();
        if (status == 'success' && data.isSuccessful) {
            showToastr('success', data.message);
            $('#activation-code-box').html(data.data.activationCode);
            $('#count-down-timer-box').parent().removeClass('d-none');
            $('#send-user-activation-sms-box').addClass('d-none');
            minute = 3;
            second = 0;
            setCountDownTimeBox();
            countDownTimerInterval = setInterval(countDown, 1000);

        }
        else {
            showToastr('error', data.message);
        }
    }).fail(function () {
        console.log("hello3");
        showToastr('error', 'خطایی به وجود آمد، لطفا مجددا تلاش نمایید');
    });

}
function getRVT(e) {
    return $(e).parents('form').find(`input[name="${rvt}"]`).val();
}
function onBeginLoginWithPhoneNumber() {
    showLoading();
}
function onCompleteLoginWithPhoneNumber() {
    hideLoading();
}
function onFailureLoginWithPhoneNumber() {
    alert("salam");
    showToastr('error', 'خطایی به وجود آمد، لطفا مجددا تلاش نمایید');
}

function onSuccessLoginWithPhoneNumber(data, status) {
    if (status == 'success' && data.isSuccessful) {
        console.log(data);
        showToastr('success', 'شما با موفقیت وارد شدید');
        location.href = '/Identity/Test';
    }
    else {
        showToastr('error', 'خطایی به وجود آمد، لطفا مجددا تلاش نمایید');
    }
}