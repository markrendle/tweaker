$(function() {
    $.get("/broken", function(data) {
        console.log(data);
    });
});