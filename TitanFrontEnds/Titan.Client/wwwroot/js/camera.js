'use strict';

const video = document.getElementById('video');
const canvas = document.getElementById('canvas');
const snap = document.getElementById("snap");
const errorMsgElement = document.querySelector('span#errorMsg');

//const constraints = {
//    audio: false,
//    video: {
//        width: 640, height: 480,

//    }
//};

// RearCam
const constraints = {
    audio: false,
    video: true,
    video: {
        width: 640, height: 480, facingMode: {
            exact: 'environment'
        }
    }
};

//const constraints = {
//    audio: false,
//    video: true,
//    video: {
//        width: 640, height: 480, facingMode: 'user'

//    }
//};

// Access webcam
async function init() {
    try {
        const stream = await navigator.mediaDevices.getUserMedia(constraints);
        handleSuccess(stream);
    } catch (e) {
        errorMsgElement.innerHTML = `navigator.getUserMedia error:${e.toString()}`;
    }
}

// Success
function handleSuccess(stream) {
    window.stream = stream;
    video.srcObject = stream;
}

// Load init
init();

// Draw image Original
var context = canvas.getContext('2d');
snap.addEventListener("click", function () {
    context.drawImage(video, 0, 0, 640, 480);
    var dataurl = canvas.toDataURL("image/png");
    console.log(dataurl);

    var dataurl = canvas.toDataURL("image/png");
    document.getElementById("Image").value = dataurl;
});

//// Draw image Original
//var context = canvas.getContext('2d');
//snap.addEventListener("click", function () {
//    context.drawImage(video, 0, 0, 640, 480);
//    var dataurl = canvas.toDataURL("image/jpeg", 1.0);
//    document.getElementById("Image").value = dataurl;
//    canvas.toDataURL("image/png").replace("image/png", "image/octet-stream");

//});