using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;

    private Player _player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.GetComponent<Player>();

            if (_player != null)
            {
                UIManager.Instance.OpenShop(_player.diamonds);
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        switch (item)
        {
            case 0: //flame sword
                UIManager.Instance.UpdateShopSelection(77);
                currentItemCost = 200;
                currentSelectedItem = 0;
                break;
            case 1: //boots of flight
                UIManager.Instance.UpdateShopSelection(-36);
                currentItemCost = 400;
                currentSelectedItem = 1;
                break;
            case 3: //key
                UIManager.Instance.UpdateShopSelection(-147);
                currentItemCost = 100;
                currentSelectedItem = 2;
                break;
            default:
                break;
        }
    }

    public void BuyItem()
    {
        if (_player.diamonds >= currentItemCost)
        {
            if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            _player.diamonds -= currentItemCost;
            shopPanel.SetActive(false);
        }
        else 
        {
            shopPanel.SetActive(false);
        }
    }
}
