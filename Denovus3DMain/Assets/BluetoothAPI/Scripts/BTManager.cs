using UnityEngine;
using ArduinoBluetoothAPI;
using System;

public class BTManager : MonoBehaviour {

	BluetoothHelper bluetoothHelper;
	public string deviceName;			// String for Glove Bluetooth Device Name
	string message;						// String for raw BT data
	//public GameObject cube;				// Object used for testing
	[HideInInspector]
	public int[] sensorValue;           // Array to hold usable sensor data
	static BTManager instance;
	private float reconnectCounter = 0f;

	void Awake()
    {
		if (instance == null)
		{
			instance = this;                    // Create singleton in first load-up
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
			Destroy(gameObject);				// Destroy duplicates if initial scene reloaded
    }

	void Start () 
	{
		sensorValue = new int[10];				// Set array to hold 10 different values
		try
		{
			bluetoothHelper = BluetoothHelper.GetInstance(deviceName);
			BluetoothConnect();					// Listens and connects to BT device
		}
		catch (Exception ex) 
		{
			Debug.Log (ex.Message);
		}

		InvokeRepeating(nameof(UpdateBluetooth), 0f, 0.04f);
	}

	// Update is called once per frame
	public void UpdateBluetooth()
	{
        if (!bluetoothHelper.isConnected())
        {
			Debug.Log("Bluetooth is not connected");
			// Assign error values
			for (int i = 0; i < 10; i++)
			{
				sensorValue[i] = -999;
				//Debug.Log("SensorValue[" + i + "] is: " + sensorValue[i]);
			}

			reconnectCounter++;

			// Try to reconnect to glove every 5 seconds if not connected
			if (reconnectCounter >= 125f)
            {
				BluetoothConnect();
				reconnectCounter = 0f;
				Debug.Log("Attempting to Reconnect");
			}
		}
        else
        {
			message = bluetoothHelper.Read();

			if (message == null || message == "")
			{
				// Check if message is empty, if empty assign error values and return
				for (int i = 0; i < 10; i++)
				{
					sensorValue[i] = -999;
					//Debug.Log("SensorValue[" + i + "] is: " + sensorValue[i]);
				}
				return;
			}

			else
			{
				//cube.GetComponent<Renderer>().material.color = Color.green;
				// Store data between commas
				string[] dataString = message.Split(',');

				// If message contains data, check for 10 values and store in array
				if (dataString[0] != "" && dataString[1] != "" && dataString[2] != "" && dataString[3] != "" && dataString[4] != "" && dataString[5] != "" && dataString[6] != "" && dataString[7] != "" && dataString[8] != "" && dataString[9] != "") //Check if all values are received
				{
					// Convert string values to float values
					for (int i = 0; i < 10; i++)
					{
						sensorValue[i] = int.Parse(dataString[i]);
						//Debug.Log("SensorValue[" + i + "] is: " + sensorValue[i]);
					}

					// Rotate the object for testing
					//if (sensorValue[0] > 100)
                    //{
					//	cube.transform.Rotate(sensorValue[0] * 0.1f, 0, 0);
					//}
					//else if (sensorValue[1] > 100)
					//{
					//	cube.transform.Rotate(0, sensorValue[1] * 0.1f, 0);
					//}
					//else if (sensorValue[2] > 100)
					//{
					//	cube.transform.Rotate(0, 0, sensorValue[2] * 0.1f);
					//}
				}
			}
		}
    }

	////////////Make below debug messages visible to user
	public void BluetoothConnect()
	{
		if (!bluetoothHelper.isConnected())
		{
			if (!bluetoothHelper.isDevicePaired())
			{
				Debug.Log(deviceName + " is not paired with Oculus Quest");
			}
			else
			{
				try
				{
					bluetoothHelper.OnConnected += OnConnected;					// Begins bluetooth listening
					bluetoothHelper.OnConnectionFailed += OnConnectionFailed;	// Displays failure message
					bluetoothHelper.setTerminatorBasedStream("\n");				// Delimits received messages at \n
					bluetoothHelper.Connect();									// Tries to connect
				}
				catch (Exception ex)
				{
					Debug.Log(ex.Message);
				}
			}
		}
		else
		{
			Debug.Log(deviceName + " is already connected");
		}
	}

	void OnConnected(BluetoothHelper helper)
	{
		try
		{
			helper.StartListening();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}

	void OnConnectionFailed(BluetoothHelper helper)
	{
		Debug.Log("Connection Failed");
	}
	
	public void BluetoothDisconnect()
	{
		if (bluetoothHelper.isConnected())
        {
			bluetoothHelper.Disconnect();
		}
		else
        {
			Debug.Log(deviceName + " is already disconnected");
		}
	}

	void OnDestroy()
	{
		if(bluetoothHelper!=null)
		bluetoothHelper.Disconnect ();
	}
}
