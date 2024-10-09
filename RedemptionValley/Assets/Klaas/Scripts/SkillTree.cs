using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class SkillTree : MonoBehaviour
{
    [Header("Player")]
    public PlayerController playerController;

    [Header("SKill's Bought")]
    public int currentSkillAmount = 0;

    [Header("Buttons")]
    [SerializeField] private Button[] skillButtons;

    /*
     * to be able to upgrade a skill the currentskillamount must exceed the skill requirement of the skill the player want to buy and have enough skill points
     */

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradeMaxHealth(int cost)
    {
      //  int skillRequirement = Int32.Parse(RequirementAndCost.Split(',')[0]);
      //  int cost = Int32.Parse(RequirementAndCost.Split(',')[1]);

      // Debug.Log($"Skill Req={skillRequirement} - Cost={cost}");

        if(playerController.playerSkillPoints >= cost)
        {
            playerController.playerHealthBar.maxValue += 50;
            currentSkillAmount++;
            Debug.Log("Skill Bought");
        } 

    }
}
