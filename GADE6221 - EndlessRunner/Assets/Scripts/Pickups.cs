using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] PickupType pickupType;

    [Header("Player")]
    //[SerializeField] MovementSystem movementSystem;
    
    [Header("Score Multiplier")]
    //ScoreCounter scoreCounter;
    //[SerializeField] float multiplierIncreaseAmount = 0.5f;
    [SerializeField] float multiplierAmount = 2f;
    [SerializeField] float multiplierActivationLength = 2f;

    [Header("JumpBoost")]
    [SerializeField] float JumpBoostAmount;

    bool multiplying = false;

    private void Awake()
    {
        GameObject gameControl = GameObject.Find("GameControl");
        //scoreCounter = gameControl.GetComponent<ScoreCounter>();
    }
    public enum PickupType 
    {
        ScoreMultiplier, 
        JumpBoost,
        SpeedBoost,
        Immunity,
        Health
    };

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Destroy(this.gameObject.transform.GetChild(0).gameObject);
            if(pickupType == PickupType.ScoreMultiplier)
            {
                if(multiplying)
                {
                    StopCoroutine(EndScoreMultiplier());
                    //Debug.Log("Routine Stop");
                    multiplying = false;
                }

                ScoreMultiplier();
                // Invoke("EndScoreMultiplier", 5f);
                StartCoroutine(EndScoreMultiplier());
            }
            else if (pickupType == PickupType.JumpBoost)
            {
                JumpBoost();
            }
            else if (pickupType == PickupType.SpeedBoost)
            {
                SpeedBoost();
            }
            else if (pickupType == PickupType.Immunity)
            {
                Immunity();
            }
            else if (pickupType == PickupType.Health)
            {
                Health();
            }
        }
    }

    void ScoreMultiplier()
    {
        //scoreCounter.multiplier =  multiplierAmount;
    }

    IEnumerator EndScoreMultiplier()
    {
        multiplying = true;
        //Debug.Log("Routine started");
        yield return new WaitForSeconds(multiplierActivationLength);
        //Debug.Log("Routine done");
        //scoreCounter.multiplier = 1f;
        multiplying = false;    
    }

    void JumpBoost()
    {
        //Not yet implemented
    }
    void SpeedBoost()
    {

    }
    void Immunity()
    {

    }
    void Health()
    {

    }

   
}
