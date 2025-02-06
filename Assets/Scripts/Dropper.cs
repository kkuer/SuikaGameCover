using UnityEngine;
using System.Collections.Generic;

public class Dropper : MonoBehaviour
{
    //x distance parameters
    public float minX;
    public float maxX;

    //fruits
    public Transform fruitSpawnPos;

    private GameObject currentFruit;

    public List<GameObject> droppableFruits;

    //get random fruit
    public GameObject getRandomFruit()
    {
        //select random index
        int randomFruitIndex = Random.Range(0, droppableFruits.Count);

        //return new fruit
        return droppableFruits[randomFruitIndex];
    }

    void Start()
    {
        SpawnFruit();
    }

    void Update()
    {
        //get mouse pos
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //clamp x parameters
        float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);

        //move dropper
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        if (currentFruit)
        {
            currentFruit.gameObject.transform.position = fruitSpawnPos.position;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DropFruit(currentFruit);
        }
    }

    private void DropFruit(GameObject fruit)
    {
        //find current fruit
        fruit = currentFruit.gameObject;

        //check fruit spawn
        if (fruit != null)
        {
            //enable fruit rb
            Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
            rb.simulated = true;

            //select new fruit
            SpawnFruit();
        }


    }

    private void SpawnFruit()
    {
        //get new fruit and instantiate
        currentFruit = Instantiate(getRandomFruit(), fruitSpawnPos.position, Quaternion.identity);

        //turn off fruit rb
        Rigidbody2D rb = currentFruit.GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }
}
