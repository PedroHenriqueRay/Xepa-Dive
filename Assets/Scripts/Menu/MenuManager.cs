using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Change Fov Settings")]
    [SerializeField] Camera mainCamera;
    [SerializeField] float transitionDuration;
    [SerializeField] float initialFOV;
    bool isTransitioning = false;
    float targetFOV;

    [Header("World Rotation Settings")]
    [SerializeField] GameObject world;
    [SerializeField] float rotationSpeed;
    bool isRotating = false;
    Vector3 initialMousePosition;

    [Header("Canvas Panels")]
    [SerializeField] GameObject btnBack;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject levelSelectionPanel;

    int menuStageIndex;
    GameObject selectedGameObject;
    LevelSelectionManager levelSelectionManager;

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        rotationSpeed = 10;
#endif
        menuStageIndex = 0;
    }

    private void Start()
    {
        if (StaticConfiguration.staticIsReturningFromGameplay)
        {
            mainCamera.fieldOfView = 32.5f;
            menuStageIndex = 1;
        }
    }

    void Update()
    {
        switch (menuStageIndex)
        {
            case 0:
                btnBack.SetActive(false);
                mainMenuPanel.SetActive(true);
                levelSelectionPanel.SetActive(false);

                world.transform.Rotate(Vector3.up, 5f * Time.deltaTime);

                if (Input.GetMouseButtonDown(0) && !isTransitioning)
                {
                    StartCoroutine(TransitionFOV(32.5f, transitionDuration));
                    menuStageIndex = 1;
                }

                break;
            case 1:
                btnBack.SetActive(true);
                mainMenuPanel.SetActive(false);
                levelSelectionPanel.SetActive(false);
                WorldPin();
                break;
            case 2:
                btnBack.SetActive(true);
                mainMenuPanel.SetActive(false);
                levelSelectionPanel.SetActive(true);
                break;
            default:
                print("Incorrect Tag");
                break;
        }
    }

    void WorldPin()
    {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    selectedGameObject = hit.collider.gameObject;
                    setLevelProperties(selectedGameObject);
                    StartCoroutine(TransitionFOV(20f, transitionDuration));
            }
                else
                {
                    isRotating = true;
                    initialMousePosition = Input.mousePosition;
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isRotating = false;
            }

            if (isRotating)
            {
                Vector3 currentMousePosition = Input.mousePosition;
                float rotationAmountX = (currentMousePosition.x - initialMousePosition.x) * rotationSpeed * Time.deltaTime;
                float rotationAmountY = (currentMousePosition.y - initialMousePosition.y) * rotationSpeed * Time.deltaTime;

                world.transform.Rotate(Vector3.up, -rotationAmountX, Space.World);
                world.transform.Rotate(Vector3.right, rotationAmountY, Space.World);

                initialMousePosition = currentMousePosition;
            }
    }

    public System.Collections.IEnumerator TransitionFOV(float targetFOV, float duration)
    {
        isTransitioning = true;

        float elapsedTime = 0f;
        float initialCameraFOV = mainCamera.fieldOfView;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            mainCamera.fieldOfView = Mathf.Lerp(initialCameraFOV, targetFOV, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
        isTransitioning = false;
    }

    void setLevelProperties(GameObject gameObject)
    {
        levelSelectionManager = gameObject.GetComponent<LevelSelectionManager>();
        StaticConfiguration.staticLevel = levelSelectionManager.level;
        StaticConfiguration.staticLevelTitle = levelSelectionManager.levelTitle;
        StaticConfiguration.staticLevelDescription = levelSelectionManager.levelDescription;
        StaticConfiguration.staticLevelGoalDescription = levelSelectionManager.levelGoalDescription;
        StaticConfiguration.staticLevelGoal = levelSelectionManager.levelGoal;
        StaticConfiguration.staticLevelObjectsNumber = levelSelectionManager.levelObjectsNumber;
        StaticConfiguration.staticLevelTimeLimit = levelSelectionManager.levelTimeLimit;
        menuStageIndex = 2;
    }
    public void BackButton()
    {
        if (!isTransitioning)
        {
            menuStageIndex -= 1;
            switch (menuStageIndex)
            {
                case 0:
                    StartCoroutine(TransitionFOV(60f, transitionDuration));
                    break;
                case 1:
                    StartCoroutine(TransitionFOV(32.5f, transitionDuration));
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("GameplayScene");
    }
}
