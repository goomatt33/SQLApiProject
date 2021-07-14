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
            if(requestType == "GET")
            {
                sqlQuery += "SELECT ";
                string[] splitString = rawData.Split(',');
                sqlQuery += "\"";
                sqlQuery += splitString[0];
                for(int i = 1; i < splitString.Length; i++)
                {
                    sqlQuery += "\", \"";
                    sqlQuery += splitString[i];
                }
                sqlQuery += "\" FROM patientData;";
            }
            if(requestType == "POST")
            {
                sqlQuery = "INSERT INTO patientData VALUES(\"";
                string[] splitString = rawData.Split(',');
                sqlQuery += splitString[0];
                for(int i = 1; i < splitString.Length; i++)
                {
                    sqlQuery += "\", \"";
                    sqlQuery += splitString[i];
                }
                sqlQuery += "\");";
            }
        }
    }
}
