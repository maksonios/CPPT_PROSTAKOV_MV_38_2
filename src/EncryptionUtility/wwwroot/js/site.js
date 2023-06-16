var currentUrl = window.location.pathname;
var segments = currentUrl.split('/');
var controllerName = segments[1];
var dropzone = new Dropzone("#my-dropzone", { 
    url: '/' + controllerName + '/upload',
    maxFiles: 1,
    params: {
        kurwa: "asd"
    }
});

dropzone.on('success', function (response) {
    var result = response.xhr.response.substring(1, response.xhr.response.length-1);
    $("#result").val(result);
});

dropzone.on("sending", function(file, xhr, formData) {
    $("#algorithm").val();
    formData.append("algorithm", $("#algorithm").val());
});

