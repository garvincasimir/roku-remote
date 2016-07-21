using System.Threading.Tasks;
using System;
using RokuDeviceLib;

namespace RokuRemote
{
    public class RemoteControl
    {
        public static async Task Start(string endpoint)
        {
            Console.WriteLine("Use the following Keys for your remote");
            Console.WriteLine("Home => h");
            Console.WriteLine("Enter => e");
            Console.WriteLine("Fwd => .");
            Console.WriteLine("Rev => ,");
            Console.WriteLine("Play => p");
            Console.WriteLine("Select => s");
            Console.WriteLine("Left => l"); 
            Console.WriteLine("Right => r");
            Console.WriteLine("Down => d");
            Console.WriteLine("Up => u");
            Console.WriteLine("Back => b");
            Console.WriteLine("InstantReplay => x");
            Console.WriteLine("Info => i");
            Console.WriteLine("Backspace => k");
            Console.WriteLine("Search => f");
            Console.WriteLine("Type q to exit remote control mode");

            var k = Console.ReadKey(true);
            do
            {
                
                if(k.Key == ConsoleKey.H)
                {  
                    await RokuClient.PressKey(endpoint,"Home");
                }
                else if(k.Key == ConsoleKey.E)
                {
                    await RokuClient.PressKey(endpoint,"Enter");
                }
                else if(k.Key == ConsoleKey.OemPeriod)
                {
                    await RokuClient.PressKey(endpoint,"Fwd");
                }
                else if(k.Key == ConsoleKey.OemComma)
                {
                    await RokuClient.PressKey(endpoint,"Rev");
                }
                else if(k.Key == ConsoleKey.P)
                {
                    await RokuClient.PressKey(endpoint,"Play");
                }
                else if(k.Key == ConsoleKey.S)
                {
                    await RokuClient.PressKey(endpoint,"Select");
                }
                else if(k.Key == ConsoleKey.L)
                {
                    await RokuClient.PressKey(endpoint,"Left");
                }
                else if(k.Key == ConsoleKey.U)
                {
                    await RokuClient.PressKey(endpoint,"Up");
                }
                else if(k.Key == ConsoleKey.R)
                {
                    await RokuClient.PressKey(endpoint,"Right");
                }
                else if(k.Key == ConsoleKey.D)
                {
                    await RokuClient.PressKey(endpoint,"Down");
                }
                else if(k.Key == ConsoleKey.B)
                {
                    await RokuClient.PressKey(endpoint,"Back");
                } 
                else if(k.Key == ConsoleKey.X)
                {
                    await RokuClient.PressKey(endpoint,"InstantReplay");
                }
                else if(k.Key == ConsoleKey.I)
                {
                    await RokuClient.PressKey(endpoint,"Info");
                }
                else if(k.Key == ConsoleKey.K)
                {
                    await RokuClient.PressKey(endpoint,"Backspace");
                }
                else if(k.Key == ConsoleKey.F)
                {
                    await RokuClient.PressKey(endpoint,"Search");
                }               
                k = Console.ReadKey(true);
            }
            while (k.Key != ConsoleKey.Q);

        }

    }
}