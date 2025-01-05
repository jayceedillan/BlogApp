$(document).ready(function () {
    $("input, select").on("input", function () {
        $(this).valid();
    });
});