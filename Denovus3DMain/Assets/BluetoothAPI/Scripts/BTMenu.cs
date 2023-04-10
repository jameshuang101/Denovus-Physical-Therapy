using UnityEngine;

public class BTMenu : MonoBehaviour
{
    BTManager BTManager;

    public void BTConnect()
    {
        BTManager = FindObjectOfType<BTManager>();
        BTManager.BluetoothConnect();
    }

    public void BTDisconnect()
    {
        BTManager = FindObjectOfType<BTManager>();
        BTManager.BluetoothDisconnect();
    }
}