var encryptDropzone = new Dropzone("#encrypt-dropzone", {
    url: '/rsa-encrypt/upload',
    maxFiles: 1,
    maxFilesize: 3,
    autoProcessQueue: false,
    multiple: false,
    maxfilesexceeded: function (files) {
        this.removeAllFiles();
        this.addFile(files);
    },
    sending: (file, xhr, formData) => {
        formData.append("publicKey", $("#encrypt-public").val());
    },
});

$("#encrypt-encrypt").click(function (e) {
    e.preventDefault();

    encryptDropzone.processQueue();

    encryptDropzone.on("complete", function (response) {
        if (response.xhr.status === 200) {
            console.log(response);
            var downloadLink = document.createElement("a");
            var fileNameInfo = JSON.parse(response.xhr.response);
            var fileId = fileNameInfo.id;
            var fileName = fileNameInfo.name;
            downloadLink.href = '/rsa-encrypt/download/'+ fileId;
            downloadLink.setAttribute("download", fileName);
            downloadLink.click();
        } else {
            console.log("Upload failed.");
        }
    });
});

var decryptDropzone = new Dropzone("#decrypt-dropzone", {
    url: '/rsa-encrypt/upload',
    maxFiles: 1,
    maxFilesize: 3,
    autoProcessQueue: false,
    multiple: false,
    maxfilesexceeded: function (files) {
        this.removeAllFiles();
        this.addFile(files);
    },

});

$("#decrypt-decrypt").click(function (e) {
    e.preventDefault();
    decryptDropzone.processQueue();
});