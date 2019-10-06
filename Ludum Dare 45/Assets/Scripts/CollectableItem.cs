using PurpleCable;
using UnityEngine;

public class CollectableItem : Item
{
    private ItemDef _ItemDef;
    public ItemDef ItemDef { get => _ItemDef; set { _ItemDef = value; GetComponent<SpriteRenderer>().sprite = _ItemDef?.DisplayImage; } }

    protected override void OnPickup(Collider2D collision)
    {
        Inventory.AddItem(ItemDef);

        ScoreManager.AddPoints(1);
    }

}
