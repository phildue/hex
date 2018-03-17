using UnityEngine;
using System.Collections;

public class MqttHandler : MonoBehaviour {
	private RigidBody rb;
	public double step_width;
	public double jump_height;
	public double duck_height;
	public double plank_height;
	public string subscribe_channel;
	public string publish_channel;
	private Dictionary<string,Action> handlers;

	public void Start () {
		rb = GetComponent<RigidBody> ();
		step_width = 0.5;
		jump_height = 1.0;
		duck_height = -0.5;
		plank_height = -0.5;
		subscribe_channel = "pao/controls";
		publish_channel = "vr/status";
		handlers = new Dictionary<string,Action> {
			{ "jump",() => jump },
			{ "duck",() => duck },
			{ "plank",() => plank },
			{ "left",() => left },
			{ "right",() => right },
		};

		Debug.WriteLine("Starting MqttClient"); 
		try{

			MqttClient client = new MqttClient("localhost");
			client.Connect("vrapp");

			client.MqttMsgPublishReceived += HandleClientMqttMsgPublishReceived;            
			client.Subscribe(new string[]{subscribe_channel}, new byte[]{1});

			client.Publish(publish_channel,Encoding.UTF8.GetBytes("VR is listening.."),1, true);}
		catch(Exception e){
			Debug.WriteLine ("Could not connect");
		}
	}

	void HandleClientMqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
	{
		string msg = System.Text.Encoding.UTF8.GetString(e.Message);
		Debug.Write ("Received message from " + e.Topic + " : " + msg);
		try {
			handlers [msg] ();
		}catch(Exception e1){
			Debug.WriteLine("Unknown Message");
		}

	}

	void jump(){
		Debug.WriteLine("Jump");
		Vector3 movement = new Vector3(0,jump_height,0);
		rb.AddForce(movement);
	}
	void duck(){
		Debug.WriteLine("Duck"); 
		Vector3 movement = new Vector3(0,duck_height,0);
		rb.AddForce(movement);
	}
	void plank(){
		Debug.WriteLine("Planck"); 
		Vector3 movement = new Vector3(0,plank_height,0);
		rb.AddForce(movement);
	}
	void left(){
		Debug.WriteLine("Left"); 
		Vector3 movement = new Vector3(-1*step_width,0,0);
		rb.AddForce(movement);
	}
	void right(){
		Debug.WriteLine("Right"); 
		Vector3 movement = new Vector3(step_width,0,0);
		rb.AddForce(movement);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

