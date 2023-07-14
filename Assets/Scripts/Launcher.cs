using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.WIAP.Checkers
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private bool isConnecting = false;
        private TextMesh debugText;

        private void Start()
        {
            debugText = GetComponent<TextMesh>();
            ConnectToPhoton();
        }

        public void ConnectToPhoton()
        {
            if (!PhotonNetwork.IsConnected)
            {
                isConnecting = true;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        public void CreatePhotonRoom()
        {
           if (isConnecting)
            {
                Debug.LogWarning("Still connecting to Photon. Please Wait.");
                return;
            }
            PhotonNetwork.CreateRoom("Checkers Room", new RoomOptions { MaxPlayers = 4 });
        }

        public void DisconnectFromPhoton()
        {
            PhotonNetwork.Disconnect();
        }

        public override void OnConnectedToMaster()
        {
            isConnecting = false;
            Debug.Log("Connected to Master");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            isConnecting = false;
            Debug.LogWarning("Disconncted from Photon: " + cause.ToString());
        }

        public override void OnCreatedRoom()
        {
            Debug.Log("Room created: " + PhotonNetwork.CurrentRoom.Name);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            UpdateDebugText("Failed to create room: " + message);
            Debug.LogError("Failed to create room: " + message);
        }

        private void UpdateDebugText(string message)
        {
            debugText.text = message;
        }
    }
}