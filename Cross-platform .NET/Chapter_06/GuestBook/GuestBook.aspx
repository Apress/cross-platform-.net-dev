<%@ Page Src="GuestBook.aspx.cs" AutoEventWireup="false" Inherits="Crossplatform.NET.GuestBook.GuestBookPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
 <head>
  <title>Cross-platform.NET - Guestbook</title>
  <link href="_css/guestbook.css" type="text/css" rel="STYLESHEET"></link>
 </head>
 
 <body>
  <h1>Cross-platform.NET Guest Book</h1>
 
  <p>Please enter your criticism and platitudes below.</p>
 
  <form runat="server">
   <asp:customvalidator id="FormValidator" runat="server" OnServerValidate="ValidateForm" EnableClientScript="False"
   ErrorMessage="Please enter a valid Email address and your thoughts in the Comments field."></asp:customvalidator>
   <asp:Label id="SuccessLabel" runat="server" ForeColor="Red">Thank you for your comments.</asp:Label>
   <table>
    <tr>
     <th>Email:</th>
     <td><asp:textbox id="Email" runat="server" size=""></asp:textbox></td>
    </tr>

    <tr valign="top">
     <th>Comment:</th>
     <td><asp:textbox id="Comments" runat="server" TextMode="Multiline" columns="50" rows="10" wrap="true"></asp:textbox></td>
    </tr>

    <tr>
     <td colSpan="2"><asp:button id="Submit" onclick="SaveEntry" runat="server" Text="Submit"></asp:button></td>
    </tr>
    
   </table>
  </form>
    
  <p>
   <asp:DataGrid id="MessageGrid" runat="server" AutoGenerateColumns="False" Width="100%">
    <AlternatingItemStyle BackColor="Silver"/>
    <HeaderStyle Font-Bold="True" BackColor="#3399FF"/>
    <Columns>
     <asp:BoundColumn DataField="Name" ReadOnly="True" HeaderText="Email:"/>
     <asp:BoundColumn DataField="Comments" ReadOnly="True" HeaderText="Comments:"/>
    </Columns>
   </asp:DataGrid>
  </p>

 </body>
</html>
