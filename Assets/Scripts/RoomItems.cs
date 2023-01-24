using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomItems : MonoBehaviour
{
    public TextMeshProUGUI textoSala;
    public PUNLobby manager;
    // Start is called before the first frame update

    public void Start() {
        manager = FindObjectOfType<PUNLobby>();
    }

    public void SetRoomName(string _textoSala) { 
        textoSala.text = _textoSala;
    }


    public void OnClickItem() {
        manager.JoinRoom(textoSala.text);
    }
}
