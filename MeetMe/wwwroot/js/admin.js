
var previewImageInitialSrc = null;
var previewImageInitialVisible = null;
// data-preview-image-target="#img-id"
$("[data-preview-image-target]").on("input", function (event) {
    var input = this;
    var targetImg = $(this).data("preview-image-target");
    var img = $(targetImg)[0];
    console.log(img);
    if (previewImageInitialVisible == null)
        previewImageInitialVisible = img.style.display != "none";
    if (previewImageInitialSrc == null) {
        previewImageInitialSrc = img.src;
    }

    // https://stackoverflow.com/questions/4459379/preview-an-image-before-it-is-uploaded
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        // reader okumayı bitirdiğinde
        reader.onload = function (e) {
            // okunan resmi <img ..> elementi üzerinde göster
            img.src = e.target.result;
            img.style.display = "inline";
        };

        reader.readAsDataURL(input.files[0]);
    }
    else {
        if (previewImageInitialVisible) {
            img.src = previewImageInitialSrc;
        }
        else {
            img.style.display = "none";
        }
    }
});