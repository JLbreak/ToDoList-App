// DataManager.cs
using UnityEngine;
using System.Collections.Generic;

// (TaskItem and TaskType definitions are here)

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }
    public List<TaskItem> allTasks = new List<TaskItem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddTask(string description, TaskType type)
    {
        TaskItem newTask = new TaskItem(description, type);
        allTasks.Add(newTask);
        Debug.Log($"Task Added: '{description}' of type '{type}'");
    }

    // --- NEW METHOD TO DELETE A TASK ---
    // We pass the TaskItem object itself to ensure we delete the correct one.
    public void DeleteTask(TaskItem taskToDelete)
    {
        if (allTasks.Remove(taskToDelete))
        {
            Debug.Log($"Task Deleted: '{taskToDelete.description}'");
        }
        else
        {
            Debug.LogWarning($"Attempted to delete task '{taskToDelete.description}' but it was not found in the list.");
        }
    }
}