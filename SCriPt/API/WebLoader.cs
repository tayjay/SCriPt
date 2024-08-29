using System;
using System.Net;
using System.Net.Http;
using MoonSharp.Interpreter;
using PluginAPI.Core;

namespace SCriPt.API;

public class WebLoader
{
    /*public static string GetFromUrl(string url) Error, was returning 403 on all requests
    {
        string response = HttpQuery.Get(url, out bool success, out HttpStatusCode code);
        if (!success)
        {
            Log.Error("Failed to get response from pastebin: " + code);
            return null;
        }

        return response;
    }*/

    public static string GetFromUrl(string url)
    {
        string urlData = String.Empty;
        WebClient WebClient = new WebClient();

        // Add headers to impersonate a web browser. Some web sites 
        // will not respond correctly without these headers
        WebClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-GB; rv:1.9.2.12) Gecko/20101026 Firefox/3.6.12");
        WebClient.Headers.Add("Accept", "*/*");
        WebClient.Headers.Add("Accept-Language", "en-gb,en;q=0.5");
        WebClient.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");

        urlData = WebClient.DownloadString(url);
        return urlData;
    }
    
    public static string GetFromPastebin(string id)
    {
        Exiled.API.Features.Log.Debug("https://pastebin.com/raw/" + id);
        return GetFromUrl("https://pastebin.com/raw/" + id);
    }
}