using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Crossplatform.NET.Chapter6.Data;

namespace Crossplatform.NET.GuestBook
{
    public class GuestBookPage : System.Web.UI.Page
    {
        protected Button Submit;
        protected CustomValidator FormValidator;
        protected TextBox Email;
        protected TextBox Comments;
        protected Label SuccessLabel;
        protected DataGrid MessageGrid;
        
        private MessageStore dataStore;

	public GuestBookPage(){
		this.PreRender += new System.EventHandler(this.OnPreRender);	
		dataStore = new MessageStore();
	}

        public void SaveEntry(object sender, EventArgs e)
        {    
            if(Page.IsValid)
            {
                dataStore.SaveMessage(Email.Text, Comments.Text);
            
                // Clear the field values
                Email.Text = String.Empty;
                Comments.Text = String.Empty;
            }
            
            SuccessLabel.Visible = Page.IsValid;
        }
    
        public void ValidateForm(Object source, ServerValidateEventArgs args)
        {                    
            args.IsValid= (IsEmail(Email.Text) & Comments.Text != String.Empty);
        }

        private bool IsEmail(string value)
        {
            string emailExpression = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]" + 
                                     @"{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+)" + 
                                     @")([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return (new Regex(emailExpression).IsMatch(value));
        }


        private void OnPreRender(object sender, System.EventArgs e)
        {
            SuccessLabel.Visible = (SuccessLabel.Visible & Page.IsPostBack);
            //Retrieve DataSet and bind to datagrid control...
            this.MessageGrid.DataSource = dataStore.GetMessages();
            this.MessageGrid.DataBind();
        }
     
    }
}
