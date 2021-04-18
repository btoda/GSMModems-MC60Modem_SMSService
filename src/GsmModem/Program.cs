using System;

namespace GsmModem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string portName = "COM4";
            if(args.Length>0){
                portName = args[0];
            }
            Console.WriteLine(portName);
            GsmHandler handler = new GsmHandler(portName, 9600, "0000");
            handler.SendSms("0754588580"," TEST GSM MODEM 1");
            handler.SendSms("0754588580"," TEST GSM MODEM 2");
            handler.SendSms("0754588580"," TEST GSM MODEM 3");
            handler.SendSms("0754588580"," TEST GSM MODEM 4");
        }
    }
}
