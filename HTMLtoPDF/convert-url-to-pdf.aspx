﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Samples.Master" AutoEventWireup="true" CodeBehind="convert-url-to-pdf.aspx.cs" Inherits="SelectPdf.Samples.convert_url_to_pdf" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Select.Pdf Free Html To Pdf Converter for .NET C# - Convert from Url to Pdf</title>
    <meta name="description" content="Select.Pdf Convert from Url to Pdf Sample for C#. Pdf Library for .NET with full sample code in C# and VB.NET." itemprop="description">
    <meta name="keywords" content="convert from url to pdf, pdf library, sample code, html to pdf, pdf converter" itemprop="keywords">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
        <article class="post type-post status-publish format-standard hentry">
            <header class="entry-header">
                <h1 class="entry-title">Select.Pdf Free Html To Pdf Converter for .NET C# - Convert from Url to Pdf</h1>
            </header>
            <!-- .entry-header -->

            <div class="entry-content">
                <p>
                    This sample shows how to use Select.Pdf html to pdf converter to convert an url to pdf, also setting a few properties.
                </p>
                <p>
                    Url:<br />
                    <asp:TextBox ID="TxtUrl" runat="server" Width="90%" Text="http://selectpdf.com"></asp:TextBox>
                </p>
                <div class="col2">
                    Pdf Page Size:<br />
                    <asp:DropDownList ID="DdlPageSize" runat="server">
                        <asp:ListItem Text="A1" Value="A1"></asp:ListItem>
                        <asp:ListItem Text="A2" Value="A2"></asp:ListItem>
                        <asp:ListItem Text="A3" Value="A3"></asp:ListItem>
                        <asp:ListItem Text="A4" Value="A4" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="A5" Value="A5"></asp:ListItem>
                        <asp:ListItem Text="Letter" Value="Letter"></asp:ListItem>
                        <asp:ListItem Text="HalfLetter" Value="HalfLetter"></asp:ListItem>
                        <asp:ListItem Text="Ledger" Value="Ledger"></asp:ListItem>
                        <asp:ListItem Text="Legal" Value="Legal"></asp:ListItem>
                    </asp:DropDownList><br />
                    <br />
                    Pdf Page Orientation:<br />
                    <asp:DropDownList ID="DdlPageOrientation" runat="server">
                        <asp:ListItem Text="Portrait" Value="Portrait" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Landscape" Value="Landscape"></asp:ListItem>
                    </asp:DropDownList><br />
                    <br />
                </div>
                <div class="col2">
                    Web Page Width:<br />
                    <asp:TextBox ID="TxtWidth" runat="server" Width="50px" Text="1024"></asp:TextBox> px<br />
                    <br />
                    Web Page Height:<br />
                    <asp:TextBox ID="TxtHeight" runat="server" Width="50px" Text=""></asp:TextBox> px<br />
                    (leave empty to auto detect)<br />
                    <br />

                </div>
                <div class="col-clear"></div>
                <p>
                    <asp:Button ID="BtnCreatePdf" runat="server" Text="Create PDF" OnClick="BtnCreatePdf_Click" CssClass="mybutton" />
                </p>
            </div>
            <!-- .entry-content -->
        </article>
        <!-- #post -->
</asp:Content>
