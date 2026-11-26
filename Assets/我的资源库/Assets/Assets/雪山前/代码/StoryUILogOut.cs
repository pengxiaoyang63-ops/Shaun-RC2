using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StoryUILogOut : MonoBehaviour
{
    // Start is called before the first frame update
    public int ChapterIndex;   // 章节索引
    public int SceneIndex;     // 场景索引
    public int ClipIndex;      // 剧情片段索引
    public int NumberIndex;    // 序号索引
    public bool StoryPlay;
    public bool Nextpage;
    [SerializeField] private InputActionAsset inputActions;
    private InputActionAsset realInputActions;
    private InputAction choice1;
    private InputAction choice2;
    private InputAction choice3;
    private InputAction choice4;
    
    void Awake()
    {
        realInputActions = inputActions != null ? inputActions : Resources.Load<InputActionAsset>("GameControls");
        var storyMap = realInputActions.FindActionMap("Story");
        choice1 = storyMap.FindAction("Choice1");
        choice2 = storyMap.FindAction("Choice2");
        choice3 = storyMap.FindAction("Choice3");
        choice4 = storyMap.FindAction("Choice4");
    }
    void OnEnable() { realInputActions.Enable(); }
    void OnDisable() { realInputActions.Disable(); }
    void Start()
    {
        StoryPlay = false;
        NumberIndex = 0;
        ChapterIndex = SceneIndex = ClipIndex = 0;
    }
    void NextPage()
    {
        if (NumberIndex == 0)
        {
            NumberIndex++;
        }
        StoryRow currentRow = PlotPlayer.Instance.GetStoryRow(
            ChapterIndex, 
            SceneIndex, 
            ClipIndex, 
            NumberIndex
            );
        if (choice1.WasPressedThisFrame())
        {
            NumberIndex++;
            Nextpage=true;
            if (currentRow.Goto1 != 0)
            {
                NumberIndex = 1;
                ClipIndex = currentRow.Goto1;
            }
        }
        else if (choice2.WasPressedThisFrame())
        {
            NumberIndex++;
            Nextpage=true;
            if (currentRow.Goto2 != 0)
            {
                NumberIndex = 1;
                ClipIndex = currentRow.Goto2;
            }
        }
        else if (choice3.WasPressedThisFrame())
        {
            NumberIndex++;
            Nextpage=true;
            if (currentRow.Goto3 != 0)
            {
                NumberIndex = 1;
                ClipIndex = currentRow.Goto3;
            }
        }
        else if (choice4.WasPressedThisFrame())
        {
            NumberIndex++;
            Nextpage=true;
            if (currentRow.Goto4 != 0)
            {
                NumberIndex = 1;
                ClipIndex = currentRow.Goto4;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (StoryPlay)
        {
            NextPage();
            StoryRow currentRow = PlotPlayer.Instance.GetStoryRow(
            ChapterIndex, 
            SceneIndex, 
            ClipIndex, 
            NumberIndex
            );
            if (currentRow != null)
            {
                ArtLoader currentIllustration = ArtLoaderBehaviors.Instance.GetArtLoadingList($"{currentRow.Name}");
                BackgroundLoader currentBackgroundLoader = BackgroundLocations.Instance.GetBackgroundLoadingList($"{currentRow.Background}");
                if (Nextpage)
                {
                    Nextpage = !Nextpage;
                    Debug.Log("=== Play ===");
                    Debug.Log($"Name:{currentRow.Name}");
                    Debug.Log($"Text {currentRow.Text}");
                    Debug.Log($"Re1 {currentRow.Re1}");
                    Debug.Log($"Re2 {currentRow.Re2}");
                    Debug.Log($"Re3 {currentRow.Re3}");
                    Debug.Log($"Re4 {currentRow.Re4}");
                    if (currentIllustration != null)
                    {
                        Debug.Log($"CurrentIllustration is {currentIllustration.IlluLocation}"); 
                    }
                    else
                    {
                        Debug.LogWarning($"IlluLocation {currentRow.Name} can not be found!");
                    }
                    if (currentBackgroundLoader != null)
                    {
                        Debug.Log($"CurrentBackgroundLoader is {currentBackgroundLoader.Backgroundpath}"); 
                    }
                    else
                    {
                        Debug.LogWarning($"Backgroundpath {currentRow.Background} can not be found!");
                    }
                }
            }
            else
            {
                ChapterIndex = SceneIndex = ClipIndex = 0;
                NumberIndex = 0;
                StoryPlay = false;
            }
        }
    }
}
