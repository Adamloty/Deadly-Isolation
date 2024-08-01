using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Voice;
using Photon.Voice.Unity;

public class Playerinfo : MonoBehaviourPunCallbacks
{
    private Animator PlayerLeftanim;
    private Text playerleftname;
    private PhotonView pv;
    private Button Mic;
    private Image Micesprite;
    [SerializeField] private Sprite MicOff;
    [SerializeField] private Sprite MicOn;
    private Recorder recorder;
    private bool openMic;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        PlayerLeftanim = GameObject.Find("Canvas").transform.GetChild(6).GetComponent<Animator>();
        playerleftname = PlayerLeftanim.gameObject.GetComponentInChildren<Text>();
        Mic= GameObject.Find("Canvas").transform.GetChild(7).GetComponent<Button>();
        recorder=GetComponent<Recorder>();
        Micesprite=Mic.GetComponent<Image>();
        Mic.onClick.AddListener(ClickMic);
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
    private void ClickMic()
    {
        if(pv.IsMine)
        {
            openMic = !openMic;
            if (openMic)
            {
                Micesprite.sprite = MicOn;
                recorder.TransmitEnabled = true;
            }
            else
            {
                Micesprite.sprite = MicOff;
                recorder.TransmitEnabled = false;
            }
        }
     
    }
}
