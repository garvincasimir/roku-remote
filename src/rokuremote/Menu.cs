using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using RokuDeviceLib;

namespace RokuRemote
{
    public class Menu
    {
        
        public static async Task RequestDeviceSelection( RokuDevice[] devices)
        {
                Console.WriteLine("Please select a device by typing in a Device # from above");
                Console.WriteLine("Enter q at any time to exit");
                string entry = Console.ReadLine();
                int deviceId;

                while (!int.TryParse(entry, out deviceId) || ( deviceId == 0 || deviceId > devices.Length)  )
                {
                    if(entry.Contains("q"))
                    {
                        break;
                    }

                    Console.WriteLine("Please enter a valid device number starting from 1");
                    entry = Console.ReadLine();
                }

            
                if(deviceId!=0)
                {
                    var selectedDevice = devices[deviceId-1];
                    Console.WriteLine("---++++++++ Selected Device +++++---");
                    Console.WriteLine("Device : {0}",deviceId);
                    Console.WriteLine("Model: {0}",selectedDevice.ModelName);
                    Console.WriteLine("Serial Number: {0}",selectedDevice.SerialNumber);
                    Console.WriteLine("---++++++++++++++++++++++++++++++++++");
                    int result = 0;
                    do
                    {
                       result = await Menu.RequestMenuSelection(selectedDevice);

                    }
                    while(result==0);

                    if(result == 1)
                    {
                      await RequestDeviceSelection(devices);
                    }
                }


       }

        public static async Task<int> RequestMenuSelection(RokuDevice selectedDevice)
        {    

            Console.WriteLine();
            Console.WriteLine("---=========== Main Menu ========---");
            Console.WriteLine("1. App Selection");
            Console.WriteLine("2. Show Current App");
            Console.WriteLine("3. Remote Control Mode"); 
            Console.WriteLine("4. Select Another Device"); 

           string entry = Console.ReadLine();
           int menuId;
           var options = 4;

           while (!int.TryParse(entry, out menuId) || ( menuId == 0 || menuId > options)  )
           {
                if(entry.Contains("q"))
                {
                    break;
                }

                Console.WriteLine("Please enter a valid menu number from 1 to {0}",options);
                entry = Console.ReadLine();
           }

           var result = 0; //return to menu 0 to exit;
            switch(menuId)
            {
                case 1:        
                    var apps = await RokuClient.ListDeviceApps(selectedDevice.Endpoint);
                    foreach(var app in apps.OrderBy(a=> a.Name))
                    {
                                Console.WriteLine("{0}.\t {1}",app.Id,app.Name);
                    }
                    await RequestAppSelection(selectedDevice.Endpoint,apps);
                    break;
                case 2:
                    var currentApp = await RokuClient.GetCurrentApp(selectedDevice.Endpoint);

                    Console.WriteLine("---++++++++ Current App +++++---");
                    Console.WriteLine("Id : {0}",currentApp.App.Id);
                    Console.WriteLine("Name: {0}",currentApp.App.Name);
                    Console.WriteLine("Type: {0}",currentApp.App.Type);
                    Console.WriteLine("Showing ScreenSaver: {0}",currentApp.ScreenSaver == null? "No": currentApp.ScreenSaver.Name);
                    Console.WriteLine("---++++++++++++++++++++++++++++++++++");

                    break;
                case 3:
                    await RemoteControl.Start(selectedDevice.Endpoint);
                    break;
                case 4:
                        return 1;

            }

            return result;

        }

         public static async Task RequestAppSelection(string endpoint, IEnumerable<RokuApp> apps )
         {
            Console.WriteLine("Please enter the Id of the app you would like to switch to");
            Console.WriteLine("Type in q if you want to return to the menu");

           string appId = Console.ReadLine();
   

           while (!apps.Any(a => a.Id == appId) )
           {
                if(appId == "q")
                {
                    break;
                }

                Console.WriteLine("Please enter a valid appp Id");
                appId = Console.ReadLine();
           }

           //I know I know what if your app is called q
          if(appId != "q")
          {
            await RokuClient.LaunchApp(endpoint,appId);
          }

         }
    }
}