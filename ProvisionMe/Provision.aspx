<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Provision.aspx.cs" Inherits="ProvisionMe.Provision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />

    <title>Provision</title>

    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="Content/toastr.min.css" rel="stylesheet" />

    <link href="css/font-awesome.min.css" rel="stylesheet" />

    <link href="css/site.css" rel="stylesheet" />

    <script type="text/jscript" src="Scripts/jquery-2.1.1.min.js"></script>
    <script type="text/jscript" src="Scripts/jquery-ui.min-1.11.1.js"></script>
    <script type="text/jscript" src="Scripts/jquery.validate.min.js"></script>
    <script type="text/jscript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/jscript" src="Scripts/toastr.min.js"></script>
    <script type="text/jscript" src="Scripts/MetroJs.min.js"></script>

    <script type="text/jscript" src="js/app.js"></script>
</head>
<body>
    <div class="container">
        <br />
        <div class="panel-primary">
            <div class="panel-heading col-md-7 text-left">
                ECIT (Bedrock) Application Self Provisioning Tool         
            </div>
            <div class="panel-heading col-md-5 text-right">
                <div>
                    <%=User.Identity.Name %>
                    <%--<% if (User.Identity.IsAuthenticated)
                       { %>
                    <a href="home/signout" class="btn-default">Signout <i class="fa fa-sign-out" style="color: orangered;"></i>
                    </a>
                    <% } %>--%>
                </div>
            </div>
        </div>
        <form class="panel-body" id="frmProvision" runat="server" style="padding: 0; width: auto;">
            <table class="table" style="width: 100%">
                <thead>
                    <tr>
                        <td class="col-md-4"></td>
                        <td class="col-md-2"></td>
                        <td class="col-md-3"></td>
                        <td class="col-md-3"></td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="text-primary">
                            <asp:Label ID="Label4" runat="server">Environment:</asp:Label></td>
                        <td colspan="2">
                            <select class="form-control" id="lstEnvironment" runat="server">
                            </select>
                        </td>
                        <td></td>
                    </tr>
                    <tr class="text-primary">
                        <td align="left">Account Name</td>

                        <td align="left">Application</td>
                        <td align="left">Role</td>
                        <td align="left">CP PCN</td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div class="input-group">
                                <span class="input-group-addon" style="color:green">
                                    <i class="fa fa-users"></i>
                                </span>
                                <asp:TextBox class="form-control" ID="txtUserName" name="txtUserName" placeholder="domain\alias"
                                    runat="server"></asp:TextBox>
                                <span id="helpUserName" class="input-group-addon" data-toggle="popover" style="color:blue" title="">
                                    <i class="fa fa-question-circle"></i>
                                </span>
                            </div>
                        </td>

                        <td align="left">
                            <select class="form-control" id="lstApplication" runat="server">
                            </select></td>
                        <td align="left">
                            <span class="form-control" style="white-space: nowrap">
                                <input type="checkbox" id="chkPQRole" />
                                Partner Quote</span>

                            <%-- <select class="form-control" id="lstMSQRoles" runat="server">
                            </select>--%>
                        </td>
                        <td align="left">
                            <div class="input-group">
                                <asp:TextBox class="form-control" ID="txtPcn" placeholder="Public Customer Number" runat="server" MaxLength="15" Enabled="True"></asp:TextBox>
                                <span class="input-group-addon" id="helpPCN" data-toggle="popover" title="" style="color:blue">                                    
                                    <i class="fa fa-question-circle"></i>
                                </span>
                            </div>
                            <%--<div class="input-group">
                        <input type="text" class="form-control" />
                        <span class="input-group-addon">
                            <i class="fa fa-question-circle"></i>
                        </span>
                    </div>--%>                    
                        </td>
                    </tr>
                </tbody>
            </table>            
            <div id="divMSQuoteRoleContainer" runat="server" style="width: 100%;">
                <table class="table" style="width: 100%;">
                    <thead>
                        <tr>
                            <td class="col-md-3 text-primary">MS Quote Roles</td>
                            <td class="col-md-3"></td>
                            <td class="col-md-6"></td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>MQ1
                            </td>
                            <td>
                                <input id="chkMQ1" type="checkbox" runat="server" value="D" onchange="javascript:return onrolechange(this)" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>MQ2
                            </td>
                            <td>
                                <input id="chkMQ2" type="checkbox" runat="server" value="D" onchange="javascript:return onrolechange(this)" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>MQ3
                            </td>
                            <td>
                                <input id="chkMQ3" type="checkbox" runat="server" value="D" onchange="javascript:return onrolechange(this)" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>MQ4
                            </td>
                            <td>
                                <input id="chkMQ4" type="checkbox" runat="server" value="D" onchange="javascript:return onrolechange(this)" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>QSU
                            </td>
                            <td>
                                <input id="chkQSU" type="checkbox" runat="server" value="D" onchange="javascript:return onrolechange(this)" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>MQF
                            </td>
                            <td>
                                <input id="chkMQF" type="checkbox" runat="server" value="D" onchange="javascript:return onrolechange(this)" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>QV
                            </td>
                            <td>
                                <input id="chkQV" type="checkbox" runat="server" value="D" onchange="javascript:return onrolechange(this)" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td><strong>Empowerment Level</strong>
                            </td>
                            <td><strong>BD</strong>
                            </td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <asp:HiddenField ID="selectedApplicationIndex" runat="server" Value="0" />            
        </form>
        <div class="panel text-right" >
            <a id="provision" class="btn btn-sm btn-primary has-spinner">
                <span class="spinner"><i class="fa fa-spinner fa-spin" style="color:darkorange"></i></span>
                Provision
            </a>           
        </div>
    </div>
</body>
</html>
