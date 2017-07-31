using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseGun
{

    public bool isRealoading;

    public string Name;
    public float damage;
    public float range;
    public float fireRate;
    public float velocity;
    public int spreadCount = 1;
    public float shootDelay;
    public float currentShootDelay;
    public int magazineSize = 30;
    public int currentMagazine;
    public float reloadTime;
    public float currentReloadTime;
    public bool fullReload;
    public GunController controller;

    public BaseGun()
    {

    }

    public BaseGun(string name, float damage, float range, float fireRate, float velocity, int spreadCount, int magazineSize, float reloadTime, bool fullReload)
    {
        this.Name = name;
        this.damage = damage;
        this.range = range;
        this.fireRate = fireRate;
        this.velocity = velocity;
        this.spreadCount = spreadCount;
        this.magazineSize = magazineSize;
        this.reloadTime = reloadTime;
        this.fullReload = fullReload;
        currentMagazine = magazineSize;
    }




    public IEnumerator Reload()
    {
        if (controller == null && controller.isShooting)
        {
            yield return null;
        }
        isRealoading = true;
        yield return new WaitForSeconds(reloadTime * ((float)(magazineSize - currentMagazine) / (float)magazineSize));
        if (controller.player.CurrentPower >= (magazineSize - currentMagazine) * damage / 5)
        {
            controller.player.CurrentPower -= (magazineSize - currentMagazine) * damage / 5;
            currentMagazine = magazineSize;
        }
        else
        {
            int ammo = Mathf.FloorToInt(controller.player.CurrentPower / (damage / 5));
            controller.player.CurrentPower -= ammo * (damage / 5);
            currentMagazine += ammo;
        }
        isRealoading = false;
        yield return null;
    }

    public static BaseGun GetRifle(float minValue, int wave)
    {
        BaseGun gun = new BaseGun();
        float val = 0;
        string name;
        for (int i = 0; i < 10; i++)
        {
            float damage = 25 * Random.Range(0.6f, 1.2f),
                    range = 15 * Random.Range(0.6f, 1.2f),
                    fireRate = 400 * Random.Range(0.6f, 1.2f),
                    velocity = 30 * Random.Range(0.6f, 1.2f),
                    magazineSize = 30 * Random.Range(0.8f, 1.2f),
                    reloadTime = 3 * Random.Range(0.8f, 1.4f);
            float damageMax = 25 * 1.2f,
                    rangeMax = 15 * 1.2f,
                    fireRateMax = 400 * 1.2f,
                    velocityMax = 30 * 1.2f,
                    magazineSizeMax = 30 * 1.2f;
            float nom = damage * 2 + range + fireRate / 10 + velocity + magazineSize * 1.5f;
            float max = damageMax * 2 + rangeMax + fireRateMax / 10 + velocityMax + magazineSizeMax * 1.5f;
            if (nom / max >= minValue)
            {
                name = GetRarity(nom, max) + " Rifle";
                gun = new BaseGun(name, Linear(damage, damage / 100, wave + 1), range, fireRate, velocity, 1, Mathf.RoundToInt(magazineSize), reloadTime, true);
                break;
            }
            else if (val < nom / max)
            {
                val = nom / max;
                name = GetRarity(nom, max) + " Rifle";
                gun = new BaseGun(name, Linear(damage, damage / 100, wave + 1), range, fireRate, velocity, 1, Mathf.RoundToInt(magazineSize), reloadTime, true);
            }
        }


        return gun;
    }
    public static BaseGun GetShotgun(float minValue, int wave)
    {
        BaseGun gun = new BaseGun();
        float val = 0;
        string name;
        for (int i = 0; i < 10; i++)
        {
            float damage = 55 * Random.Range(0.6f, 1.2f),
            range = 10 * Random.Range(0.6f, 1.2f),
            fireRate = 70 * Random.Range(0.6f, 1.2f),
            velocity = 15 * Random.Range(0.6f, 1.2f),
            magazineSize = 8 * Random.Range(0.8f, 1.2f),
            reloadTime = 6 * Random.Range(0.8f, 1.4f);
            float damageMax = 55 * 1.2f,
                rangeMax = 10 * 1.2f,
                fireRateMax = 70 * 1.2f,
                velocityMax = 15 * 1.2f,
                magazineSizeMax = 8 * 1.2f;
            int spread = Random.Range(2, 5);
            float nom = damage * 2 + range + fireRate / 10 + velocity + magazineSize * 1.5f;
            float max = damageMax * 2 + rangeMax + fireRateMax / 10 + velocityMax + magazineSizeMax * 1.5f;
            if (nom / max >= minValue)
            {
                name = GetRarity(nom, max) + " Shotgun";
                gun = new BaseGun(name, Linear(damage, damage / 100, wave + 1), range, fireRate, velocity, spread, Mathf.RoundToInt(magazineSize), reloadTime, true);
                break;
            }
            else if (val < nom / max)
            {
                val = nom / max;
                name = GetRarity(nom, max) + " Shotgun";
                gun = new BaseGun(name, Linear(damage, damage / 100, wave + 1), range, fireRate, velocity, spread, Mathf.RoundToInt(magazineSize), reloadTime, true);
            }
        }


        return gun;
    }
    public static BaseGun GetPistol(float minValue, int wave)
    {
        BaseGun gun = new BaseGun();
        float val = 0;
        string name;
        for (int i = 0; i < 10; i++)
        {
            float damage = 15 * Random.Range(0.6f, 1.2f),
                    range = 12 * Random.Range(0.6f, 1.2f),
                    fireRate = 300 * Random.Range(0.6f, 1.2f),
                    velocity = 20 * Random.Range(0.6f, 1.2f),
                    magazineSize = 16 * Random.Range(0.8f, 1.2f),
                    reloadTime = 1.5f * Random.Range(0.8f, 1.4f);
            float damageMax = 15 * 1.2f,
                    rangeMax = 12 * 1.2f,
                    fireRateMax = 300 * 1.2f,
                    velocityMax = 20 * 1.2f,
                    magazineSizeMax = 16 * 1.2f;
            float nom = damage * 2 + range + fireRate / 10 + velocity + magazineSize * 1.5f;
            float max = damageMax * 2 + rangeMax + fireRateMax / 10 + velocityMax + magazineSizeMax * 1.5f;
            if (nom / max >= minValue)
            {
                name = GetRarity(nom, max) + " Pistol";
                gun = new BaseGun(name, Linear(damage, damage / 100, wave + 1), range, fireRate, velocity, 1, Mathf.RoundToInt(magazineSize), reloadTime, true);
                break;
            }
            else if (val < nom / max)
            {
                val = nom / max;
                name = GetRarity(nom, max) + " Pistol";
                gun = new BaseGun(name, Linear(damage, damage / 100, wave + 1), range, fireRate, velocity, 1, Mathf.RoundToInt(magazineSize), reloadTime, true);
            }
        }


        return gun;
    }
    public static BaseGun GetSniper(float minValue, int wave)
    {
        BaseGun gun = new BaseGun();
        float val = 0;
        string name;
        for (int i = 0; i < 10; i++)
        {
            float damage = 60 * Random.Range(0.6f, 1.2f),
            range = 20 * Random.Range(0.6f, 1.2f),
            fireRate = 80 * Random.Range(0.6f, 1.2f),
            velocity = 40 * Random.Range(0.6f, 1.2f),
            magazineSize = 5 * Random.Range(0.8f, 1.2f),
            reloadTime = 8 * Random.Range(0.8f, 1.4f);
            float damageMax = 60 * 1.2f,
                rangeMax = 20 * 1.2f,
                fireRateMax = 80 * 1.2f,
                velocityMax = 40 * 1.2f,
                magazineSizeMax = 8 * 1.2f;
            float nom = damage * 2 + range + fireRate / 10 + velocity + magazineSize * 1.5f;
            float max = damageMax * 2 + rangeMax + fireRateMax / 10 + velocityMax + magazineSizeMax * 1.5f;
            if (nom / max >= minValue)
            {
                name = GetRarity(nom, max) + " Sniper";
                gun = new BaseGun(name, Linear(damage, damage / 100, wave + 1), range, fireRate, velocity, 1, Mathf.RoundToInt(magazineSize), reloadTime, true);
                break;
            }
            else if (val < nom / max)
            {
                val = nom / max;
                name = GetRarity(nom, max) + " Sniper";
                gun = new BaseGun(name, Linear(damage, damage / 100, wave + 1), range, fireRate, velocity, 1, Mathf.RoundToInt(magazineSize), reloadTime, true);
            }
        }


        return gun;
    }

    static float Linear(float a, float b, float c)
    {
        return a + b * c;
    }

    public static string GetRarity(float num, float max)
    {
        float percent = num / max;
        if (percent <= 0.6f)
        {
            return "Common";
        }
        else if (percent <= 0.8f)
        {
            return "Uncommon";
        }
        else if (percent <= 0.9f)
        {
            return "Normal";
        }
        else if (percent <= 0.97f)
        {
            return "Rare";
        }
        else if (percent <= 1f)
        {
            return "Legendary";
        }
        else
        {
            return "";
        }
    }



}
