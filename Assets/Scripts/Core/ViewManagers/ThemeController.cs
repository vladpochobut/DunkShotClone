using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour
{
    private const string ThemeIdKey = "ThemeId";

    [SerializeField]
    private GameObject[] ThemeCheck;

    [SerializeField]
    private SpriteRenderer _bgRenderer;
    [SerializeField]
    private SpriteRenderer _wallRenderer;

    private void OnEnable()
    {
        CheckTheme();
        UpdateTheme();
    }

    private void CheckTheme()
    {
        for (int i = 0; i < ThemeCheck.Length; i++)
        {
            ThemeCheck[i].SetActive(false);
        }
        ThemeCheck[PlayerPrefs.GetInt(ThemeIdKey)].SetActive(true);
    }

    public void SelectTheme(int id)
    {
        PlayerPrefs.SetInt(ThemeIdKey, id);
        CheckTheme();
        UpdateTheme();
    }

    private void UpdateTheme()
    {
        int themeId = PlayerPrefs.GetInt(ThemeIdKey);
        _bgRenderer.sprite = Resources.Load<Sprite>("Themes/" + themeId + "/" + "bg_" + themeId);
        _wallRenderer.sprite = Resources.Load<Sprite>("Themes/" + themeId + "/" + "wall_" + themeId);
    }
}
