//Filename: LastRunDb.cs
using System;
using System.Data;
using ByteFX.Data.MySqlClient;


namespace Crossplatform.NET.Chapter6
{
    
    class ApplicationStore
    {

        private const string DB_CONNECTION = "data source=192.168.0.5;user id=Chapter6;pwd=LastRun;database=ApplicationStore";

        private MySqlConnection connection;
        private MySqlDataAdapter adapter;

        private string moniker;
        private DataSet dataKeyValues;

        public ApplicationStore(string applicationName)
        {
            this.connection = new MySqlConnection(DB_CONNECTION);
            
            string selectCmd = String.Format("SELECT * FROM DataKeyValue WHERE ApplicationName='{0}'", applicationName);
            
            //Create a data adapter for DB communication
            adapter = new MySqlDataAdapter(selectCmd, connection);
            MySqlCommandBuilder cb = new MySqlCommandBuilder(adapter);
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            //Store values in member fields
            moniker = applicationName;
            dataKeyValues = RetrieveData();
        }

        //Retrieve the actual records from the DB
        private DataSet RetrieveData()
        {
            connection.Open();
            DataSet records = new DataSet();
            adapter.Fill(records);

            connection.Close();
            return records;
        }

        //Update the DB with any changes...
        public void Update()
        {
            this.connection.Open();
            this.adapter.Update(dataKeyValues);
            this.connection.Close();
        }
        
        //Provide indexer access to the data
        public string this[string key]
        {
            get
            {
                DataRow record = GetDataRowByKey(key);

                if(record != null)
                {
                    return (record["DataValue"].ToString());
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                DataRow record = GetDataRowByKey(key);
                
                if(record != null)
                {
                    //Update the exisitng row
                    record["DataValue"] = value;
                }
                else
                {
                    //Add a row!!!
                    object[]newRow = new object[3]{moniker, key, value};
                    dataKeyValues.Tables["Table"].Rows.Add(newRow);
                }
            }
        }



        //Retrieve a DataRow with the given key
        private DataRow GetDataRowByKey(string key)
        {
            object[]searchTerms = new object[2]{moniker, key};        
            return (dataKeyValues.Tables["Table"].Rows.Find(searchTerms));
        }    
    }
    
    class LastRunDb
    {
        private const string errorMsg = "error '{0}' was raised from '{1}'";

        static void Main(string[] args)
        {
            try
            {
                DisplayUsageDetails();
            } 
            catch (Exception e)
            {
                Console.Error.WriteLine(errorMsg, e.Message, e.Source);
            }                  
        }

        private static void DisplayUsageDetails()
        {
            ApplicationStore dataStore = new ApplicationStore("TEST");
            
            //Display the output 
            Console.WriteLine("Last run by user '{0}@{1}' at '{2}'",
                              dataStore["userName"],
                              dataStore["hostName"],
                              dataStore["lastRun"]);
            //Update the data
            dataStore["userName"] = Environment.UserName;
            dataStore["hostName"] = Environment.MachineName;
            dataStore["lastRun"] = DateTime.Now.ToString();
            dataStore.Update();
        }
    }
}
