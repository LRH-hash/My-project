using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject itemPrefab;
    public itemData item;
    public itemData[] PossileDrop;
    public int amountofCount;
    public List<itemData> DropList;
    // Start is called before the first frame update
    void Start()
    {
        DropList = new List<itemData>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void GenerateDrop()
    {
        for(int i=0;i<PossileDrop.Length;i++)
        {
           /* if (Random.Range(0, 100) <=PossileDrop[i].dropchance)*/
           //����������ܳ���List�ĸ���������amountfCOUNT�����
                DropList.Add(PossileDrop[i]);
        }
        for(int i=amountofCount-1;i>=0;i--)
        {
            if (DropList == null)
                break;
            int list = Random.Range(0, DropList.Count - 1);
            DropItem(DropList[list]);
            DropList.Remove(DropList[list]);
        }
    }
    public void DropItem(itemData _item)
    {
        GameObject go = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        Vector2 RandomVelocity = new Vector2(Random.Range(0, 5), Random.Range(5, 10));
        go.GetComponent<ItemObject>().Setitem(_item, RandomVelocity);
    }
}
