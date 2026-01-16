// TaskData.cs
using System; // Required for [Serializable]

// This enum will define the different types of tasks
public enum TaskType
{
    Work,
    Chores,
    BucketList,
    Studies
}

// This class will hold the details of a single task
// [Serializable] allows instances of this class to be serialized and deserialized
// which is useful if you later want to save/load tasks to a file.
[Serializable]
public class TaskItem
{
    public string description;
    public TaskType type;
    public bool isCompleted;

    public TaskItem(string desc, TaskType t)
    {
        description = desc;
        type = t;
        isCompleted = false; // Tasks are not completed by default when added
    }
}
