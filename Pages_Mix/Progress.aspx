<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Progress.aspx.cs" Inherits="Progress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .LoadingWrapper {
    position: fixed;
    left: 0px;
    top: 0px;
    width: 100%;
    height: 100%;
    z-index: 9999;
    background-color: rgba(255, 255, 255, 0.54);
}

.Loader {
    position: absolute;
    vertical-align: middle;
    width: 167px;
    height: 167px;
    left: 50%;
    top: 50%;
    margin-left: -84px;
    margin-top: -84px;
}


    .Loader > div {
        position: absolute;
    }

.OuterCircle {
    background-image: url(../../images/ExtL.png);
    width: 166px;
    height: 167px;
    display: block;
    top: 0;
    left: 0;
    animation: spin 3s linear infinite;
}

.MiddleCircle {
    background-image: url(../../images/MiddleL.png);
    width: 120px;
    height: 120px;
    display: block;
    top: 22px;
    left: 23px;
    animation: spin1 2s linear infinite;
}

.InnerCircle {
    background-image: url(../../images/CenterL.png);
    width: 73px;
    height: 73px;
    display: block;
    top: 48px;
    left: 47px;
    animation: spin2 2s linear infinite;
}

.MinHand {
    background-image: url(../../images/MinL.png);
    width: 9px;
    height: 92px;
    display: block;
    top: 43px;
    left: 79px;
    animation: spin2 2s linear infinite;
}

.HourHand {
    background-image: url(../../images/HourL.png);
    width: 54px;
    height: 8px;
    display: block;
    top: 81px;
    left: 58px;
    animation: spin2 2s linear infinite;
}
.loadingText
{
    top:180px;
    width: 167px;
}
.loadingText>span
{
    display:block;
    text-align:center;
    font-size:20px;
        color: rgb(67, 67, 67);
        text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.4);
}
@keyframes spin {
    0% {
        transform: rotate(0deg);
    }

    100% {
        transform: rotate(360deg);
    }
}

@keyframes spin1 {
    0% {
        transform: rotate(360deg);
    }

    100% {
        transform: rotate(0deg);
    }
}

@keyframes spin2 {
    0% {
        -webkit-transform: rotateY(0);
        -ms-transform: rotateX(0);
        -moz-transform: rotateY(0);
        transform: rotateY(0);
    }

    100% {
        -webkit-transform: rotateY(180deg);
        -ms-transform: rotateX(360deg);
        -moz-transform: rotateY(360deg);
        transform: rotateY(360deg);
    }
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="LoadingWrapper">
        <div class="Loader">
            <div class="OuterCircle"></div>
            <div class="MiddleCircle"></div>
            <div class="InnerCircle"></div>
            <div class="MinHand"></div>
            <div class="HourHand"></div>
            <div class="loadingText">
             <asp:Label ID="lblUpdateProgress" runat="server" Text="...الرجاء الإنتظار" ></asp:Label></div>
        </div>

    </div>
       
    </form>
</body>
</html>
