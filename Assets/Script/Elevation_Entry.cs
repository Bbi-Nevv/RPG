using UnityEngine;

public class Elevation_Entry : MonoBehaviour
{
    public Collider2D[] mountains;
    public Collider2D[] mountbins;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var mountain in mountains)
            {
                mountain.enabled = false;
            }
            foreach (var mountbin in mountbins)
            {
                mountbin.enabled = true;
            }

            collision.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}
