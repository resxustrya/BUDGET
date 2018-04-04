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

    $("#upload_mooe").click(function () {
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
    var re = new RegExp(/^.*\//);
    return re.exec(window.location.href);
}