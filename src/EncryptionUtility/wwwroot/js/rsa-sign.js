var signatureDropzone = new Dropzone("#signature-dropzone", {
    url: '/rsa-signature/generate',
    maxFiles: 1,
    maxFilesize: 3,
    autoProcessQueue: false,
    uploadMultiple: false,
    maxfilesexceeded: function (files) {
        this.removeAllFiles();
        this.addFile(files);
    },
    sending: (file, xhr, formData) => {
        formData.append("privateKey", $("#signature-key").val());
    },
});

$("#sign-sign").click(function (e) {
    e.preventDefault();

    signatureDropzone.processQueue();

    signatureDropzone.on("complete", function (response) {
        if (response.xhr.status === 200) {
            console.log(response);
            var result = response.xhr.response.substring(1, response.xhr.response.length-1);
            $("#signature-result").val(result);
            
        } else {
            console.log("Upload failed.");
        }
    });
});


var verifyDropzone = new Dropzone("#verification-dropzone", {
    url: '/rsa-signature/verify',
    maxFiles: 1,
    maxFilesize: 3,
    autoProcessQueue: false,
    uploadMultiple: false,
    maxfilesexceeded: function (files) {
        this.removeAllFiles();
        this.addFile(files);
    },
    sending: (file, xhr, formData) => {
        formData.append("publicKey", $("#verification-key").val());
        formData.append("signature", $("#verification-signature").val());
    },
});

$("#verify-verify").click(function (e) {
    e.preventDefault();

    verifyDropzone.processQueue();

    verifyDropzone.on("complete", function (response) {
        if (response.xhr.status === 200) {
            console.log(response);
            var result = response.xhr.response;
            $('#verification-result').val(result);
        } else {
            console.log("Upload failed.");
        }
    });
});