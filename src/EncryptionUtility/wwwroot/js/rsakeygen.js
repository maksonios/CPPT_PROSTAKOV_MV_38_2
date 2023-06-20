$('#pair-generate').click(function() {
    $.post('/rsa-key/generate-pair', {keySize: $('#key_size').val()}, function(data) {
        $('#pair-private').val(data.privateKey);
        $('#pair-public').val(data.publicKey);
    }).fail(function(response) {
        console.log('Error: ' + response.responseText);
    });
});

$('#public-generate').click(function() {
    $.post('/rsa-key/generate-public', {privateKey: $('#public-private').val()}, function(data) {
        $('#public-public').val(data);
    });
});

