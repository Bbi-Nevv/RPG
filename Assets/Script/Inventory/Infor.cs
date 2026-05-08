using UnityEngine;

public class Infor : MonoBehaviour
{
    public ScriptableObject item;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ManagerInventory.instance.AddItem(item as Items);
            Destroy(gameObject);
        }
    }
}
