using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] public int amount = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if(player != null)
            {
                player.UpdateDiamonds(amount);
                Destroy(this.gameObject);
            }
        }
    }

    public void UpdateDiamondValue(int newAmount)
    {
        amount = newAmount;
        Debug.Log("After update: " + amount);
    }

}
