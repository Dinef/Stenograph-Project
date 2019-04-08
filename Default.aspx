<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Testing._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Stenography</h1>
        
    </div>

    <div class="row">
        <div class="col-md-4">
                <asp:FileUpload ID="FileUpload1" runat="server" />
                 <br />
                <asp:Button ID="btnUpload" Text="Upload" CssClass="btn btn-primary"  runat="server" OnClick="UploadFile" />
                <hr />
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                <asp:Image ID="Image1" runat="server" Height = "300" Width = "300" />
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtMsg" runat="server" CssClass="form-control" 
    Width="250px" TabIndex="1"></asp:TextBox>
            <br />
            
            <asp:Button ID="btnEncode" Text="Encode" CssClass="btn btn-success"  runat="server" OnClick="btnEncode_Click" />

            <br />

                        <asp:Button ID="btnDecode" Text="Decode" CssClass="btn btn-danger"  runat="server" />

            <asp:TextBox ID="txtDecodedMsg" runat="server"></asp:TextBox>

         </div>
     
    </div>

</asp:Content>
