﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Samples.Master" AutoEventWireup="true" CodeBehind="pdf-converter-properties.aspx.cs" Inherits="SelectPdf.Samples.pdf_converter_properties" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Select.Pdf Free Html To Pdf Converter for .NET C# - Setting Document Properties with Html to Pdf Converter</title>
    <meta name="description" content="Select.Pdf Document Properties with Html to Pdf Converter Sample for C#. Pdf Library for .NET with full sample code in C# and VB.NET." itemprop="description">
    <meta name="keywords" content="document properties, html to pdf converter, pdf library, sample code, html to pdf, pdf converter" itemprop="keywords">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
        <article class="post type-post status-publish format-standard hentry">
            <header class="entry-header">
                <h1 class="entry-title">Select.Pdf Free Html To Pdf Converter for .NET C# - Setting Document Properties with Html to Pdf Converter</h1>
            </header>
            <!-- .entry-header -->

            <div class="entry-content">
                <p>
                    This sample shows how to convert a web page to pdf using Select.Pdf and set the basic pdf document information (title, subject, keywords) with the values taken from the webpage.
                </p>
                <p>
                    Url:<br />
                    <asp:TextBox ID="TxtUrl" runat="server" Width="90%" Text="http://selectpdf.com"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="BtnCreatePdf" runat="server" Text="Create PDF" OnClick="BtnCreatePdf_Click" CssClass="mybutton" />
                </p>
            </div>
            <!-- .entry-content -->
        </article>
        <!-- #post -->
</asp:Content>
