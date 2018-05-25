$(document).ready(function () {
    $("#gaa_upload").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $(this).data('url');
        
        $.get(url, function (res) {
            $('.modal_body').html(res);
            $('.loading').hide();
        });
        
    });

    $("#upload_prex").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $(this).data('url');

        $.get(url, function (res) {
            $('.modal_body').html(res);
            $('.loading').hide();
        });
    });

    $("#uacs_upload").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $(this).data('url');

        $.get(url, function (res) {
            $('.modal_body').html(res);
            $('.loading').hide();
        });
    });


    $("#print_ors").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $(this).data('url');

        $.get(url, function (res) {
            $('.modal_body').html(res);
            $('.loading').hide();
        });
    });
});


function getBaseUrl() {
    var url = window.location.href;
    var segments = url.split('/');
    
    var hostname = window.location.hostname;
    var protocol = window.location.protocol;
    var url = protocol + "//" + hostname + "/" + segments[3] + "/";
    return url;
}