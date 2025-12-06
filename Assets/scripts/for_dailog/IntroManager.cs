using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public TextMeshProUGUI mainStoryText;
    public Image backgroundPanel; // Ссылка на фоновую панель (черный фон)
    [TextArea(5, 10)] 
    public string[] storySentences; 
    public float timeBetweenSentences = 3f; // Время ожидания между предложениями
    public float fadeDuration = 1.5f; // Длительность плавного появления/затухания

    private int sentenceIndex = 0;
    
    void Start()
    {
        if (mainStoryText == null || backgroundPanel == null)
        {
            Debug.LogError("mainStoryText или backgroundPanel не назначен в инспекторе!");
            return;
        }
        
        mainStoryText.text = ""; 
        
        // **********************************************
        // Убеждаемся, что фон СРАЗУ черный и непрозрачный
        // **********************************************
        Color startColor = backgroundPanel.color;
        startColor.a = 1f; // Устанавливаем альфа-канал в 1 (непрозрачно)
        backgroundPanel.color = startColor;

        // Запускаем основную последовательность интро
        StartCoroutine(RunIntroSequence());
    }

    IEnumerator RunIntroSequence()
    {
        // *************************************************************
        // НОВОЕ: Сначала плавно убираем черный экран (Fade In в игру)
        // *************************************************************
        yield return StartCoroutine(FadeBackground(1f, 0f, fadeDuration));

        // Теперь запускается ваше интро
        while (sentenceIndex < storySentences.Length)
        {
            yield return StartCoroutine(TypeSentence(storySentences[sentenceIndex]));
            yield return new WaitForSeconds(timeBetweenSentences);
            sentenceIndex++;
        }

        // *************************************************************
        // НОВОЕ: Когда все предложения показаны, плавно затемняем экран (Fade Out)
        // *************************************************************
        StartCoroutine(EndIntroWithFade());
    }

    IEnumerator TypeSentence(string sentence)
    {
        mainStoryText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            mainStoryText.text += letter;
            yield return new WaitForSeconds(0.05f); // Скорость печати
        }
    }
    
    IEnumerator EndIntroWithFade()
    {
        // Убираем текст с экрана
        mainStoryText.text = ""; 

        Debug.Log("Затемнение экрана перед загрузкой сцены.");
        
        // Плавно затемняем фон обратно в черный (от прозрачного (0) к непрозрачному (1))
        yield return StartCoroutine(FadeBackground(0f, 1f, fadeDuration));

        Debug.Log("Интро закончено, загрузка сцены 2.");
        
        // Загружаем вторую сцену (индекс 2 в Build Settings)
        SceneManager.LoadScene(2);
    }

    // *************************************************************
    // НОВАЯ ВСПОМОГАТЕЛЬНАЯ ФУНКЦИЯ ДЛЯ ПЛАВНОГО ИЗМЕНЕНИЯ АЛЬФА-КАНАЛА ФОНА
    // *************************************************************
    IEnumerator FadeBackground(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0;
        Color color = backgroundPanel.color;
        
        while (timer < duration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, timer / duration);
            backgroundPanel.color = color;
            yield return null;
        }
        
        // Убеждаемся, что конечное значение установлено точно
        color.a = endAlpha;
        backgroundPanel.color = color;
    }
}


