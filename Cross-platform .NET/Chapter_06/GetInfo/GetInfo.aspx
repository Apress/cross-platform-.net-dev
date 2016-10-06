<%@ Page language="c#"%>
<!-- Filename: GetInfo.aspx -->

<html>
 <head>
  <title>Cross-platform .NET: GetInfo </title>
  <link href="_css/getinfo.css" type="text/css" rel="STYLESHEET"></link>
 </head>
 <body>
  <h1>ASP.NET does GetInfo</h1>
   <table border="1" width="300">
    <tr><th>Operating system: </th><td><%= System.Environment.OSVersion.Platform.ToString() %></td></tr>
    <tr><th>OS Version: </th><td><%= System.Environment.OSVersion.Version.ToString() %></td></tr>
    <tr><th>Todays date is: </th><td><%= System.DateTime.Now.ToString() %></td></tr>
   </table>
 </body>
</html>