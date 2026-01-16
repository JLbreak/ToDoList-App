// ChecklistItemUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChecklistItemUI : MonoBehaviour
{
    public Toggle taskToggle;
    public TextMeshProUGUI taskDescriptionText;
    public Button deleteButton; // Assign this in the Inspector

    // Optional: A method to initialize the UI for the item
    public void Setup(string description, bool isCompleted)
    {
        taskDescriptionText.text = description;
        taskToggle.isOn = isCompleted;
        // Delete button state will be handled by ChecklistDisplayManager
    }
}