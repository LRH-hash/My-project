using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    PlayerCharacter player;
    void Awake()
    {
        player = GetComponent<PlayerCharacter>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        player.Move(h);
        player.InAttack();
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            player.music.Pause();
            player.Jump();
        }
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            player.StartSliding();
        }
        if (Input.GetKeyDown(KeyCode.X)) 
        {
            player.Attack();
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            player.StartBackStep();
        }
        if (Input.GetKeyDown(KeyCode.C)) 
        {
            player.HeavyAttack();
        }
    }
}
