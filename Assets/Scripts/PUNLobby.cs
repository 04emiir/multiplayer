using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PUNLobby : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomInput;
    public TextMeshProUGUI roomName;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.JoinLobby(); 
    }

    public void OnClickCreate() {
        if (roomInput.text.Length >= 1)
            PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions() { MaxPlayers = 5 });
    }

    public override void OnJoinedRoom() { 
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
    }


    
}
