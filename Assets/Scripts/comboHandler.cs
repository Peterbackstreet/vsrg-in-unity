using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class comboHandler : MonoBehaviour
{
    private int combo = 0, maxCombo = 0;
    private TMP_Text comboText;
    void Start()
    {
        comboText = gameObject.GetComponent<TMP_Text>();
        updateComboText();
    }

    public void addCombo()
    {
        combo++;
        maxCombo = math.max(combo, maxCombo);
        updateComboText();
    }

    public void resetCombo()
    {
        combo = 0;
        updateComboText();
    }
    public void updateComboText()
    {
        comboText.text = combo.ToString();
    }
}
