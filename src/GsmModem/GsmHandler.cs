using System;
using System.IO.Ports;

namespace GsmModem
{
    public class GsmHandler{
        private string _pinCode = null;
        private SerialPort _port = null;
        public GsmHandler(string serialPort, int baudRate, string pinCode){
            _port = new SerialPort(serialPort, baudRate);
            _port.Open();
            _pinCode = pinCode;
            if(_pinCode == null || _pinCode.Length!=4){
                throw new Exception("Invalid pincode.");
            }

            this.Test();


            this.Init();

        }

        private void Test(){
            Console.WriteLine("testing");
            SendMessage("ATI");
            WaitForReply();
        }

        private void SendMessage(string message){
            Console.WriteLine("Sending message: "+message);
            _port.WriteLine(message);
        }

        private string WaitForReply(params string[] waitedMessages){
            bool done = false;
            int maxLines=5;
            int lines = 0;
            string resultMessage = null;
            while(!done && lines<maxLines){
                string line = _port.ReadLine();
                Console.WriteLine("Message Received: "+line);
                lines ++;
                if(line.Contains("ERROR")){
                    throw new Exception("GSM Error " + line);
                }
                if(waitedMessages!=null)
                    foreach(string waitedMessage in waitedMessages){
                        if(line.Contains(waitedMessage)){
                            resultMessage = waitedMessage;
                        }
                    }
                if(line.Contains("OK")){
                    return resultMessage;
                }
            }
            return null;
        }

        private void CheckPIN(){
            SendMessage("AT+CPIN?");
            WaitForReply();
            string result = WaitForReply("+CPIN: SIM PIN");
            if(result == "+CPIN: SIM PIN")
            {
                // enter pin
                SendMessage("AT+CPIN="+_pinCode);
                result = WaitForReply("+CPIN: READY");
                if(result!="+CPIN: READY"){
                    throw new Exception("Invalid pincode.");
                }
                // wait for 5 seconds for the service to start
                Console.WriteLine("Waiting for service to start...");
                System.Threading.Thread.Sleep(10000);
            }
        }

        private void Init(){
            CheckPIN();
        }

        public void SendSms(string phoneNumber, string message){
            SendMessage("AT+CMGS=?");
            string result = WaitForReply("OK");
            if(result!="OK"){
                throw new Exception("Message could not be sent");
            }

            SendMessage("AT+CMGF=1");
            if(WaitForReply("OK")!="OK") throw new Exception("Could not send SMS");

            SendMessage("AT+CSCS=\"GSM\"");
            if(WaitForReply("OK")!="OK") throw new Exception("Could not send SMS");

            SendMessage("AT+CMGS=\""+phoneNumber+"\"");
            SendMessage(message+(char)26); // (char)26 - ctrl+z
            WaitForReply("OK");
        }
    }
}