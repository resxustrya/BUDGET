$(document).ready(function () {
    $("#gaa_upload").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $(this).data('url');
        setTimeout(function () {
            $.get(url, function (res) {
                $('.modal_body').html(res);
                $('.loading').hide();
            });
        },1000);
    });

    $("#upload_mooe").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $(this).data('url');
        setTimeout(function () {
            $.get(url, function (res) {
                $('.modal_body').html(res);
                $('.loading').hide();
            });
        }, 1000);
    });
});

