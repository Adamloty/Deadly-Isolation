using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public Text Name;
    public void JoinRoom()
    {
        GameObject.Find("JoinorCreate room").GetComponent<createorjoinroom>().JoinRoomList(Name.text);
    }

}
