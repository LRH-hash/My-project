using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_DeathBringer : enemy
{
    public bool bossbegan;
    public float spellAmount;
    public float spellcooldown;
    public GameObject spellPrefab;
    public float spellcastCoolDown;
    public float lastspellcasttime;
    public BoxCollider2D arena;
    public Vector2 surroundingSize;
    public Transform surroundingTransform;
    public float chanceToteleport;
    public float defaultchanceToteleport=25;  
   public Death_move death_move { get; private set; }
    public Death_attack death_Attack { get; private set; }
    public Death_battle death_Battle { get; private set; }
    public Death_spellcast death_Spellcast { get; private set; }
    public Death_teleport death_Teleport { get; private set; }
    public Death_Dead death_Dead { get; private set; }
    public Death_idle death_Idle { get; private set; }
   public override void Start()
    {
        base.Start();
        death_Idle = new Death_idle(this, EnemyStateMachine, "Idle", this);
        death_move = new Death_move(this, EnemyStateMachine, "Move", this);
        death_Attack = new Death_attack(this, EnemyStateMachine, "Attack", this);
        death_Battle = new Death_battle(this, EnemyStateMachine, "Move", this);
        death_Spellcast = new Death_spellcast(this, EnemyStateMachine, "SpellCast", this);
        death_Teleport = new Death_teleport(this, EnemyStateMachine, "TelePort", this);
        death_Dead = new Death_Dead(this, EnemyStateMachine, "Die", this);
        EnemyStateMachine.Init(death_Idle);
      
    }
   public override void Update()
    {
        base.Update();
      
    }
    public override void Die()
    {
        base.Die();
        EnemyStateMachine.ChangeState(death_Dead);
        Destroy(gameObject, 1);
    }
    public void Canspell()
    {
        float xoffset = 0;
        if (player.rb.velocity.x != 0)
            xoffset = player.moveRight * 3;
        GameObject newspell = Instantiate(spellPrefab, new Vector3(player.transform.position.x + xoffset, player.transform.position.y+2.2f,0), Quaternion.identity);
        newspell.GetComponent<DeathSpell_Controller>().setspell(charactState);
    }
    public void FindPosition()
    {
        float x = Random.Range(arena.bounds.min.x + 3, arena.bounds.max.x - 3);
        float y = Random.Range(arena.bounds.min.y + 3, arena.bounds.max.y - 3);
        transform.position = new Vector2(x, y);
        transform.position = new Vector2(transform.position.x, transform.position.y - GroundBelow().distance + (cd.size.y/2));
        if (!GroundBelow() || somethingisGround())
            FindPosition();
    }
    public RaycastHit2D GroundBelow() => Physics2D.Raycast(transform.position, Vector2.down, 100, Ground);
    public bool somethingisGround() => Physics2D.BoxCast(transform.position, surroundingSize, 0, Vector2.zero,0,Ground);
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(surroundingTransform.position,new Vector2(surroundingTransform.position.x, surroundingTransform.position.y-GroundBelow().distance));
        Gizmos.DrawWireCube(surroundingTransform.position, surroundingSize);
        
    }
    public bool canteleport()
    {
        if (Random.Range(0, 100) < chanceToteleport)
        {
            chanceToteleport = defaultchanceToteleport;
            return true;
        }
        else return false;
    }
    public bool canSpellCast()
    {
        if (Time.time > lastspellcasttime + spellcastCoolDown)
        {
            lastspellcasttime = Time.time;
            return true;
        }
        return false;
    }
}
