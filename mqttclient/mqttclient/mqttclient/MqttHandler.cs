using UnityEngine;
using System.Collections;

public class MqttHandler : MonoBehaviour {
	public GameObject player;
	public void Start () {
		Debug.WriteLine("Starting MqttClient"); 
		try{

			MqttClient client = new MqttClient("localhost");
			client.Connect("testmono");

			client.MqttMsgPublishReceived += HandleClientMqttMsgPublishReceived;            
			client.Subscribe(new string[]{"pao/controls"}, new byte[]{1});

			client.Publish("vr/status", Encoding.UTF8.GetBytes("VR is listening.."),1, true);}
		catch(Exception e){
			Debug.WriteLine ("Could not connect");
		}
	}

	void HandleClientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
	{
		string msg = System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Write ("Received message from " + e.Topic + " : " + msg);
		switch (msg) {
		case "jump":
			this.jump ();
			break;
		case "duck":
			this.duck ();
			break;
		case "plank":
			this.plank ();
			break;
		case "left":
			this.left ();
			break;
		case "right":
			this.right ();
			break;
		}

	}

	void jump(){
		Debug.WriteLine("Jump"); 
	}
	void duck(){
		Debug.WriteLine("Duck"); 
	}
	void plank(){
		Debug.WriteLine("Plack"); 
	}
	void left(){
		Debug.WriteLine("Left"); 
	}
	void right(){
		Debug.WriteLine("Right"); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

