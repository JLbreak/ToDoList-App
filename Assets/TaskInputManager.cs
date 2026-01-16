// TaskInputManager.cs
using UnityEngine;
using TMPro; // Required for TextMeshPro UI components
using UnityEngine.SceneManagement; // Required for scene loading
using UnityEngine.UI; // Required for general UI components like Button (if you have any other than TMP buttons)

public class TaskInputManager : MonoBehaviour
{
    public TMP_InputField taskInputField;
    public TMP_Dropdown taskTypeDropdown;

    // This method is called when the "Add To List" button is clicked
    public void OnAddTaskButtonClick()
    {
        // Check if DataManager exists
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager not found! Cannot add task. Please ensure it's in Scene1 and set up correctly.");
            return;
        }

        // Get the task description from the input field
        string taskDescription = taskInputField.text;

        // Validate the input field content
        if (string.IsNullOrWhiteSpace(taskDescription))
        {
            Debug.LogWarning("Task description cannot be empty!");
            return;
        }

        // Get the selected task type from the dropdown
        TaskType selectedType = (TaskType)taskTypeDropdown.value; // Cast dropdown index to TaskType enum

        // Add the new task to the DataManager
        DataManager.Instance.AddTask(taskDescription, selectedType);

        // Determine which checklist scene to load based on the selected task type
        string sceneToLoad = "";
        switch (selectedType)
        {
            case TaskType.Work:
                sceneToLoad = "WorkChecklistScene";
                break;
            case TaskType.Chores:
                sceneToLoad = "ChoresChecklistScene";
                break;
            case TaskType.BucketList:
                sceneToLoad = "BucketListChecklistScene";
                break;
            case TaskType.Studies:
                sceneToLoad = "StudiesChecklistScene";
                break;
            default:
                // This should theoretically not be reached if enum and dropdown options match
                Debug.LogError("Unknown TaskType selected! Please check dropdown options and TaskType enum.");
                return; // Stop execution if an unknown type is somehow selected
        }

        // Log the scene that is about to be loaded
        Debug.Log($"Attempting to load scene: '{sceneToLoad}' after adding task.");

        // Load the determined checklist scene
        SceneManager.LoadScene(sceneToLoad);
    }

    // --- NEW METHODS FOR DIRECT SCENE NAVIGATION BUTTONS ---

    // Method to load the Work Checklist Scene
    public void GoToWorkList()
    {
        Debug.Log("Loading Work Checklist Scene.");
        // Ensure scene name exactly matches the name in File > Build Settings
        SceneManager.LoadScene("WorkChecklistScene");
    }

    // Method to load the Chores Checklist Scene
    public void GoToChoresList()
    {
        Debug.Log("Loading Chores Checklist Scene.");
        // Ensure scene name exactly matches the name in File > Build Settings
        SceneManager.LoadScene("ChoresChecklistScene");
    }

    // Method to load the Bucket List Checklist Scene
    public void GoToBucketList()
    {
        Debug.Log("Loading Bucket List Checklist Scene.");
        // Ensure scene name exactly matches the name in File > Build Settings
        SceneManager.LoadScene("BucketListChecklistScene");
    }

    // Method to load the Studies Checklist Scene
    public void GoToStudiesList()
    {
        Debug.Log("Loading Studies Checklist Scene.");
        // Ensure scene name exactly matches the name in File > Build Settings
        SceneManager.LoadScene("StudiesChecklistScene");
    }

    // --- NEW METHOD TO EXIT THE APPLICATION ---
    public void ExitApplication()
    {
        Debug.Log("Exiting Application...");

        // This line quits the application. It only works in a built game.
        // In the Unity Editor, it will stop Play Mode.
        Application.Quit();

        // Optional: This part is for stopping play mode directly in the Unity Editor
        // It's wrapped in a preprocessor directive so it's not included in actual builds.
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}