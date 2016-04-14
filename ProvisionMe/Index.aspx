<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ProvisionMe.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <title>ECIT Access Provisioning Tool</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <%-- <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Content/toastr.min.css" rel="stylesheet" />--%>

    <%--<link href="//cdnjs.cloudflare.com/ajax/libs/font-awesome/4.2.0/css/font-awesome.css" rel="stylesheet" />--%>
    <%--<link href="css/font-awesome.min.css" rel="stylesheet" />--%>

    <%--<link href="css/app.css" rel="stylesheet" />--%>

    <script type="text/jscript" src="Scripts/jquery-2.1.1.min.js"></script>
    <script type="text/jscript" src="Scripts/jquery.validate.min.js"></script>
    <script type="text/jscript" src="Scripts/bootstrap.min.js"></script>
    <%-- <script type="text/jscript" src="Scripts/toastr.min.js"></script>
    <script type="text/jscript" src="Scripts/MetroJs.min.js"></script>--%>

    <%--<script type="text/jscript" src="js/app.js"></script>--%>
    <style>
        .error {
            color: red;
            font-size: 0.8em;
        }
    </style>
</head>
<body>
    <%--<div class="col-sm-2">
    </div>--%>
    <div class="col-sm-6">
        <form class="form-horizontal">
            <fieldset>
                <legend>Information</legend>
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="environment">Environment</label>
                    <div class="col-sm-9">
                        <select id="environment" name="environment" class="form-control">
                            <option value="1">1</option>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="application">Application</label>
                    <div class="col-sm-9">
                        <select id="application" name="application" class="form-control">
                            <option value="1">1</option>
                        </select>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="username">Account</label>
                    <div class="col-sm-6">
                        <textarea id="username" rows="5" cols="40" name="username" class="form-control"></textarea>
                        <%--<span class="help-block">Users alias</span>--%>
                    </div>
                    <div class="col-sm-3"></div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <div class="checkbox">
                            <label for="pqaccess">
                                <input type="checkbox" id="pqaccess" name="pqaccess" />
                                PQ Access</label>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label" for="pcn">Partner PCN</label>
                    <div class="col-sm-9">
                        <input type="text" id="pcn" name="pcn" class="form-control" />
                    </div>
                </div>
            </fieldset>
            <div class="form-group">
                <div class="col-sm-9">
                    <input type="submit" id="submit" class="btn btn-primary" name="submit" value="Submit" />
                </div>
            </div>
        </form>
    </div>
    <%--<div class="col-sm-2">
    </div>--%>

    <script>
        $('form').validate({
            rules: {
                username: {
                    required: true
                }
            },
            messages: {
                username: {
                    required: "test"
                }
            },
            submitHandler: function (form) {
                alert($('form').serialize());
            },
            highlight: function (element, errorClass) {
                $(element).closest('.form-group').addClass('has-error');
            },
            unhighlight: function (element, errorClass) {
                $(element).closest('.form-group').removeClass('has-error');
            },
            errorPlacement: function (error, element) {
                error.appendTo(element.parent().next());
            }
        });

        //$('.input submit').click(function (src) {
        //    src.preventDefault();
        //});
    </script>
</body>
</html>
