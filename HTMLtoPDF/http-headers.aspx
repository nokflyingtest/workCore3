﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Samples.Master" AutoEventWireup="true" CodeBehind="http-headers.aspx.cs" Inherits="SelectPdf.Samples.http_headers" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Select.Pdf Free Html To Pdf Converter for .NET C# - Sending HTTP Headers with Html to Pdf Converter</title>
    <meta name="description" content="Select.Pdf Sending HTTP Headers with Html to Pdf Converter Sample for C#. Pdf Library for .NET with full sample code in C# and VB.NET." itemprop="description">
    <meta name="keywords" content="http headers, pdf library, sample code, html to pdf, pdf converter" itemprop="keywords">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
        <article class="post type-post status-publish format-standard hentry">
            <header class="entry-header">
                <h1 class="entry-title">Select.Pdf Free Html To Pdf Converter for .NET C# - Sending HTTP Headers with Html to Pdf Converter</h1>
            </header>
            <!-- .entry-header -->

            <div class="entry-content">
                <p>
                    This sample shows how to send HTTP headers to the page that will be converted using the html to pdf converter from Select.Pdf Pdf Library for .NET.
                    <br />
                    <br />
                    Below, there is a link to a test page that will display the HTTP headers sent to it. Converting this page will display the headers sent by the html to pdf converter.
                    <br />
                    <br />
                    <asp:HyperLink ID="LnkTest" runat="server" Text="Test page" Target="_blank"></asp:HyperLink>
                </p>
                <p>
                    Url:<br />
                    <asp:TextBox ID="TxtUrl" runat="server" Width="90%" Text=""></asp:TextBox>
                    <br />
                    <br />
                    HTTP Headers:<br />
                    <br />
                    Name: <asp:TextBox ID="TxtName1" runat="server" Width="40%" Text="Name1"></asp:TextBox>
                    Value: <asp:TextBox ID="TxtValue1" runat="server" Width="40%" Text="Value1"></asp:TextBox>
                    <br />
                    Name: <asp:TextBox ID="TxtName2" runat="server" Width="40%" Text="Name2"></asp:TextBox>
                    Value: <asp:TextBox ID="TxtValue2" runat="server" Width="40%" Text="Value2"></asp:TextBox>
                    <br />
                    Name: <asp:TextBox ID="TxtName3" runat="server" Width="40%" Text="Name3"></asp:TextBox>
                    Value: <asp:TextBox ID="TxtValue3" runat="server" Width="40%" Text="Value3"></asp:TextBox>
                    <br />
                    Name: <asp:TextBox ID="TxtName4" runat="server" Width="40%" Text="Name4"></asp:TextBox>
                    Value: <asp:TextBox ID="TxtValue4" runat="server" Width="40%" Text="Value4"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="BtnCreatePdf" runat="server" Text="Create PDF" OnClick="BtnCreatePdf_Click" CssClass="mybutton" />
                </p>
            </div>
            <!-- .entry-content -->
        </article>
        <!-- #post -->
</asp:Content>
