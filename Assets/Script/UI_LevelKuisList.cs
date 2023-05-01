
using UnityEngine;

public class UI_LevelKuisList : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private UI_OpsiLevelKuis _tombolLevel = null;

    [SerializeField]
    private RectTransform _content = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private GameSceneManager _gameScaneManager = null;

    [SerializeField]
    private string _gamePlayScene = null;

    //Start is called before the first frame update
    private void Start()
    {
        // Subscribe events
        UI_OpsiLevelKuis.EventSaatKlik += UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void OnDestroy()
    {
        //unsubscribe
        UI_OpsiLevelKuis.EventSaatKlik -= UI_OpsiLevelKuis_EventSaatKlik;
    }

    private void UI_OpsiLevelKuis_EventSaatKlik(int index)
    {
        _inisialData.levelIndex = index;
        _gameScaneManager.BukaScene(_gamePlayScene);
    }

    //Update is called once per frame
    public void UnloadLevelPack(LevelPackKuis levelPack)
    {
        HapusIsiKonten();

        _levelPack = levelPack;
        for (int i = 0; i < levelPack.BanyakLevel; i++)
        {
            //membuat salinan objek dari prefab tombol level 
            var t = Instantiate(_tombolLevel);

            t.SetLevelKuis(levelPack.AmbilLevelKe(i), i);

            //masukan objek tombol sebagai anak dari objek "content"
            t.transform.SetParent(_content);
            t.transform.localScale = Vector3.one;
        }   
    }

    private void HapusIsiKonten()
    {
        var cc = _content.childCount;

        for (int i = 0; i < cc; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
    }
}
