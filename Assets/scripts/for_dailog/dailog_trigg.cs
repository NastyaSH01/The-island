using UnityEngine;

public class dailog_trigg : MonoBehaviour
{
    // Это поле, куда вы вводите текст в инспекторе Юнити
    public string monologueText; 
    
    // Ссылка на менеджер диалогов в сцене. 
    // Тип переменной изменен на DailogManager
    private DailogManager dialogueManager;

    void Start()
    {
        // Найти DailogManager в сцене при старте игры
        dialogueManager = FindObjectOfType<DailogManager>();
        
        if (dialogueManager == null)
        {
            Debug.LogError("DailogManager не найден в сцене! Пожалуйста, добавьте его.");
        }
    }

    // Вызывается Unity, когда другой коллайдер входит в триггер
    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект, вошедший в триггер, — это игрок (убедитесь, что у игрока тег "Player")
        if (other.CompareTag("Player"))
        {
            // Автоматически запускаем диалог через DailogManager
            dialogueManager.StartDialogue(monologueText);
        }
    }

    // Вызывается Unity, когда другой коллайдер выходит из триггера
    void OnTriggerExit2D(Collider2D other)
    {
        // Когда игрок выходит из триггера
        if (other.CompareTag("Player"))
        {
            // Автоматически закрываем диалог через DailogManager
            dialogueManager.EndDialogue();
        }
    }
}
