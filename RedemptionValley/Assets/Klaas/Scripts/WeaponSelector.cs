using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour
{
    public GameObject[] weapons;
    [SerializeField] private string playerName;
    [SerializeField] private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        Time.timeScale = 0;
    }
    public void AddWeaponToPlayer(int weaponNum)
    { 
        GameObject weapon = Instantiate(weapons[weaponNum], GameObject.FindGameObjectWithTag(playerName).transform.position, Quaternion.identity);
        weapon.transform.SetParent(GameObject.FindGameObjectWithTag(playerName).transform);
        Time.timeScale = 1;
        gameManager.hasWeaponBeenSelected = true;
        Destroy(gameObject);
    }

}
