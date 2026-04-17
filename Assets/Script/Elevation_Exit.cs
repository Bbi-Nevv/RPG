using UnityEngine;

public class Elevation_Exit : MonoBehaviour
{
    public Collider2D[] mountains;
    public Collider2D[] mountbins;
    void Start()
    {
        foreach (var mountbin in mountbins)
        {
            mountbin.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var mountain in mountains)
            {
                mountain.enabled = true;
            }
            foreach (var mountbin in mountbins)
            {
                mountbin.enabled = false;
            }
            collision.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
