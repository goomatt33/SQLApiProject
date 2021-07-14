using System;
using System.Net;

namespace apiProject
{
    class HttpManager
    {
        private string[] prefixes;
        private HttpListener listener;
        private HttpListenerContext context;
        private HttpListenerRequest request;
        private bool listening;
        
        public HttpListenerRequest Request{ get{return request;}}
        public bool Listening{get{return listening;}}

        public HttpManager(string[] Prefixes)
        {
            listening = false;
            prefixes = Prefixes;
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            listener = new HttpListener();

            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
        }
        
        public HttpListenerRequest listenForRequest()
        {
            listening = true;
            listener.Start();
            context = listener.GetContext();
            request = context.Request;
            listening = false;
            return request;
        }

        public void sendResponse(string responseString)
        {
            HttpListenerResponse response = context.Response;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer,0,buffer.Length);
            output.Close();
            listener.Stop();
        }
		
		public string getContent()
		{
			System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);

            string s = reader.ReadToEnd();
			return s;
		}
    }


}

