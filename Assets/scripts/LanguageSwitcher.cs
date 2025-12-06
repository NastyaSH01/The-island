using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;
using System.Globalization;
// Добавляем этот using для работы с асинхронными операциями
using UnityEngine.ResourceManagement.AsyncOperations; 

public class LanguageSwitcher : MonoBehaviour
{
    // Start вызывается Unity автоматически
    void Start()
    {
        // Система локализации инициализируется автоматически при старте. 
        // Мы можем просто дождаться её завершения, если она ещё не готова.
        if (LocalizationSettings.Instance != null && !LocalizationSettings.InitializationOperation.IsDone)
        {
            // В Start() лучше просто запустить процесс, а не блокировать его.
            // Функционал кнопок работает через корутину, которая сама ждет инициализации.
        }
    }

    /// <summary>
    /// Переключает язык игры на указанную локаль (например, "ru" или "en").
    /// </summary>
    /// <param name="localeCode">Код локали (например, "ru", "en-US", "fr").</param>
    public void ChangeLanguage(string localeCode)
    {
        // Запускаем корутину для асинхронной смены языка
        StartCoroutine(SetLocale(localeCode));
    }

    // Корутина для смены локали
    IEnumerator SetLocale(string localeCode)
    {
        // Ждем, пока система локализации будет готова
        // LocalizationSettings.InitializationOperation можно использовать как yield
        yield return LocalizationSettings.InitializationOperation;

        // Ищем нужную локаль по коду (например, "ru")
        System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(localeCode);
        
        // Используем AvailableLocales.GetLocale напрямую
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(cultureInfo);

        Debug.Log($"Язык изменен на {cultureInfo.NativeName}");
    }
}

