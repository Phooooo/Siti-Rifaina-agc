using UnityEngine;

public class UI_LevelPackList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_LevelKuisList _levelList = null;

    [SerializeField]
    private UI_OpsiLevelPack _tombolLevelPack = null;

    [SerializeField]
    private RectTransform _content = null;

    [SerializeField]
    private LevelPackKuis[] _levelPacks = new LevelPackKuis[0];

    // Start is called before the first frame update
    private void Start()
    {
        LoadLevelPack();

        if (_inisialData.SaatKalah)
        {
            UI_OpsiLevelPack_EventSaatKlik(_inisialData.levelPack, false);
        }

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
        if (terkunci)
            return;
        //buka menu levels
        _levelList.gameObject.SetActive(true);
        _levelList.UnloadLevelPack(levelPack);

        gameObject.SetActive(false);

        _inisialData.levelPack = levelPack;
    }

    // Update is called once per frame
    private void LoadLevelPack()
    {
        foreach (var lp in _levelPacks)
        {
            //membuat salinan objek dari prefab tombol level 
            var t = Instantiate(_tombolLevelPack);

            t.SetLevelPack(lp);

            //masukan objek tombol sebagai anak dari objek "content"
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;
        }   
    }
}
