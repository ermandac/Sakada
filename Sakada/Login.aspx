<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Sakada.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sakada Record Monitoring System</title>

    <%--<link rel="stylesheet" href="css/bootstrap.min.css" />--%>
    <link rel="stylesheet" href="css/w3.css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <style>
        html, h1, h2, h3, h4, h5 {
            font-family: 'Open Sans', sans-serif;
            
        }
        /* latin-ext */
        @font-face {
            font-family: 'Segoe UI';
            font-style: normal;
            font-weight: 400;
            src: local('Segoe UI'), local('Segoe UI-SemiBold'), url(https://fonts.gstatic.com/s/raleway/v11/yQiAaD56cjx1AooMTSghGfY6323mHUZFJMgTvxaG2iE.woff2) format('woff2');
            unicode-range: U+0100-024F, U+1E00-1EFF, U+20A0-20AB, U+20AD-20CF, U+2C60-2C7F, U+A720-A7FF;
        }
        /* latin */
        @font-face {
            font-family: 'Segoe UI';
            font-style: normal;
            font-weight: 400;
            src: local('Segoe UI'), local('Segoe UI-SemiBold'), url(https://fonts.gstatic.com/s/raleway/v11/0dTEPzkLWceF7z0koJaX1A.woff2) format('woff2');
            unicode-range: U+0000-00FF, U+0131, U+0152-0153, U+02C6, U+02DA, U+02DC, U+2000-206F, U+2074, U+20AC, U+2212, U+2215;
        }

        .CustomColor{
            background-color:#002050;
        }
        body{
            font-family: 'Open Sans', sans-serif;
        }
        .bg-image{
           overflow: auto;
           position: relative;
        }
        .bg-image::before{
           content: "";
           display: block;
           position: fixed;
           left: 0;
           right: 0;
           z-index: -1;
           background-image: url('/img/bg.jpeg');
           background-repeat: no-repeat;
           background-size: cover;
           width: 100%;
           height: 100%;
           background-position: center;
           filter:blur(3px);
        }
    </style>

</head>
<body>
    <div class="bg-image"></div>
    <form id="form1" class="bg-image" runat="server">
    <div class="w3-row bg-image-inside" style="margin-top:100px;">
        <div class="w3-col s4">
            &nbsp;
            <asp:Label ID="lblUserName" runat="server" Text="0" style="display:none;" />
            <asp:Label ID="lblAccessLevel" runat="server" Text="0" style="display:none;" />
        </div>
        <div class="w3-col s4 w3-card w3-white text-center w3-round-small w3-animate-zoom">
            <div class="w3-col s12 w3-border w3-text-white w3-center" style="height:50px;background-color:#002050;">
                <h4>Sakada Record Monitoring System</h4>
            </div>
            <div class="w3-animate-zoom">
                <asp:Panel ID="pnlWarningMessage" runat="server" CssClass="w3-red w3-center" Visible="false">
                    <asp:Label ID="lblWarningMessage" runat="server">Username or Password is incorrect.</asp:Label>
                </asp:Panel>
                
                <div class="w3-center" style="font-size:small;"><label>&nbsp;</label></div>
                <hr style="margin-top:0px;"/>
                <div class="w3-padding-left w3-padding-right w3-animate-opacity">
                    <i class="fa fa-user w3-text-gray"></i>
                    <label>User Name</label>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="w3-input w3-border w3-round-small w3-center"></asp:TextBox>
                </div>

                <div class="w3-padding-left w3-padding-right w3-animate-opacity">
                    <i class="fa fa-lock w3-text-gray"></i>
                    <label>Password</label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="w3-input w3-border w3-round-small w3-center"></asp:TextBox>
                </div>

                <div class="w3-padding-left w3-padding-right w3-padding-bottom w3-margin-top">
                    <asp:Button ID="btnLogin" runat="server" CssClass="w3-btn w3-light-gray w3-border" Text="Login" style="width:100%;"/>
                </div>

                <hr style="margin-top:0px;margin-bottom:0px;" />

                <div style="margin-top:0px;" class="w3-center">
                    <label class="w3-text-gray" style="font-size:11px;margin-top:0px;margin-bottom:0px;">©Copyright 2021</label><br />
                </div>

            </div>
        </div>
        <div class="w3-col s4">
            &nbsp;
        </div>
    </div>
    </form>
</body>
</html>
