using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dropper : MonoBehaviour
{
    //x distance parameters
    public float minX;
    public float maxX;

    //fruits
    public Transform fruitSpawnPos;
    public Transform nextFruitPos;

    [SerializeField] private float spawnDelay;

    private GameObject currentFruit;
    private GameObject nextFruit;

    public List<GameObject> droppableFruits;

    public GameObject getRandomFruit()
    {
        //select random index
        int randomFruitIndex = Random.Range(0, droppableFruits.Count);

        //return new fruit
        return droppableFruits[randomFruitIndex];
    }

    void Start()
    {
        //load next fruit, move it to dropper position, then spawn a new next fruit
        loadNextFruit();
        spawnFruit();
        loadNextFruit();
    }

    void Update()
    {
        if (GameManager.gameManagerInstance != null)
        {
            //only run logic if game is active
            if (GameManager.gameManagerInstance.gameActive == true)
            {
                //get mouse pos
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //clamp x parameters
                float clampedX = Mathf.Clamp(mousePosition.x, minX, maxX);

                //move dropper
                transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
            }

            if (currentFruit)
            {
                currentFruit.gameObject.transform.position = fruitSpawnPos.position;
            }

            if (Input.GetMouseButtonDown(0) && currentFruit != null)
            {
                //drop fruit
                StartCoroutine(dropFruit(currentFruit));
            }
        }
    }

    IEnumerator dropFruit(GameObject fruit)
    {
        //find current fruit
        fruit = currentFruit.gameObject;

        //check fruit spawn
        if (fruit != null)
        {
            Debug.Log("pass1");
            //enable fruit rb
            Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
            Debug.Log(rb);
            rb.simulated = true;
            Debug.Log(rb.simulated);
            rb.angularVelocity = Random.Range(-90f, 90f);
            Debug.Log(rb.angularVelocity);

            //set current fruit to null
            currentFruit = null;

            //select a new fruit with buffer delay
            yield return new WaitForSeconds(spawnDelay);
            spawnFruit();
            loadNextFruit();
        }
    }

    private void spawnFruit()
    {
        //reassign next fruit to get ready to drop
        currentFruit = nextFruit;
    }

    private void loadNextFruit()
    {
        //get new fruit and instantiate
        nextFruit = Instantiate(getRandomFruit(), nextFruitPos.position, Quaternion.identity);

        //turn off fruit rb
        Rigidbody2D rb = nextFruit.GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }
}
