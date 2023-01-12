// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ShowNotification(innerHTML) {
    _WriteNotification(innerHTML, innerHTML, "Notification")
}

function ShowError(error) {
    var notificationErrorOccured = "<h1 class='text-danger'>An error occured while processing your request</h1>";
    var notificationDetails = "<h3 class='text-danger'><q>";
    var notificationMiddle = "</q></h3><br /><h4 class='text-danger'>Please contact an administrator if this is a recurring problem</h4>";
    var notificationRef = "<h4 class='text-danger'>Reference: <code>";
    var notificationEnd = "</code></h4>";

    var notificationHTML = notificationErrorOccured + notificationRef + error.correlationId + notificationEnd
    var modalHTML = notificationErrorOccured + notificationDetails + error.message + notificationMiddle + notificationRef + error.correlationId + notificationEnd

    _WriteNotification(notificationHTML, modalHTML, "Error")
}

function _WriteNotification(notificationHTML, modalHTML, modalTitle) {
    $("#notification").html(notificationHTML);
    $('#notificationModalBody').html(modalHTML);
    $('#notificationModalTitle').html(modalTitle);
    $("#notification").addClass("show");
    setTimeout(function () { $("#notification").removeClass("show"); }, 3000);
}

function HandleXHRError(xhr) {
    if (xhr.status == 401) {
        var currentLoc = window.location.href;
        window.location.href = `/Account/Login?ReturnUrl=${encodeURIComponent(currentLoc)}`;
    }

    if (xhr?.responseJSON?.message != undefined) {
        ShowError(xhr.responseJSON);
    }
    else {
        ShowNotification("<h1 class='text-danger'>An error occured while processing your request</h1><br /><h4 class='text-danger'>Please contact an administrator if this is a recurring problem</h4>")
    }
}