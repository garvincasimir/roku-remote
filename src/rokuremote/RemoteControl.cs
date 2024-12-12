using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RokuDeviceLib;

namespace RokuRemote;

public class RemoteControl 
{
    private static readonly Dictionary<ConsoleKey, string> KeyMap = new()
    {
        { ConsoleKey.H, "Home" },
        { ConsoleKey.E, "Enter" },
        { ConsoleKey.OemPeriod, "Fwd" },
        { ConsoleKey.OemComma, "Rev" },
        { ConsoleKey.P, "Play" },
        { ConsoleKey.S, "Select" },
        { ConsoleKey.L, "Left" },
        { ConsoleKey.U, "Up" },
        { ConsoleKey.R, "Right" },
        { ConsoleKey.D, "Down" },
        { ConsoleKey.B, "Back" },
        { ConsoleKey.X, "InstantReplay" },
        { ConsoleKey.I, "Info" },
        { ConsoleKey.K, "Backspace" },
        { ConsoleKey.F, "Search" }
    };

    public static async Task Run(string endpoint)
    {
        Console.WriteLine("Use the following Keys for your remote");
        foreach (var key in KeyMap.Keys)
        {
            Console.WriteLine($"{KeyMap[key]} => {key}");
        }
        var k = Console.ReadKey(true);
        do
        {
            if (KeyMap.TryGetValue(k.Key, out string rokuKey))
            {
                await Client.PressKey(endpoint, rokuKey);
            }
            k = Console.ReadKey(true);
        }
        while (k.Key != ConsoleKey.Q);
    }
}