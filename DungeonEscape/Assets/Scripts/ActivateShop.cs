using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShop : MonoBehaviour
{
    [SerializeField] GameObject _shopPanel;
    public int _currentSelectedItem;
    public int _currentItemCost;
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
            if(player != null)
            {
                UIManager.Instance.OpenShop(player._diamonds);
            }
            _shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _shopPanel.SetActive(false);
            UIManager.Instance.TurnOffSelection();
        }
    }

    public void SelectItem(int item)
    {
        switch (item)
        {
            case 0: UIManager.Instance.UpdateShopSelection(72);
                _currentSelectedItem = 0;
                _currentItemCost = 200;
                break;
            case 1: UIManager.Instance.UpdateShopSelection(-40);
                _currentSelectedItem = 1;
                _currentItemCost = 400;
                break;
            case 2: UIManager.Instance.UpdateShopSelection(-141);
                _currentSelectedItem = 2;
                _currentItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
       if(player._diamonds >= _currentItemCost)
        {
            if(_currentSelectedItem == 2)
            {
                //updates the game manager to let it know that the player has bought the key
                GameManager.Instance._hasCastleKey = true;
            }
            Debug.Log("Award item");
            //Take away the gems from the player after they have bought an item
            player.MinusDiamonds(_currentItemCost);
            //Updates the gems in the shop UI
            UIManager.Instance.OpenShop(player._diamonds);
        }
        else
        {
            Debug.Log("You do not have enough gems to but that");
            _shopPanel.SetActive(false);
        }

    }


}
