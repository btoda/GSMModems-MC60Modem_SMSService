﻿using System;

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
            GsmHandler handler = new GsmHandler("MODEM01", portName, 9600, "0000");
            Console.WriteLine(handler.SendSms("0754588580"," TEST GSM MODEM 1"));
        }
    }
}
