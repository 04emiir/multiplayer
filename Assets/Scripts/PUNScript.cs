using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PUNScript : MonoBehaviourPunCallbacks {
    public TMP_InputField usernameInput;
    public TextMeshProUGUI buttonText;
    // Start is called before the first frame update
    public void OnClickConnect() {
        if (usernameInput.text.Length >= 1) {
            PhotonNetwork.NickName = usernameInput.text;
            buttonText.fontSize = 56;
            buttonText.text = "Conectando...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster() {
        SceneManager.LoadScene("LobbyScene");
    }
}


