using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;

namespace RokuDeviceLib;

public class Client
{
    private static readonly HttpClient httpClient = new();
    public const string DISCOVER_MESSAGE = "M-SEARCH * HTTP/1.1\r\n" +
                        "HOST: {0}:{1}\r\n" +
                        "ST:roku:ecp\r\n" +
                        "MAN:\"ssdp:discover\"\r\n";

    public const string LOCATION_PATTERN = @"location:\s?(?<address>http://[^/]+/)";

    //Get a list of Available Roku Devices
    public static async Task<RokuDevice[]> Discover(string ip = "239.255.255.250", int port = 1900, int waitSeconds = 15)
    {
        var token = new CancellationTokenSource();
        
        var ucs = new UdpClient();
    
        var mcEndpoint = new IPEndPoint(IPAddress.Parse(ip),port);

        var data = string.Format(DISCOVER_MESSAGE,ip,port);
        
        var discoverBytes = Encoding.UTF8.GetBytes(data);

        var startTime = DateTime.Now;
        UdpReceiveResult result;
        string DeviceResponse;
        token.CancelAfter(TimeSpan.FromSeconds(waitSeconds));
        var deviceQueries = new List<Task<RokuDevice>>();

        using(var udp = new UdpClient())
        {
            await ucs.SendAsync(discoverBytes, discoverBytes.Length,mcEndpoint);

            while (!token.IsCancellationRequested)
            {
                var operation = ucs.ReceiveAsync();
            
                try
                {
                    operation.Wait(token.Token); //Wait for result until cancelled
                }
                catch (Exception)
                {   
                    //We either have no devices or we didn't get any additional devices
                    //Not sure where to log this but can't display to user
                }

                if(operation.IsCompleted && !operation.IsFaulted)
                {  
                    result = operation.Result;
                    DeviceResponse = Encoding.ASCII.GetString(result.Buffer);
                
                    var match = Regex.Match(DeviceResponse,LOCATION_PATTERN,RegexOptions.IgnoreCase);
                    if(match.Success && DeviceResponse.ToLower().Contains("roku"))
                    {
                        var address = match.Groups["address"].Value;
                        //Don't allow device info request to hold up discovering additional devices
                        var deviceQuery = ReadDevice(address);
                        
                        deviceQueries.Add(deviceQuery);
                        
                        
                    }
                }
            }

        }

        //Wait seconds does not apply to this
        var devices = await Task.WhenAll(deviceQueries);

        return devices;
    }




    //Get additional device info
    public static async Task<RokuDevice> ReadDevice(string endpoint)
    {
        var requestUrl = endpoint + "query/device-info";
        var result = await httpClient.GetStreamAsync(requestUrl);

        var serializer = new XmlSerializer(typeof(RokuDevice));
        var device = (RokuDevice)serializer.Deserialize(result);
        device.Endpoint = endpoint;

        return device;
    }

    //Get a list of apps installed on a device
    public static async Task<IEnumerable<App>> ListDeviceApps(string endpoint)
    {
        var requestUrl = endpoint + "query/apps";
        var result = await httpClient.GetStreamAsync(requestUrl);
        
        var serializer = new XmlSerializer(typeof(DeviceAppWrapper));
        var wrapper = (DeviceAppWrapper)serializer.Deserialize(result);

        return wrapper.Apps;
    }

    //Get the current running app
    public static async Task<ActiveApp> GetCurrentApp(string endpoint)
    {
        var requestUrl = endpoint + "query/active-app";
        var result = await httpClient.GetStreamAsync(requestUrl);
        
        var serializer = new XmlSerializer(typeof(ActiveApp));
        var activeApp = (ActiveApp)serializer.Deserialize(result);

        return activeApp;
    }

    //Launch an app with the app id returned from the app listing
    public static async Task LaunchApp(string endpoint,string id)
    {
        var requestUrl = string.Format(endpoint + "launch/{0}",id);
        var result = await httpClient.PostAsync(requestUrl,null);
    }

    //Emulate the roku remote by sending a key command
    //Reference here: https://sdkdocs.roku.com/display/sdkdoc/External+Control+Guide#ExternalControlGuide-KeypressKeyValues
    public static async Task PressKey(string endpoint,string key)
    {
        var requestUrl = string.Format(endpoint + "keypress/{0}",key);
        var result = await httpClient.PostAsync(requestUrl,null);
    }

    
}
