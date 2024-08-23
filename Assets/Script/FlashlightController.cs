using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlashlightController : MonoBehaviourPun
{
    public Light flashlight; // —«»Ў ≈бм гяжд «бя‘«Ё

    void Start()
    {
        //  √яѕ гд √д яб б«Џ» бѕне Ќ«б… «бя‘«Ё ’ЌнЌ… Џдѕ »ѕЅ «ббЏ»…
        if (photonView.IsMine)
        {
            //  Џннд «бя‘«Ё бняжд гЁЏб Џдѕ »ѕЅ «ббЏ»…
            flashlight.enabled = true;
        }
        else
        {
            //  √яѕ гд √д «бя‘«Ё џн— гЁЏб бб«Џ»нд «б¬ќ—нд
            flashlight.enabled = false;
        }
    }

    void Update()
    {
        //  Ќёё гд ≈ѕќ«б «бб«Џ» «б–н нг бя «бя‘«Ё ЁёЎ
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.F))
        {
            photonView.RPC("ToggleFlashlight", RpcTarget.AllBuffered); // «” ќѕг RpcTarget.AllBuffered б÷г«д  ЌѕнЋ ћгнЏ «бб«Џ»нд Ќ м Џдѕ «д÷г«гег б«Ќё«р
        }
    }

    [PunRPC]
    void ToggleFlashlight()
    {
        //  џнн— Ќ«б… «бя‘«Ё
        flashlight.enabled = !flashlight.enabled;
    }
}
