var archiveDropzone = new Dropzone("#archive-dropzone", {
    url: '/archive-helper/upload',
    maxFiles: 5,
    maxFilesize: 5,
    autoProcessQueue: false,
    uploadMultiple: true,
    paramName: 'files',
    maxfilesexceeded: function (files) {
        this.removeAllFiles();
        this.addFile(files);
    },
    parallelUploads: 10
});

$("#submit").click(function (e) {
    e.preventDefault();

    archiveDropzone.processQueue();

    archiveDropzone.on("complete", function (response) {
        if (response.xhr.status === 200) {
            console.log(response);
            //var downloadLink = document.createElement("a");
            // var archiveInfo = JSON.parse(response.xhr.response);
            // var archiveId = archiveInfo.id;
            // var archiveName = "encrypted_archive.zip";
            //downloadLink.href = '/archive-helper/download';
            //downloadLink.setAttribute("download", archiveName);
            //downloadLink.click();
        } else {
            console.log("Upload failed.");
        }
    });
});