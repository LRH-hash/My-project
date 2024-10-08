using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    public Player player;
    public PlayerStateMachine StateMachine;
    public string animBoolname;
    public Rigidbody2D rb;
    public float xinput;
    public float yinput;
    public float movespeed = 4;
    public float statetimer;
    public SkillManager skill;

    public PlayerState(Player _player,PlayerStateMachine _playerStateMachine,string _animname)
    {
        player = _player;
        StateMachine = _playerStateMachine;
        animBoolname = _animname;
    }


    // Start is called before the first frame update
    public void Start()
    {
       
    }

    // Update is called once per frame
 
    public virtual void Enter()
    {
        player.anim.SetBool(animBoolname, true);
        rb = player.rb;
        skill = SkillManager.instance;
    }
    public virtual void Update()
    {
        xinput = Input.GetAxisRaw("Horizontal");
        yinput = Input.GetAxisRaw("Vertical");
        statetimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolname, false);
        
    }
  
}
