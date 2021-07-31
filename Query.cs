using System;
using System.Data.SqlClient;
using System.Text;

namespace apiProject
{
    class Query
    {
        private string requestType;
        private string rawData;
        public string sqlQuery;

        public Query(string RequestType, string RequestData)
        {
            requestType = RequestType;
            rawData = RequestData;
            sqlQuery = "";

            string[] splitString = rawData.Split(',');

            if(requestType == "GET")
            {
                sqlQuery += "SELECT * FROM patientData WHERE(";
                sqlQuery += splitString[0];
                for(int i = 1; i < splitString.Length; i++)
                {
                    sqlQuery += ", ";
                    sqlQuery += splitString[i];
                }
                sqlQuery += ");";
            }
            if(requestType == "POST")
            {           
                sqlQuery = "INSERT INTO patientData VALUES(\'";
                sqlQuery += splitString[0];
                for(int i = 1; i < splitString.Length; i++)
                {
                    sqlQuery += "\', \'";
                    sqlQuery += splitString[i];
                }
                sqlQuery += "\');";
            }
        }
    }
}
