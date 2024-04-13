// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// Hiển thị popup
function showPopup() {
    document.getElementById("smallPopup").style.display = "block";
}


// Khi người dùng nhấp vào nút đóng
document.querySelector(".close").addEventListener("click", function () {
    hidePopup();
});
