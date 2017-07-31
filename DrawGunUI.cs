using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DrawGunUI : MonoBehaviour {

    public PlayerController player;
    public Image AmmoFill;
    public Text AmmoText;
    public Text Name;
    public Text Details;
    float t = 0;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        BaseGun g = player.gunController.currentGun;
        if (g.isRealoading)
        {
            float timeToFullReaload = g.reloadTime * ((float)((g.magazineSize - g.currentMagazine)) / (float)g.magazineSize);
            if (t <= timeToFullReaload)
            {
                t += Time.deltaTime;
                AmmoFill.fillAmount = Mathf.Lerp((float)g.currentMagazine / (float)g.magazineSize, 1, t / timeToFullReaload);
            }
            else
            {
                AmmoFill.fillAmount = 1;
                t = 0f;
            }
        }
        else
        {
            AmmoFill.fillAmount = (float)g.currentMagazine / (float)g.magazineSize;
        }
        Name.text = g.Name;
        Details.text = "Damage: " + Mathf.RoundToInt(g.damage) + "\n" + "Fire rate: " + Mathf.RoundToInt(g.fireRate) + "\n" + "Range: " + Mathf.RoundToInt(g.range);
        AmmoText.text = g.currentMagazine.ToString();

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
}
