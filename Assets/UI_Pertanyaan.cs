using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Pertanyaan : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _tempatJudul = null;

    [SerializeField]
    private TextMeshProUGUI tempatTeks = null;

    [SerializeField]
    private Image tempatGambar = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(tempatTeks.text);
    }

    public void SetPertanyaan(string judul, string teksPertanyaan, Sprite gambarHint)
    {
        _tempatJudul.text = judul;
        tempatTeks.text = teksPertanyaan;
        tempatGambar.sprite = gambarHint;
    }
}
