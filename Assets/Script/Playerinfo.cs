using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Playerinfo : MonoBehaviourPunCallbacks
{
    private Animator PlayerLeftanim;
    private Text playerleftname;
    private PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        PlayerLeftanim = GameObject.Find("Canvas").transform.GetChild(6).GetComponent<Animator>();
        playerleftname = PlayerLeftanim.gameObject.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        playerleftname.text = otherPlayer.NickName + " Lefted The Room";
        PlayerLeftanim.SetTrigger("Left");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        playerleftname.text = newPlayer.NickName + " Entered The Room";
        PlayerLeftanim.SetTrigger("Left");
    }
}
