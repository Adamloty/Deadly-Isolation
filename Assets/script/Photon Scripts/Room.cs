using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text playersText;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        nameText.text = roomInfo.Name;
        playersText.text = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(nameText.text);
    }
}
