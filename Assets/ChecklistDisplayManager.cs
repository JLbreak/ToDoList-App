// ChecklistDisplayManager.cs
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class ChecklistDisplayManager : MonoBehaviour
{
    public TaskType currentSceneTaskType;
    public GameObject checklistItemPrefab;
    public Transform contentParent;

    // We'll call DisplayTasks() from Start, but also after a deletion,
    // so we make it public (or callable internally after events).
    void Start()
    {
        Debug.Log("--- ChecklistDisplayManager Start Method Called ---");
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager not found! Please ensure it's in Scene1 and set up correctly.");
            SceneManager.LoadScene("Scene1_TaskInput");
            return;
        }
        Debug.Log($"DataManager found. Total tasks currently stored: {DataManager.Instance.allTasks.Count}");

        // Call DisplayTasks on start
        DisplayTasks();
    }

    // This method will now be called after a delete as well
    public void DisplayTasks() // Made public if you want to call it from other scripts later, or keep private if only internal
    {
        Debug.Log("--- DisplayTasks Method Called ---");

        // Clear existing items to refresh the list
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        var tasksToDisplay = DataManager.Instance.allTasks
                                        .Where(task => task.type == currentSceneTaskType)
                                        .ToList();

        Debug.Log($"Filtering tasks for type: {currentSceneTaskType}. Found {tasksToDisplay.Count} tasks to display.");

        if (checklistItemPrefab == null)
        {
            Debug.LogError("ERROR: 'checklistItemPrefab' is NULL in the Inspector!");
            return;
        }
        if (contentParent == null)
        {
            Debug.LogError("ERROR: 'contentParent' is NULL in the Inspector!");
            return;
        }

        foreach (var task in tasksToDisplay)
        {
            GameObject itemGO = Instantiate(checklistItemPrefab, contentParent);
            Debug.Log($"Successfully instantiated GameObject: '{itemGO.name}' under parent: '{contentParent.name}'");

            // Get the ChecklistItemUI script from the instantiated prefab
            ChecklistItemUI itemUI = itemGO.GetComponent<ChecklistItemUI>();

            if (itemUI != null)
            {
                // Set initial text and toggle state using the helper script
                itemUI.taskDescriptionText.text = task.description;
                itemUI.taskToggle.isOn = task.isCompleted;

                // --- NEW: Add Listener for Toggle (existing logic) ---
                itemUI.taskToggle.onValueChanged.AddListener((bool value) => {
                    task.isCompleted = value;
                    Debug.Log($"Task '{task.description}' completion status changed to: {value}");
                    // Optionally, show/hide delete button based on completion
                    // itemUI.deleteButton.gameObject.SetActive(value);
                });

                // --- NEW: Add Listener for Delete Button ---
                itemUI.deleteButton.onClick.AddListener(() => {
                    // Call the DeleteTask method on the DataManager
                    DataManager.Instance.DeleteTask(task);
                    // Refresh the display after deletion
                    DisplayTasks();
                });

                // --- Optional: Control Delete Button Visibility ---
                // If you only want the delete button to show when task is completed
                // itemUI.deleteButton.gameObject.SetActive(task.isCompleted);

                Debug.Log($"Task '{task.description}' configured in UI.");
            }
            else
            {
                Debug.LogError($"ERROR: ChecklistItemUI script not found on {itemGO.name}! Ensure it's attached to the prefab root.");
                // Fallback to old method if ChecklistItemUI is somehow missing
                TextMeshProUGUI taskText = itemGO.GetComponentInChildren<TextMeshProUGUI>();
                if (taskText != null) taskText.text = task.description;
            }
        }
        Debug.Log("--- Finished displaying tasks ---");
    }

    public void GoBackToInputScene()
    {
        Debug.Log("Back button clicked. Loading Scene1_TaskInput.");
        SceneManager.LoadScene("Scene1_TaskInput");
    }
}