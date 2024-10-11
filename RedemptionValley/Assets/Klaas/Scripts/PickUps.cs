using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public enum ItemType {None,Ammo,Gold,SkillPoint}
public class PickUps : MonoBehaviour
{
    public static event HandleItemPickup OnPickUp;
    public delegate void HandleItemPickup(int Amount, ItemType itemType);


    [Header("Item Info")]
    public int itemAmount;
    public ItemType itemType;
    [SerializeField] private LayerMask playerLayers;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch(itemType)
            {
                case ItemType.Ammo:
                    OnPickUp?.Invoke(itemAmount, itemType);
                    Destroy(gameObject);
                    break;

                case ItemType.Gold:
                    OnPickUp?.Invoke(itemAmount, itemType);
                    Destroy(gameObject);
                    break;

                case ItemType.SkillPoint:
                    OnPickUp?.Invoke(itemAmount, itemType);
                    Destroy(gameObject);
                    break;

                case ItemType.None:
                    Debug.LogWarning("No ItemType Has Been Selected");
                    break;

                default:
                    Debug.LogWarning("Something Went Wrong");
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
