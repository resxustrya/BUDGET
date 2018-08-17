$(document).ready(function () {

    $(document).on('focus', ':input', function () {
        $(this).attr('autocomplete', 'off');
    });

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

    $("#saob").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $("#saob").data('url');
        $.get(url, function (res) {
            $('.modal_body').html(res);
            $('.loading').hide();
        });
    });

    $("#saobsheet2").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $("#saobsheet2").data('url');
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

    $("#upload_orsmooe").click(function () {
        $("#page_modal").modal('show');
        $('.loading').show();
        $('.modal_body').html('');
        var url = $(this).data('url');

        $.get(url, function (res) {
            $('.modal_body').html(res);
            $('.loading').hide();
        });
    });

    $("#ors_page").click(function () {
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


$(window).resize(function () {
    resize();
});

function resize() {
    var d_h = window.innerHeight;
    var h = d_h - 115;
    $("#ors_table").height(h);
}
