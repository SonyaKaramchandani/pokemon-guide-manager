using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Net;

public partial class InvokeServiceClr
{
    //[SqlProcedure]
    public static void InvokeService(SqlString apiUrl, out SqlString json)
    {
        string commandResult = string.Empty;
        try
        {
            HttpWebRequest request = WebRequest.Create(new Uri(apiUrl.ToString())) as HttpWebRequest;
            request.Method = "GET";
            request.Timeout = 600000;
            CredentialCache credentialCache = new CredentialCache
                {
                    { new Uri(apiUrl.ToString()), "Basic", new NetworkCredential("product", "data") }
                };
            request.Credentials = credentialCache;

            using (var response = request.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                using (var reader = new StreamReader(responseStream))
                {
                    // get the response as text
                    commandResult = reader.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            commandResult = ex.Message;
        }
        json = commandResult;
    }
    public static void InvokeSpatialService(SqlString apiUrl, out SqlString json)
    {
        string commandResult = string.Empty;
        try
        {
            HttpWebRequest request = WebRequest.Create(new Uri(apiUrl.ToString())) as HttpWebRequest;
            request.Method = "GET";
            request.Timeout = 600000;
            CredentialCache credentialCache = new CredentialCache
                {
                    { new Uri(apiUrl.ToString()), "Basic", new NetworkCredential("dbuser", "database") }
                };
            request.Credentials = credentialCache;

            using (var response = request.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                using (var reader = new StreamReader(responseStream))
                {
                    // get the response as text
                    commandResult = reader.ReadToEnd();
                }
            }
        }
        catch (Exception ex)
        {
            commandResult = ex.Message;
        }
        json = commandResult;
    }
}

