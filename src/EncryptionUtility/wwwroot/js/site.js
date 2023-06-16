var currentUrl = window.location.pathname;
var segments = currentUrl.split('/');
var controllerName = segments[1];

new Dropzone("#my-dropzone", { 
    url: '/' + controllerName + '/upload',
    maxFiles: 1,
    sending: (file, xhr, formData) => {
        formData.append("algorithm", $("#algorithm").val());
    },
    success: (response) => {
        var result = response.xhr.response.substring(1, response.xhr.response.length-1);
        $("#result").val(result);
    }
});
