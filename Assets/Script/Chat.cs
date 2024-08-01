using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
//using ArabicSupport;
public class Chat : MonoBehaviour
{
    public InputField inputfield;
    public GameObject Message;
    public GameObject content;
    private Animator chat;
    private bool open;
   // private bool close;
    // Start is called before the first frame update
    void Start()
    {
        chat=GetComponent<Animator>();
        chat=transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // inputfield.text =ArabicFixer.Fix(inputfield.text);
        if(Input.GetKeyDown(KeyCode.Return)&&inputfield.text!="")
        {
            SendMessage();
        }
        
    }
    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, PlayerPrefs.GetString("ur") + ": " + inputfield.text);
        inputfield.text = "";
    }
    [PunRPC]
    public void GetMessage(string ReciveMessage)
    {
        GameObject M = Instantiate(Message, Vector3.zero,Quaternion.identity,content.transform);
       // M.transform.SetParent(content.transform);
        M.GetComponent<Message>().message.text =ReciveMessage;
    }
    public void OpenChat()
    {
        if(!open)
        {
            chat.SetTrigger("Open");
        }
        open = true;
       // close = false;
    }
    public void CloseChat()
    {
        if (open)
        {
            chat.SetTrigger("Close");
        }
        open = false;
      //  close = true;
    }
}
