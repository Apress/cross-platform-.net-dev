//Filename: MessageStore.cs
using System;
using System.Data;
using ByteFX.Data.MySqlClient;

namespace Crossplatform.NET.Chapter6.Data
{   
    public class MessageStore
    {
        private const string DB_CONNECTION = "data source=127.0.0.1;user id=MessageStoreUser;pwd=password;database=MessageStore";

        private readonly MySqlConnection conn;

        public MessageStore()
        {
            conn = new MySqlConnection(DB_CONNECTION);
        }

        //Retrieve the records from the DB
        public DataSet GetMessages()
        {
            const string sqlCmd = "SELECT * FROM Message ORDER BY LoggedDate DESC";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sqlCmd, conn);

            conn.Open();
            DataSet records = new DataSet();
            adapter.Fill(records, "Message");
            conn.Close();

            return records;
        }

        //Insert the message into the DB...
        public void SaveMessage(string name, string comments)
        {
            string sqlCmd = "INSERT INTO Message(Name,Comments,LoggedDate)VALUES('{0}','{1}','{2}')";
            sqlCmd  = String.Format(sqlCmd, 
                                    PrepareString(name),
                                    PrepareString(comments), 
                                    FormatDate(DateTime.Now));
                                    
            MySqlCommand insertCommand = new MySqlCommand(sqlCmd, conn);
            
            conn.Open();
            insertCommand.ExecuteNonQuery();
            conn.Close();
        }
        
        //Double-up single quotes to stop strings from 
        //being inadvertently delimited in SQL queries.
        private string PrepareString(string value)
        {
            return value.Replace("'", "''");
        }

        private string FormatDate(DateTime date){
            return date.Year + "-" + 
                   date.Month + "-" + 
                   date.Day + " " +
                   date.Hour + ":" +
                   date.Minute + ":" +
                   date.Second;
        }
    }
}
