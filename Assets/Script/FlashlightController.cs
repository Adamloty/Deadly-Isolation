using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FlashlightController : MonoBehaviourPun
{
    public Light flashlight; // ���� ��� ���� ������

    void Start()
    {
        // ���� �� �� �� ���� ���� ���� ������ ����� ��� ��� ������
        if (photonView.IsMine)
        {
            // ����� ������ ����� ���� ��� ��� ������
            flashlight.enabled = true;
        }
        else
        {
            // ���� �� �� ������ ��� ���� ������� �������
            flashlight.enabled = false;
        }
    }

    void Update()
    {
        // ���� �� ����� ������ ���� ����� ������ ���
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.F))
        {
            photonView.RPC("ToggleFlashlight", RpcTarget.AllBuffered); // ������ RpcTarget.AllBuffered ����� ����� ���� �������� ��� ��� �������� ������
        }
    }

    [PunRPC]
    void ToggleFlashlight()
    {
        // ����� ���� ������
        flashlight.enabled = !flashlight.enabled;
    }
}
