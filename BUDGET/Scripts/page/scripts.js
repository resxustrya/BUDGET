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


function openWindowWithPost(url, data, allotmentID) {
    var form = document.createElement("form");
    form.target = "_blank";
    form.method = "POST";
    form.action = url;
    form.style.display = "none";

    for (var key in data) {
        var input = document.createElement("input");
        input.type = "hidden";
        input.name = "rows[]";
        input.value = data[key];
        form.appendChild(input);
    }

    var allotment = document.createElement("input");
    allotment.type = "hidden";
    allotment.name = "allotmentID";
    allotment.value = allotmentID;
    form.appendChild(allotment);

    document.body.appendChild(form);
    form.submit();
    document.body.removeChild(form);
}

function selectedRows(selection, tempdata,col)
{
    var data = [];
    if (selection.length > 1) {
        for (var row = 0; row < selection.length; row++) {
            data[row] = tempdata[selection[row][0]][col];
        }
    } else if (selection.length == 1) {
        var index = 0;
        for (var row = selection[0][0]; row <= selection[0][2]; row++, index++) {
            data[index] = tempdata[row][col];
        }
    }
    return data;
}

function orsUACS(id)
{
    if (id) {
        var url = $("#ors_table").data('uacs_amount') + "/" + id;
        $("#ors_modal").modal({
            backdrop: 'static',
            keyboard: false,
            show: true
        });
        $(".ors_body").html('');
        $(".loading").show();
        $.get(url, function (res) {
            $(".ors_body").html(res);
            $(".loading").hide();
        });
    } else {
        Lobibox.alert("warning",
        {
            msg: "This row is not yet saved. Press Ctrl + S to save",
            title: "UACS Amount Entry / Disbmt"
        });
    }
}

function deleteRows(url,data)
{
    var rowID = [];
    for (key in data) {
        rowID[key] = {
            "ID" : data[key]
        }
    }
    var postData = {
        data: JSON.stringify(rowID)
    };
    $.post(url, postData, function (resdata) {
    });
}