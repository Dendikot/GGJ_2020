using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinePart : MonoBehaviour
{
    public enum MachineType
    {
        WaterFilter,
        CoalProcessor,
        ProcessingUnit,
        Valves,
        Transmisson,
        Tunnel,
        Engine
    }

    [SerializeField]
    public MachineType _type;
    [SerializeField]
    //[HideInInspector]
    public float _health;
    [Range(0f, 100f)]
    public float damageChance;
    [Range(0,1)]
    public float healthDropRate;
    [Range(0, 1)]
    public float healthIncreaseRate;

    public bool debugPressButton = false;
    private bool isAvatarColliding = false;

    void Start()
    {
        _health = 100;
       
    }

    void FixedUpdate()
    {
        HealthDrop(damageChance);
        if (isAvatarColliding)
        {
            Repair(0, 0, debugPressButton);
        }
    }

    public void HealthDrop(float dropChance)
    {
        float _chance = Random.Range(0,100);
        if (_chance < dropChance)
        {
            //Going to get a drop on health
            _health -= healthDropRate;

        }
    }

    public void Repair( int playerID, int buttonDeviceID, bool isRepairButtonPressed)
    {
        if (playerID == buttonDeviceID && isRepairButtonPressed)
        {
            if (_health < 100)
            {
                _health += healthIncreaseRate;
            }                 
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //There is no Player script yet
        //int playerID = other.gameObject.GetComponent<Player>().playerID;
        if (collision.CompareTag("Avatar"))
        {
            healthIncreaseRate = 0.5f;
            isAvatarColliding = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Avatar"))
        {
            healthIncreaseRate = 0;
            isAvatarColliding = false;
        }
    }
}
