$('#generate').click(function() {
    $.post('/RSAKeygen/GenerateRsaKey', {keySize: $('#key_size').val()}, function(data) {
        $('#private').val(data.privateKey);
        $('#public').val(data.publicKey);
    });
});

