using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder_Controller : MonoBehaviour
{
    public CharactState Target;
    public Animator anim;
    public float moveSpeed = 8;
    public bool istrigget=true;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetThunder(int _damage,CharactState _Target)
    {
        Target = _Target;
        damage = _damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector2(3, 3);
            transform.position += new Vector3(0, 3.2f, 0);
            anim.SetTrigger("Hit");
            istrigget = false;
            Destroy(gameObject, .5f);
        }
        if (istrigget)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, Target.transform.position) < .3f)
            {
                transform.rotation = Quaternion.identity;
                transform.localScale = new Vector2(3, 3);
                transform.position += new Vector3(0, 3.2f, 0);
                anim.SetTrigger("Hit");
                Invoke("Attack", .2f);
                istrigget = false;
                Destroy(gameObject, .5f);
            }
        }
    }
    public void Attack()
    {
        if (Target == null)
            return;
        Target.ApplyShock(true);
        Target.Takedamage(damage);
        Target.Fx.creatText(damage.ToString(), Color.yellow);
    }
}
