using UnityEngine;
using TMPro; // Не забудьте добавить это
using System.Collections; // Для эффекта печати

// Имя класса должно совпадать с тем, что мы используем в dailog_trigg
public class DailogManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI monologueText;
    private string currentMonologue;

    void Start()
    {
        // Убедитесь, что панель скрыта при запуске
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    public void StartDialogue(string monologue)
    {
        // Останавливаем предыдущую корутину печати, если она была запущена
        StopAllCoroutines(); 
        dialoguePanel.SetActive(true);
        currentMonologue = monologue;
        StartCoroutine(TypeSentence(currentMonologue));
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        monologueText.text = "";
        // Опционально можно остановить корутину при выходе, чтобы текст не допечатывался в пустоту
        StopAllCoroutines(); 
    }
    
    // Эффект печати текста
    IEnumerator TypeSentence(string sentence)
    {
        monologueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            monologueText.text += letter;
            yield return new WaitForSeconds(0.05f); // Регулируйте скорость
        }
    }
}
