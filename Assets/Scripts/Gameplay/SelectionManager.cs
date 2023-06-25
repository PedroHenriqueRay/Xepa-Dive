using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] int slotIndex = 0;
    [SerializeField] GameObject[] slots;
    [SerializeField] GameObject selectedObject;
    [SerializeField] GameObject[] slotedObjects = new GameObject[5];
    [SerializeField] bool isMoving;

    ScoreManager scoreManager;
    AudioManager audioManager;

    private void Awake()
    {
        scoreManager = GameObject.Find("LevelManager").GetComponent<ScoreManager>();
        audioManager = GameObject.Find("LevelManager").GetComponent<AudioManager>();
        isMoving = false;
    }
    private void Start()
    {
        //GameObject go = GameObject.Find("Objetos");
        //Debug.Log(go.name + " has " + go.transform.childCount + " children");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) &&!isMoving)
            {
                selectedObject = hit.collider.gameObject;
               
                if(slotIndex <= slots.Length - 1)
                {
                    //Slots possuem espaço
                    PrepareMoveAtoB(selectedObject);
                }
                else
                {
                    //Slots estão cheios
                    print("Full Slots");
                }
            }
        }
    }

    void PrepareMoveAtoB(GameObject selectedObject)
    {
        isMoving = true;
        slotedObjects[slotIndex] = selectedObject;
        slotIndex += 1;

        var selectedObjectRigidBody = selectedObject.GetComponent<Rigidbody>();
        selectedObjectRigidBody.isKinematic = true;
        //selectedObject.transform.rotation = Quaternion.identity;
        selectedObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        selectedObject.layer = 2;
        Vector3 position = slots[slotIndex - 1].transform.position;

        StartCoroutine(MoveAtoB(selectedObject, position, 7.0f));
    }

    IEnumerator MoveAtoB(GameObject gameObjectA, Vector3 position, float speedTranslation)
    {
        while (gameObjectA.transform.position != position)
        {
            gameObjectA.transform.position = Vector3.MoveTowards(gameObjectA.transform.position, position, speedTranslation * Time.deltaTime);
            yield return null;

        }
        CheckObjectsTag(gameObjectA);
    }

    void CheckObjectsTag(GameObject selectedObject)
    {
        if (slotIndex >= 2)
        {
            if (selectedObject.tag != slotedObjects[0].tag)
                ReturnWrongObject(selectedObject);
            if (slotIndex == slots.Length)
                ConvertSlotedObjects();
        }
        isMoving = false;
    }

    void ConvertSlotedObjects()
    {
        scoreManager.Soma(slotedObjects[0].tag);
        audioManager.playSuccessCombinationAudioClip();
        DestroyObjects();
    }

    void ReturnWrongObject(GameObject selectedObject)
    {
        var selectedObjectRigidBody = selectedObject.GetComponent<Rigidbody>();
        selectedObjectRigidBody.isKinematic = false;
        selectedObject.layer = 6;
        slotIndex -= 1;
        scoreManager.resetMultiplier();
        audioManager.playWrongCombinationAudioClip();
        //StartCoroutine(MoveAtoB(selectedObject, DrawAxes(), 1.0f));
        selectedObjectRigidBody.AddForce(selectedObject.transform.forward * 5.0f, ForceMode.Impulse);
    }

    void DestroyObjects()
    {
        foreach(GameObject objet in slotedObjects)
        {
            Destroy(objet);
        }
        slotIndex = 0;
        scoreManager.levelCompleteCheck();
    }

    Vector3 DrawAxes()
    {
        var xAxis = Random.Range(-1.0f, 1.0f);
        var zAxis = Random.Range(-1.0f, 3.0f);
        var yAxis = Random.Range(0, 3);

        Vector3 position = new Vector3(xAxis, yAxis, zAxis);
        return position;
    }
}
