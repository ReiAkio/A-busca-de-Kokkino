using UnityEngine;

public class Item : MonoBehaviour
{
    public KeyObject key_scpobjt;
    public InventoryObject inventory;

    private void Awake()
    {
        gameObject.SetActive(!key_scpobjt.wasCollected);
        gameObject.GetComponent<SpriteRenderer>().sprite = key_scpobjt.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inventory.itemCollectedQuantity++;
        key_scpobjt.wasCollected = true;
        gameObject.SetActive(false);
    }
}
