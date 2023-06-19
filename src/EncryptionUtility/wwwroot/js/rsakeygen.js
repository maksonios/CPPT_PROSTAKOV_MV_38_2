$('#home-generate').click(function() {
    $.post('/RSAKeygen/GenerateRsaKey', {keySize: $('#key_size').val()}, function(data) {
        $('#home-private').val(data.privateKey);
        $('#home-public').val(data.publicKey);
    });
});

$('#profile-generate').click(function() {
    $.post('/RSAKeygen/GeneratePublicRsaKey', {privateKey: $('#profile-private').val()}, function(data) {
        $('#profile-private').val(data.privateKey);
        $('#profile-public').val(data.publicKey);
    });
});

