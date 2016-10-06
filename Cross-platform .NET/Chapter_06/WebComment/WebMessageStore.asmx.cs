//Filename: WebMessageStore.asmx.cs
using System;
using System.Data;
using System.Web;
using System.Web.Services;
using Crossplatform.NET.Chapter6.Data;

namespace Crossplatform.NET.Chapter6
{   
    [WebService(Namespace="http://www.cross-platform.net/GuestBook/", Description="Provides access to the Guest Book entries")]
    public class WebMessageStore : WebService
    {
    [WebMethod(Description="Retrieves all Guest Book entries")]
        public DataSet GetMessages()
        {
            return new MessageStore().GetMessages();
        }
    }
}
