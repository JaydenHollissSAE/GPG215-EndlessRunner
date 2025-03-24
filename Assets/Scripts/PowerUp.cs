
// PowerUp.cs
using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static PowerUp powerUpInstance;
    //public float duration = 5f;
    private bool endingPowerup = false;
    //public string powerUpType;
    public bool powerUpActive = false;


    void Awake()
    {
        if (powerUpInstance == null)
        {
            powerUpInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.name.Contains("Player"))
    //    {
    //        Debug.Log("Hit");
    //        StartCoroutine(PickUp(other));
    //    }
    //}

    /* On collision, check if collided object is the player.
     * If so run the player pick up function
    */

    /* Check what the powerup type is.
     * Return different variable changes to the player based on the type.
     * Makes the object non interactable and invisible but not inactive to ensure the script contuinues running.
     * Waits for the duration of the powerup.
     * Return different variable changes to the player based on the type, undoing the activation step.
     * Destroys the object.
    */

    IEnumerator PickUp(Collider2D player, string powerUpType)
    {
        while(powerUpActive)
        {
            yield return new WaitForEndOfFrame();
        }
        powerUpActive = true;
        float duration = 4f;
        // Apply effect based on powerUpType
        switch (powerUpType)
        {
            case "ReducedGravity":
                Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
                rb.gravityScale /= 1.8f;
                player.gameObject.GetComponent<PlayerController>().jumpMultiplierMax -= 1f;
                duration = 10f;
                break;
            case "DoubleJump":
                //player.GetComponent<PlayerController>().speed *= 2;
                break;
            case "Strength":
                //player.GetComponent<PlayerController>().strength *= 2;
                break;
        }

        // Disable the power-up object
        //GetComponent<Renderer>().enabled = false;
        //GetComponent<Collider>().enabled = false;

        // Wait for the duration
        yield return new WaitForSeconds(duration);

        // Reverse the effect
        EndPowerUp(player.gameObject, powerUpType);

        // Destroy the power-up object
    }

    public void EndPowerUp(GameObject player, string powerUpType)
    {
        if (!endingPowerup)
        {
            endingPowerup = true;
            switch (powerUpType)
            {
                case "ReducedGravity":
                    Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
                    rb.gravityScale *= 1.8f;
                    player.gameObject.GetComponent<PlayerController>().jumpMultiplierMax += 1f;
                    break;
                case "Speed":
                    //player.GetComponent<PlayerController>().speed /= 2;
                    break;
                case "Strength":
                    //player.GetComponent<PlayerController>().strength /= 2;
                    break;
            }
            powerUpActive = false;
        
        }
    }
}

