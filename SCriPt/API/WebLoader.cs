using System.Net;
using System.Net.Http;
using MoonSharp.Interpreter;
using PluginAPI.Core;

namespace SCriPt.API;

public class WebLoader
{
    public static string GetFromUrl(string url)
    {
        string response = HttpQuery.Get(url, out bool success, out HttpStatusCode code);
        if (!success)
        {
            Log.Error("Failed to get response from pastebin: " + code);
            return null;
        }

        return response;
    }
    
    public static string GetFromPastebin(string id)
    {
        return GetFromUrl("https://pastebin.com/raw/" + id);
    }
}