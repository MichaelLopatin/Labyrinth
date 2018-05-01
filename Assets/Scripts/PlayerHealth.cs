using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public delegate void HealthChanging(int curentHealth);
    /// <summary>
    /// HealthChangingEvent occurred every helth change.
    /// </summary>
    public event HealthChanging HealthChangingEvent;
    /// <summary>
    /// HealthZeroEvent occurred when helth==0.
    /// </summary>
    public event HealthChanging HealthZeroEvent;

    private int minHealth = 0;
    private int maxHealth = 100;
    private int curentHealth;

    private void Awake()
    {
        curentHealth = maxHealth;
    }


    public int CurentHealth
    {
        private set
        {
            if (value <= minHealth)
            {
                curentHealth = minHealth;
                if (HealthZeroEvent != null)
                {
                    HealthZeroEvent(minHealth);
                }

            }
            else if (value > maxHealth)
            {
                curentHealth = maxHealth;
            }
            else
            {
                curentHealth = value;
            }
            if (HealthChangingEvent != null)
            {
                HealthChangingEvent(curentHealth);
            }
        }
        get
        {
            return curentHealth;
        }
    }

    private void ChangeHealth(int healthChangeValue)
    {
        CurentHealth += healthChangeValue;
    }
}
