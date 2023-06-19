var dropzone = new Dropzone("#home-dropzone", {
    url: '/RSAEncrypt/Upload',
    maxFiles: 1,
    maxFilesize: 3,
    autoProcessQueue: false,
    multiple: false,
    maxfilesexceeded: function (files) {
        this.removeAllFiles();
        this.addFile(files);
    },
    
});

$("#home-encrypt").click(function (e) {
    e.preventDefault();
    dropzone.processQueue();
});