//WebMessageStoreProxy.cs
using System;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

[System.Web.Services.WebServiceBinding(Name="WebMessageStoreSoap")]
public class WebMessageStoreProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
{   
    public WebMessageStoreProxy()
    {
        this.Url = "http://127.0.0.1/WebMessageStore.asmx";
    }
    
    [System.Web.Services.Protocols.SoapDocumentMethod("http://www.cross-platform.net/GuestBook/GetMessages", 
         RequestNamespace="http://www.cross-platform.net/GuestBook/", 
         ResponseNamespace="http://www.cross-platform.net/GuestBook/")]
    public System.Data.DataSet GetMessages()
    {
        object[] results = this.Invoke("GetMessages", new object[0]);
        return ((System.Data.DataSet)(results[0]));
    }
}
