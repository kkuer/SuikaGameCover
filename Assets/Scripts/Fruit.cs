using UnityEngine;

public class Fruit : MonoBehaviour
{
    public enum fruitType
    {
        Cherry,
        Strawberry,
        Grape,
        Dekopon,
        Persimmon,
        Apple,
        Pear,
        Peach,
        Pineapple,
        Melon,
        Watermelon
    }

    public fruitType type;

    public int scoreToSend;

    public GameObject nextTier;

    private bool hasCollided = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        //check if a collision has been registered
        if (!hasCollided)
        {
            Fruit otherFruit = other.gameObject.GetComponentInParent<Fruit>();

            //check for 1 collision
            if (otherFruit != null && !otherFruit.hasCollided)
            {
                //create new fruit
                createNewFruit(other);
            }
        }
    }

    private void createNewFruit(Collision2D other)
    {
        //get fruit info
        Fruit thisFruit = this;
        Fruit otherFruit = other.gameObject.GetComponentInParent<Fruit>();

        //find point of collision
        Vector2 globalContactPoint = other.contacts[0].point;

        //check for fruit match, if fruit is already max tier
        if (thisFruit.type == otherFruit.type && thisFruit.type != fruitType.Watermelon)
        {
            Debug.Log("collided with the same fruit");

            //cancel duplicate collisions
            hasCollided = true;
            otherFruit.hasCollided = true;

            //destroy fruits
            Destroy(thisFruit.gameObject);
            Destroy(otherFruit.gameObject);

            //instantiate next tier, enable physics
            GameObject newFruit = Instantiate(nextTier, globalContactPoint, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            Rigidbody2D rb = newFruit.GetComponent<Rigidbody2D>();
            rb.simulated = true;

            //get score of fruit combination and send to game manager
            int newScore = newFruit.gameObject.GetComponentInParent<Fruit>().scoreToSend;

            if (GameManager.gameManagerInstance != null)
            {
                GameManager.gameManagerInstance.addScore(newScore);
            }
        }
        else
        {
            Debug.Log("collided with a different fruit");
        }
    }
}
