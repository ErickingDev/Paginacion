// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function zoomImage(event) {
    var zoomedImg = event.target.parentNode.querySelector('.zoomed-image');
    var rect = event.target.getBoundingClientRect();
    var offsetX = event.clientX - rect.left;
    var offsetY = event.clientY - rect.top;
    var scale = 1.2;

    zoomedImg.style.transformOrigin = offsetX + 'px ' + offsetY + 'px';
    zoomedImg.style.transform = 'translate(-50%, -50%) scale(' + scale + ')';
    zoomedImg.style.display = 'block';
}

function resetZoom() {
    var zoomedImgs = document.querySelectorAll('.zoomed-image');
    zoomedImgs.forEach(function (zoomedImg) {
        zoomedImg.style.transform = 'translate(-50%, -50%) scale(1)';
        zoomedImg.style.display = 'none';
    });
}