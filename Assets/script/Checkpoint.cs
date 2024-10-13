using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Animator anim;
    public string id;
    public bool activationStatus;

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    [ContextMenu("Generate checkpoint id")]
    private void GenerateId()//����ID����
    {
        id = System.Guid.NewGuid().ToString();//������path��ֱ������id
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()//������㺯��
    {
        anim.SetBool("Active", true);
        activationStatus = true;
    }
}