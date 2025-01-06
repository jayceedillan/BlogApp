// Ensure the DOM is ready
$(function () {
    // Validate inputs and selects on input
    $("input, select").on("input", function () {
        $(this).valid();
    });

    // Handle file input changes for banner image preview
    $('#BannerImage').on('change', function (event) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagePreview')
                .attr('src', e.target.result)
                .show();
        };

        reader.readAsDataURL(event.target.files[0]);
    });
});
