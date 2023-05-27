using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName = "New Quest";
    public string description;
    public bool isActive;
    public bool isComplete = false;

    // Additional quest properties and data
    public int requiredItemCount;

    // public GameObject questGiver;

    // ...

    // Methods to handle quest progression and completion
    public void StartQuest()
    {
        isActive = true;
        Debug.Log("Quest started: " + questName);
    }

    public void CompleteQuest()
    {
        isActive = false;
        isComplete = true;
        Debug.Log("Quest completed: " + questName);
        // Perform any necessary actions upon quest completion
    }
}
