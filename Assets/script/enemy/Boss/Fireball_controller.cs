using System.Collections;
using UnityEngine;
 public class Fireball_controller : MonoBehaviour
 {
    public CharactState _Stat;
    public float speed = 4;
    public float time = 4;
    public float moveright;
    public void Update()
    {
        time -= Time.deltaTime;
        float movedistance = speed * Time.deltaTime;
        transform.position += transform.right*moveright * movedistance;;
        if (time < 0)
            Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            _Stat.Dodamage(PlayerManager.instance.player.charactState, transform);
            Destroy(gameObject);
        }
    }
    public void SetFireBall(float _moveRight,CharactState _stat)
    {
        _Stat = _stat;
        moveright = _moveRight;
    }
}
