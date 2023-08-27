using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.WIAP.Checkers
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        private bool isConnecting = false;

        /// <summary>
        /// Called when the script starts.
        /// </summary>
        private void Start()
        {
            // Automatically connect to Photon when the game starts.
            ConnectToPhoton();
        }

        /// <summary>
        /// Connects to the Photon server using the configured settings.
        /// </summary>
        public void ConnectToPhoton()
        {
            if (!PhotonNetwork.IsConnected)
            {
                isConnecting = true;
                PhotonNetwork.ConnectUsingSettings();
            }
        }

        /// <summary>
        /// Creates a new Photon room for players to join.
        /// </summary>
        public void CreatePhotonRoom()
        {
           if (isConnecting)
            {
                Debug.LogWarning("Still connecting to Photon. Please Wait.");
                return;
            }
            PhotonNetwork.CreateRoom("Checkers Room", new RoomOptions { MaxPlayers = 4 });
        }

        /// <summary>
        /// Disconnects from the Photon server.
        /// </summary>
        public void DisconnectFromPhoton()
        {
            PhotonNetwork.Disconnect();
        }

        /// <summary>
        /// Called when connected to the Photon Master Server.
        /// </summary>
        public override void OnConnectedToMaster()
        {
            isConnecting = false;
            Debug.Log("Connected to Master");
        }

        /// <summary>
        /// Called when disconnected from the Photon server.
        /// </summary>
        /// <param name="cause">The reason for disconnection.</param>
        public override void OnDisconnected(DisconnectCause cause)
        {
            isConnecting = false;
            Debug.LogWarning("Disconncted from Photon: " + cause.ToString());
        }

        /// <summary>
        /// Called when a room is successfully created.
        /// </summary>
        public override void OnCreatedRoom()
        {
            Debug.Log("Room created: " + PhotonNetwork.CurrentRoom.Name);
        }

        /// <summary>
        /// Called when creating a room fails.
        /// </summary>
        /// <param name="returnCode">The error code.</param>
        /// <param name="message">The error message.</param>
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.LogError("Failed to create room: " + message);
        }

        /// <summary>
        /// Called when a new player enters the room.
        /// </summary>
        /// <param name="newPlayer">The new player that entered the room.</param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("Player entered the room");
            LogCurrentPlayerCount();
        }

        /// <summary>
        /// Called when a player leaves the room.
        /// </summary>
        /// <param name="otherPlayer">The player who left the room.</param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("Player left the room");
            LogCurrentPlayerCount();
        }

        /// <summary>
        /// Logs the current player count in the room.
        /// </summary>
        public void LogCurrentPlayerCount()
        {
            if (PhotonNetwork.CurrentRoom != null)
                Debug.Log("Current player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }
}