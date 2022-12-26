<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GRCweb1.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html lang="en">
	<head>
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
		<meta charset="utf-8" />
        <title>GRC board | Login</title>

		<meta name="description" content="User login page" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0" />

		<!-- bootstrap & fontawesome -->
		<link rel="stylesheet" href="assets/css/bootstrap.min.css" />
		<link rel="stylesheet" href="assets/font-awesome/4.2.0/css/font-awesome.min.css" />

		<!-- text fonts -->
		<link rel="stylesheet" href="assets/fonts/fonts.googleapis.com.css" />


    <style type="text/css">
        .colorgraph
        {
            height: 5px;
            border-top: 0;
            background: #c4e17f;
            border-radius: 5px;
            background-image: -webkit-linear-gradient(left, #c4e17f, #c4e17f 12.5%, #f7fdca 12.5%, #f7fdca 25%, #fecf71 25%, #fecf71 37.5%, #f0776c 37.5%, #f0776c 50%, #db9dbe 50%, #db9dbe 62.5%, #c49cde 62.5%, #c49cde 75%, #669ae1 75%, #669ae1 87.5%, #62c2e4 87.5%, #62c2e4);
            background-image: -moz-linear-gradient(left, #c4e17f, #c4e17f 12.5%, #f7fdca 12.5%, #f7fdca 25%, #fecf71 25%, #fecf71 37.5%, #f0776c 37.5%, #f0776c 50%, #db9dbe 50%, #db9dbe 62.5%, #c49cde 62.5%, #c49cde 75%, #669ae1 75%, #669ae1 87.5%, #62c2e4 87.5%, #62c2e4);
            background-image: -o-linear-gradient(left, #c4e17f, #c4e17f 12.5%, #f7fdca 12.5%, #f7fdca 25%, #fecf71 25%, #fecf71 37.5%, #f0776c 37.5%, #f0776c 50%, #db9dbe 50%, #db9dbe 62.5%, #c49cde 62.5%, #c49cde 75%, #669ae1 75%, #669ae1 87.5%, #62c2e4 87.5%, #62c2e4);
            background-image: linear-gradient(to right, #c4e17f, #c4e17f 12.5%, #f7fdca 12.5%, #f7fdca 25%, #fecf71 25%, #fecf71 37.5%, #f0776c 37.5%, #f0776c 50%, #db9dbe 50%, #db9dbe 62.5%, #c49cde 62.5%, #c49cde 75%, #669ae1 75%, #669ae1 87.5%, #62c2e4 87.5%, #62c2e4);
        }
    </style>


</head>


<body style="padding-top: 50px;background:url(Images/bg-login1.png) top repeat;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <%--<ajaxToolkit:ToolkitScriptManager runat="server" ID="ToolkitScriptManager1" />--%>

        <div id="container" class="container">            
            <div class="row" style="margin-top: 20px;">
                <div class="col-xs-12 col-sm-8 col-md-6 col-sm-offset-2 col-md-offset-3" style="background-color: #ffffff; border: 2px #ccc solid; box-shadow: 0 0 30px black;">

                    <fieldset>
                        <script type="text/javascript">
                            // Get the instance of PageRequestManager.
                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            // Add initializeRequest and endRequest
                            prm.add_initializeRequest(prm_InitializeRequest);
                            prm.add_endRequest(prm_EndRequest);

                            // Called when async postback begins
                            function prm_InitializeRequest(sender, args) {
                                // get the divImage and set it to visible
                                var panelProg = $get('divImage');
                                panelProg.style.display = '';
                                // reset label text
                                var lbl = $get('<%= this.lblText.ClientID %>');
                                lbl.innerHTML = '';
                                // Disable button that caused a postback
                                //$get(args._postBackElement.id).disabled = true;
                            }

                            // Called when async postback ends
                            function prm_EndRequest(sender, args) {
                                // get the divImage and hide it again
                                var panelProg = $get('divImage');
                                panelProg.style.display = 'none';
                                // Enable button that caused a postback
                                //$get(sender._postBackSettings.sourceElement.id).disabled = false;
                            }
                        </script>

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <h2>
                                    <asp:Image ID="Image1" ImageUrl="~/img/GRCLogo.png" Width="44px" Height="30px" runat="server" />
                                    GRC board | <font color="green">Sign In</font></h2>
                                <hr class="colorgraph">
                                <div class="form-group">
                                    <%--<input name="username" id="txtLogin" class="form-control input-lg" placeholder="User Name" type="text">--%>
                                    <asp:TextBox ID="txtLogin" runat="server"  name="username" class="form-control input-lg" placeholder="User Name" type="text"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <%--<input name="password" id="password" class="form-control input-lg" placeholder="Password" type="password">--%>
                                    <asp:TextBox name="password" ID="txtPassword" runat="server" class="form-control input-lg" placeholder="Password" type="password" TextMode="Password"></asp:TextBox>
                                </div>
                                <hr class="colorgraph">
                                <div class="row">
                                    <div class="col-xs-6 col-sm-6 col-md-6">
                                        <input id="btnLogin" runat="server" name="LoginButton" class="btn btn-lg btn-success btn-block" value="Sign In" type="submit" onserverclick="btnLogin_ServerClick" accesskey="L">
                                    </div>
                                    <div id="divImage" style="display: none;padding-top:5px;">
                                        <asp:Image ID="img1" runat="server" ImageUrl="~/img/loadingAnim.gif" Width="28px" Height="28px" />
                                    </div>
                                    <div style="padding-top:9px;">
                                    <asp:Label ID="lblText" runat="server" Text="" Style="color: red;"></asp:Label>
                                        </div>                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <br />
                </div>
            </div>
        </div>
        <br />
        <br />
    </form>


</body>



</html>
