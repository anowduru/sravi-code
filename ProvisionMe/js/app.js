
function onenvironmentchange(src) {
    refreshApplicationList();
    onapplicationchange($("#lstApplication")[0]);
    resetMsQuoteRoles();
    if ($('#lstApplication').val() != "eMSL") {
        $('#txtPcn').val("");
        $('#chkPQRole').prop('checked', false);
    }
}

function refreshApplicationList() {
    var lstApplication = $('#lstApplication')[0];
    var lstEnvironment = $('#lstEnvironment')[0];

    lstApplication.options.length = 0;

    //lstApplication.options[0] = new Option("VBUI", "VBUI");
    //lstApplication.options[1] = new Option("eMSL", "eMSL");
    //lstApplication.options[2] = new Option("MOET", "MOET");
    //lstApplication.options[3] = new Option("MS Quote", "MSQuote");

    if (lstEnvironment.options[lstEnvironment.selectedIndex].getAttribute("hasmoet") == "true") {
        //$('#lstApplication').not(this)
        //    .children('option[value=MOET]')
        //    .prop('disabled', false);

        lstApplication.options[0] = new Option("VBUI", "VBUI");
        lstApplication.options[1] = new Option("eMSL/POET", "eMSL");
        lstApplication.options[2] = new Option("MOET", "MOET");
        lstApplication.options[3] = new Option("MS Quote", "MSQuote");
    }
    else {
        lstApplication.options[0] = new Option("VBUI", "VBUI");
        lstApplication.options[1] = new Option("eMSL/POET", "eMSL");
        lstApplication.options[2] = new Option("MS Quote", "MSQuote");

        //$('#lstApplication').not(this)
        //    .children('option[value=MOET]')
        //    .prop('disabled', true);
    }

    if (lstEnvironment.options[lstEnvironment.selectedIndex].getAttribute("hasmopet") == "true") {
        lstApplication.options[lstApplication.options.length] = new Option("MOPET/AWS", "MOPET");
    }

    //var seletedItem = $('#lstApplication').find(':selected').first();

    //if ($('#lstApplication').first().selectedItem == seletedItem)
    //{

    //}

    var index = $('#selectedApplicationIndex').val() == lstApplication.options.length ? lstApplication.options.length - 1 : $('#selectedApplicationIndex').val();

    lstApplication.options[index].selected = "true";
}

function onapplicationchange(src) {
    var source = src.target ? src.target : src;

    $('#selectedApplicationIndex').prop('value', source.selectedIndex);

    showHideMsQuoteRolePanel(source);
    enableDisablePcnTextBox(source);
}

function onrolechange(src) {
    if (src.checked)
        src.value = "I";
    else
        src.value = "D";
}

function resetMsQuoteRoles() {
    $('#divMSQuoteRoleContainer').find(':checkbox').prop('checked', false);
}

function showHideMsQuoteRolePanel(src) {

    if (src.value == 'MSQuote') {
        $('#divMSQuoteRoleContainer').find(':checkbox').prop('disabled', false);
    }
    else {
        resetMsQuoteRoles();
        $('#divMSQuoteRoleContainer').find(':checkbox').prop('disabled', true);
    }
}

function enableDisablePcnTextBox(src) {
    if (src.value == 'eMSL') {
        $('#txtPcn').prop('disabled', false);
        $('#chkPQRole').prop('disabled', false);
    }
    else {
        $('#txtPcn').val("");
        $('#chkPQRole').prop('checked', false);
        $('#txtPcn').prop('disabled', true);
        $('#chkPQRole').prop('disabled', true);
    }
}

$.validator.addMethod('validAccoutName', function (value, element) {
    var isValid = true;
    var items = $.trim(value).split(',');
    $.each(items, function (index, item) {
        var parts = $.trim(item).split('\\');
        if (parts.length <= 1 ||
            $.trim(parts[0]).length < 1 ||
            $.trim(parts[1]).length < 1) {
            isValid = false;
        }
    });

    return isValid;
}, "Invalid account name.");

function getContent() {
    return '<div id="popover-content-message" name="popover-content-message" style="width:185px;color:red;">{Dynamic Content}</div>';
}

$(function () {
    //$('a, button').click(function () {
    //    $(this).toggleClass('active');
    //});

    refreshApplicationList();

    showHideMsQuoteRolePanel($('#lstApplication')[0]);
    enableDisablePcnTextBox($('#lstApplication')[0]);

    $("#lstEnvironment").on("change", onenvironmentchange);
    $("#lstApplication").on("change", onapplicationchange);

    $("#txtUserName").popover({
        placement: 'top',
        trigger: 'manual',
        html: true,
        content: getContent
    });

    $("#helpUserName").popover({
        placement: 'bottom',
        trigger: 'hover',
        html: true,
        content: '<div style="width:250px"><span>Account name format for single user is <strong>domain</strong>\\alias</span> and <span><strong>domain</strong>\\alias</span>, <span><strong>domain</strong>\\alias</span> for multiple users.</div>'
    });

    $("#helpPCN").popover({
        placement: 'bottom',
        trigger: 'hover',
        html: true,
        content: '<div style="width:245px">Leave it blank for eMSL ROC access.</div>'
    });

    //$("#msqRole").load("html/msqroles.html");

    $('#provision').click(function (event) {
        $('form').submit();

        //event.preventDefault();        
    });

    $('form').validate({
        rules: {
            txtUserName: {
                required: true,
                validAccoutName: true
            }
        },
        messages: {
            txtUserName: {
                required: "Account name is required."
            }
        },
        submitHandler: function (form) {
            post();
        },
        highlight: function (element, errorClass) {
            $(element).closest('.input-group').addClass('has-error');
            $(element).popover('show');
        },
        unhighlight: function (element, errorClass) {
            $(element).closest('.input-group').removeClass('has-error');
            $(element).popover('hide');
        },
        errorPlacement: function (error, element) {
            var p = element.parent().find('#popover-content-message');
            if (p.length > 0) {
                p.text(error.text());
            }
            //error.appendTo(element.closest('.input-group').parent());                       
        }
    });

    //$('#txtUserName').popover({
    //    html: true,
    //    title: 'Popover Title <a class="close" href="#");">&times;</a>',
    //    content: '<div class="msg">Your Text Here</div>',
    //});
    //$('#txtUserName').click(function (e) {
    //    e.stopPropagation();
    //});

    //$(document).click(function (e) {
    //    if (($('.popover').has(e.target).length == 0) || $(e.target).is('.close')) {
    //        $('#txtUserName').popover('hide');
    //    }
    //});

    //$(function () {
    //    $("[rel='popover']").popover({
    //        placement: 'top', // top, bottom, left or right
    //        title: 'This is my Title',
    //        html: 'true',
    //        content: '<div id="popOverBox">Your Text Here</div>'
    //    });
    //});
});

function post() {
    $('#provision').toggleClass('active');

    var input = {
        Environment: $("#lstEnvironment").val(),
        Application: $("#lstApplication").val(),
        Users: $("#txtUserName").val(),
        PQAccess: $("#chkPQRole").prop('checked'),
        PCN: $("#txtPcn").val(),
        MSQRoles: [
            { "Name": "MQ1", "Value": $("#chkMQ1").val() },
            { "Name": "MQ2", "Value": $("#chkMQ2").val() },
            { "Name": "MQ3", "Value": $("#chkMQ3").val() },
            { "Name": "MQ4", "Value": $("#chkMQ4").val() },
            { "Name": "QSU", "Value": $("#chkQSU").val() },
            { "Name": "MQF", "Value": $("#chkMQF").val() },
            { "Name": "QV", "Value": $("#chkQV").val() }
        ]
    };

    $.ajax({
        contentType: 'application/json',
        data: JSON.stringify(input),
        type: 'POST',
        url: 'api/provision',
        dataType: 'json'
    })
    //$.post('api/provision', '=' + JSON.stringify(input),
    //    function ()
    //    { })
        .done(function (data) {
            try {
                showToast(data.Status, (data.Message1 == null ? data.Message2 : data.Message1));
            } catch (e) {
                console.log(e);
            }
        })
    .fail(function (jqXHR, textStatus, errorThrown) {
        alert("Request failed: " + textStatus);
    })
    .always(function () {
        $('#provision').toggleClass('active');
    });
}

function showToast(type, msg) {
    toastr.clear();

    toastr.options.closeButton = true;
    toastr.options.positionClass = 'toast-top-right';
    toastr.options.extendedTimeOut = 1000;
    toastr.options.timeOut = 5000;
    toastr.options.showDuration = 300;
    toastr.options.hideDuration = 5000;
    toastr.options.showEasing = "swing";
    toastr.options.hideEasing = "linear";
    toastr.options.showMethod = "fadeIn";
    toastr.options.hideMethod = "fadeOut";
    toastr.options.onclick = null;

    toastr[type.toLowerCase()](msg);
}