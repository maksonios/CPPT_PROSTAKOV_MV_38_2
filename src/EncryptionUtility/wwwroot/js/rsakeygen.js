$.ajax({
    type: 'POST',
    url: '/RSAKeygen/GenerateRsaKey',
    data: {
        type: $('#length').val()
    },
    success: (response) => {
        console.log(response);
    },
    error: (response) => {
        console.log(response);
    },
    //dataType: dataType
});

$('#generate').click(function() {
        $.post('/RSAKeygen/GenerateRsaKey', function(data) {
            $('#private').val(data.privateKey);
            $('#public').val(data.publicKey);
        });
});

