using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;
    private List<GameObject> roomObjects = new List<GameObject>();
    private TypedLobby sqlLobby = new TypedLobby("myLobby", LobbyType.SqlLobby);

    //private RoomList Roomslist;

    private void Start()
    {
        if (PhotonNetwork.InLobby)
        {
            RequestRoomList();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
        InvokeRepeating("RequestRoomList", 0f, 5f); // تحديث القائمة كل 5 ثواني
    }

    public override void OnJoinedLobby()
    {
        RequestRoomList();
    }


    private void RequestRoomList()
    {
        if(PhotonNetwork.IsConnected)
        {
            if(PhotonNetwork.InLobby)
            {
                PhotonNetwork.GetCustomRoomList(sqlLobby, "C0=1");
            }
        }
       
    }
    private void Update()
    {
       
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateRoomList(roomList);
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        // مسح الغرف القديمة
        foreach (GameObject roomObj in roomObjects)
        {
            Destroy(roomObj);
        }
        roomObjects.Clear();

        // إنشاء الغرف الجديدة
        foreach (RoomInfo roomInfo in roomList)
        {
            if (roomInfo.IsOpen && roomInfo.IsVisible && roomInfo.PlayerCount > 0 && roomInfo.MaxPlayers > roomInfo.PlayerCount)
            {
                GameObject roomObj = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Content").transform);
                roomObj.GetComponent<Room>().SetRoomInfo(roomInfo);
                roomObjects.Add(roomObj);
            }
        }
    }
}