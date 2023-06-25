var archiveDropzone = new Dropzone("#archive-dropzone", {
    url: '/archive-helper/upload',
    maxFiles: 5,
    maxFilesize: 5,
    autoProcessQueue: false,
    uploadMultiple: true,
    paramName: 'files',
    parallelUploads: 10,
    maxfilesexceeded: function (files) {
        this.removeAllFiles();
        this.addFile(files);
    },
    sending: (file, xhr, formData) => {
        formData.append("password", $("#archive-password").val());
    }
});

$("#submit").click(function (e) {
    e.preventDefault();

    archiveDropzone.processQueue();
    var downloadTrigger = false;
    archiveDropzone.on("complete", function (response) {
        if (!downloadTrigger && response.xhr.status === 200) {
            console.log(response);
            var downloadLink = document.createElement("a");
            var fileNameInfo = JSON.parse(response.xhr.response);
            var fileId = fileNameInfo.id;
            var fileName = fileNameInfo.name;
            downloadLink.href = '/archive-helper/download/'+ fileId;
            downloadLink.setAttribute("download", fileName);
            downloadLink.click();
            downloadTrigger = true;
        } else {
            console.log("Upload failed.");
        }
    });
});