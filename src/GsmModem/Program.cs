using System;

namespace GsmModem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GsmHandler handler = new GsmHandler("COM4", 9600, "0000");
            handler.SendSms("0754588580"," TEST GSM MODEM ");
        }
    }
}
