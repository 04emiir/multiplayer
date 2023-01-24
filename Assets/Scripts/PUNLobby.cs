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

    public RoomItems roomItemPrefab;
    List<RoomItems> roomItems = new List<RoomItems>();
    public Transform contentObject;

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    List<PlayerItem> playerItemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.JoinLobby(); 
    }

    private void Update() {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2) {
            playButton.SetActive(true);
        } else {
            playButton.SetActive(false);
        }
    }

    public void OnClickPlay() {
        PhotonNetwork.LoadLevel("GameScene");
    }

    public void OnClickCreate() {
        if (roomInput.text.Length >= 1) {
            PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions() { MaxPlayers = 5 });
        }
    }

    public override void OnJoinedRoom() { 
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        if (Time.time >= nextUpdateTime) {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time;
        }
    }

    void UpdateRoomList(List<RoomInfo> list) {
        foreach (RoomItems item in roomItems) {
            Destroy(item.gameObject);
        }
        roomItems.Clear();

        foreach (RoomInfo room in list) {
            RoomItems newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.SetRoomName(room.Name);
            roomItems.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName) {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickLeaveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
    }

    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinLobby();
    }

    void UpdatePlayerList() {
        foreach (PlayerItem item in playerItemsList) {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null) {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players) {
            PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.SetPlayerInfo(player.Value);
            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player newPlayer) {
        UpdatePlayerList();
    }

}
