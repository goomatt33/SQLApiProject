using System;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace apiProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] prefixes = {"http://localhost:8080/"};
            HttpManager httpManager = new HttpManager(prefixes);

            bool running = true;
            while(running)
            {

                HttpListenerRequest request = httpManager.listenForRequest();
                Query sqlquery = new Query(request.HttpMethod, httpManager.getContent());
    
                try
    			{
                    /* Modify to apropriate values */
    				SqlManager sqlManager = new SqlManager(
    					"DESKTOP-CR0PHL1\\SQLEXPRESS",
    					"sa",
    					"serverAdmin",
    					"patients");
    				if(!sqlManager.connectToServer())
    				{
    					Console.WriteLine("\n");
    					Console.WriteLine(sqlManager.Exception);
    				}
    				
    				Console.WriteLine("\nQuery Data example:");
    				Console.WriteLine("=============================================\n");
    					
    				String result = sqlManager.makeQuery(sqlquery.sqlQuery);
    				Console.WriteLine(result);
    
                 httpManager.sendResponse(result);
    				
    				sqlManager.disconnectFromServer();
    			
    			}
    			catch (SqlException e)
    			{
    				Console.WriteLine(e.ToString());
    			}
            }			
			Console.WriteLine("\nDone. Press enter.");
			Console.ReadLine();
        }
    }
}
