using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{

   public static UIHandler instance { get; private set; }

   // UI dialogue window variables
   public float displayTime = 4.0f;
   private VisualElement m_NonPlayerDialogue;
   private Label dialogueLabel;
   private float m_TimerDisplay;
   UIDocument uiDocument ;


   // Awake is called when the script instance is being loaded (in this situation, when the game scene loads)
   private void Awake()
   {
       instance = this;
   }

   // Start is called before the first frame update
   private void Start()
   {
       uiDocument = GetComponent<UIDocument>();



       m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
       dialogueLabel = m_NonPlayerDialogue.Q<VisualElement>("Background").Q<Label>("Label");
       if(dialogueLabel == null)
       {
        Debug.Log("Dialogue label does not exist");
       }
       m_NonPlayerDialogue.style.display = DisplayStyle.None;
       m_TimerDisplay = -1.0f;


   }



   private void Update()
   {
       if (m_TimerDisplay > 0)
       {
           m_TimerDisplay -= Time.deltaTime;
           if (m_TimerDisplay < 0)
           {
               m_NonPlayerDialogue.style.display = DisplayStyle.None;
           }


       }
   }


   
   
   public void DisplayDialogue(string dialogue)
   {
    if(dialogueLabel != null)
    {
        dialogueLabel.text = dialogue;
    }
   m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
   m_TimerDisplay = displayTime;
   }

}