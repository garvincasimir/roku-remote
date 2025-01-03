﻿using System;
using System.Linq;
using System.Threading.Tasks;
using RokuDeviceLib;

namespace RokuRemote;

public class Program
{
    private const string MC_IP = "239.255.255.250";
    private const int MC_Port = 1900;
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Discovering Roku Devices Please Wait.....");
        Console.WriteLine();
        var discoveredDevices = await Client.Discover(MC_IP,MC_Port,15);
        Console.WriteLine();
        if(discoveredDevices.Length > 0)
        {
            Console.WriteLine("-========== Summary ==========-");
            foreach (var item in discoveredDevices.Select((device,index)=> new { Device = device, Index =index}))
            {
                Console.WriteLine("Device : {0}",item.Index + 1);
                Console.WriteLine("Model: {0}",item.Device.ModelName);
                Console.WriteLine("Serial Number: {0}",item.Device.SerialNumber);
                Console.WriteLine("-====================-");
                Console.WriteLine();
            }
            
            await Menu.RequestDeviceSelection(discoveredDevices);
        }
        else
        {
            Console.WriteLine("No devices found...");
        }
            
    }
}