using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    int objectsNumber;
    [SerializeField] GameObject[] foods = new GameObject[0];
    [SerializeField] GameObject[] unfreshFoods = new GameObject[0];
    [SerializeField] GameObject[] scraps = new GameObject[0];

    private void Awake()
    {
        objectsNumber = StaticConfiguration.staticLevelObjectsNumber;
    }
    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        GameObject go = GameObject.Find("Objetos");

        for (int i = 0; i < objectsNumber; i++)
        {
            GameObject foodGameObject = Instantiate(foods[Random.Range(0, foods.Length)], DrawAxes(), Quaternion.identity);
            foodGameObject.transform.parent = go.transform;
            yield return new WaitForSeconds(0.01f);

            GameObject unfreshFoodGameObject = Instantiate(unfreshFoods[Random.Range(0, unfreshFoods.Length)], DrawAxes(), Quaternion.identity);
            unfreshFoodGameObject.transform.parent = go.transform;
            yield return new WaitForSeconds(0.01f);

            GameObject scrapGameObject = Instantiate(scraps[Random.Range(0, scraps.Length)], DrawAxes(), Quaternion.identity);
            scrapGameObject.transform.parent = go.transform;
            yield return new WaitForSeconds(0.01f);
        }
    }

    Vector3 DrawAxes()
    {
        var xAxis = Random.Range(-0.4f, 0.4f);
        var zAxis = Random.Range(-0.9f, 0.9f);
        var yAxis = 1.6f;

        Vector3 position = new Vector3(xAxis, yAxis, zAxis);
        return position;
    }



}
