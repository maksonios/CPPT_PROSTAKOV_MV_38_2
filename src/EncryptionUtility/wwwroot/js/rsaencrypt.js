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
    
});

$("#encrypt-encrypt").click(function (e) {
    e.preventDefault();
    encryptDropzone.processQueue();
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