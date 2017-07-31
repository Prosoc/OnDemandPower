using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractableUI : MonoBehaviour
{

    public PlayerController player;
    public Text Name;
    public Text Text;
    Image Renderer;
    Color baseColor;
    // Use this for initialization
    void Start()
    {
        Renderer = transform.GetComponent<Image>();
        baseColor = Renderer.color;
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.inInteractable)
        {
            Renderer.color = baseColor;
            Text.color = Color.black;
            Name.color = Color.black;
            Text.text = player.interactable.text;
            Name.text = player.interactable.gun.Name;
            switch (Name.text.Split(' ')[0])
            {
                case "Common":
                    {
                        Name.color = Color.gray;
                        break;
                    }
                case "Uncommon":
                    {
                        Name.color = new Color32(230, 230, 230, 255);
                        break;
                    }
                case "Normal":
                    {
                        Name.color = Color.black;
                        break;
                    }
                case "Rare":
                    {
                        Name.color = new Color32(40, 40, 240, 255);
                        break;
                    }
                case "Legendary":
                    {
                        Name.color = new Color32(255, 140, 0, 255);
                        break;
                    }
                default:
                    break;
            }
        }
        else if (player.inShop)
        {
            Renderer.color = baseColor;
            if (player.Shop.BuyArmor)
            {
                Text.color = Color.black;
                Text.text = player.Shop.text;
            }
            else if (player.Shop.BuyPower)
            {
                Text.color = Color.black;
                Text.text = player.Shop.text;
            }
            else
            {
                Text.color = Color.black;
                Name.color = Color.black;
                Text.text = player.Shop.text;
                Name.text = player.Shop.g.Name;
                switch (player.Shop.g.Name.Split(' ')[0])
                {
                    case "Common":
                        {
                            Name.color = Color.gray;
                            break;
                        }
                    case "Uncommon":
                        {
                            Name.color = new Color32(230, 230, 230, 255);
                            break;
                        }
                    case "Normal":
                        {
                            Name.color = Color.black;
                            break;
                        }
                    case "Rare":
                        {
                            Name.color = new Color32(40, 40, 240, 255);
                            break;
                        }
                    case "Legendary":
                        {
                            Name.color = new Color32(255, 140, 0, 255);
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        else
        {
            Renderer.color = new Color(0, 0, 0, 0);
            Text.color = new Color(0, 0, 0, 0);
            Name.color = new Color(0, 0, 0, 0);
        }
    }
}
