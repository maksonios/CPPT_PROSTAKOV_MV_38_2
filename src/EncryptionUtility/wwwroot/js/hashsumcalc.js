var dropzone = new Dropzone("#my-dropzone", {
    url: '/hash-sum/upload',
    maxFiles: 1,
    maxFilesize: 3,
    autoProcessQueue: false,
    multiple: false,
    maxfilesexceeded: function (files) {
        this.removeAllFiles();
        this.addFile(files);
    },
    sending: (file, xhr, formData) => {
        formData.append("algorithm", $("#algorithm").val());
    },
    success: (response) => {
        var result = response.xhr.response.substring(1, response.xhr.response.length-1);
        $("#result").val(result);
    }
});

$("#submit").click(function (e) {
    e.preventDefault();
    dropzone.processQueue();
});