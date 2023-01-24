using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerItem : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    // Start is called before the first frame update
    public void SetPlayerInfo(Player _player) {
        playerName.text = _player.NickName;
    }
}
