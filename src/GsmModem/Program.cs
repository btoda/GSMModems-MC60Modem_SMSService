using System;

namespace GsmModem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string portName = "COM1";
            if(args.Length>0){
                portName = args[0];
            }
            GsmHandler handler = new GsmHandler(portName, 9600, "0000");
            handler.SendSms("0754588580"," TEST GSM MODEM ");
        }
    }
}
