using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuConfirmMessage : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private gameObject _pesanCukupKoin = null;

    [SerializeField]
    private gameObject _pesanTakCukupKoin = null;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);

        //subscribe
        UI_OpsiLevelPack.EventSaatKlik += UI_OpsiLevelPack_EventSaatKlik;
    }

    private void OnDestroy()
    {
        //Unsubscribe
        UI_OpsiLevelPack.EventSaatKlik -= UI_OpsiLevelPack_EventSaatKlik;
    }

    private void UI_OpsiLevelPack_EventSaatKlik(LevelPackKuis levelPack, bool terkunci)
    {
        //cek apakah terkunci atau tidak, jika tidak maka abaikan
        if (!terkunci) return;

        gameObject.SetActive(true);

        //Cek kecukupan koin untuk membeli level pack 
        if (_playerProgress.progresData.koin < levelPack.Harga)
        {
            //jika tidak cukup 
            _pesanCukupKoin.SetActive(false);
            _pesanTakCukupKoin.SetActive(true);
            return;
        }

        //jika cukup 
        _pesanTakCukupKoin.SetActive(false);
        _pesanCukupKoin.SetActive(true);
    }
}
