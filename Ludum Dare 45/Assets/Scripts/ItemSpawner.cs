using PurpleCable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private CollectableItem ItemPrefab = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var item = Instantiate(ItemPrefab, transform);
            item.ItemDef = Inventory.GetRandomItem();
        }
    }
}
