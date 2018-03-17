using System;
using uPLibrary.Networking.M2Mqtt;
using System.Net;
using System.Text;
using System.Diagnostics;
namespace M2MqttTest
 {
	     class MainClass
	     {
		         public static void Main (string[] args)
		         {
						 Debug.WriteLine("Starting Client"); 
						 MqttClient client = new MqttClient("192.168.0.10");
			             client.Connect("testmono");
			             
			             client.MqttMsgPublishReceived += HandleClientMqttMsgPublishReceived;            
						 client.Subscribe(new string[]{"sens/mot/2"}, new byte[]{1});
			             
						 client.Publish("sens/mot/1", Encoding.UTF8.GetBytes("Hello"),1, true);
			             
			             Console.ReadLine();
			             
			         }
		  
		         static void HandleClientMqttMsgPublishReceived (object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
		         {
			            Debug.WriteLine(Encoding.UTF8.GetString(e.Message));
			         }
		    }
	 }