using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private InisialDataGameplay _inisialData = null;

    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private LevelPackKuis _soalSoal = null;

    [SerializeField]
    private UI_Pertanyaan _pertanyaan = null;
    
    [SerializeField]
    private UI_PoinJawaban[] _pilihanJawaban = new UI_PoinJawaban[0];

    [SerializeField]
    private GameSceneManager _gameScaneManager = null;

    [SerializeField]
    private string _namaScanePilihMenu = string.Empty;

    private int _indexSoal = -1;

    private void Start()
    {
        // _soalSoal = _inisialData.levelPack;
        _indexSoal = _inisialData.levelIndex - 1;
        
        NextLevel();

        //Subscribe event
        UI_PoinJawaban.EventJawabSoal += UI_PoinJawaban_EventJawabSoal;

    }
    
    private void OnDestroy()
    {
        //Unsubscribe
        UI_PoinJawaban.EventJawabSoal -= UI_PoinJawaban_EventJawabSoal;
    }

    private void OnApplicationQuit()
    {
        _inisialData.SaatKalah = false;
    }

    private void UI_PoinJawaban_EventJawabSoal(string jawaban, bool adalahBenar)
    {
        if (adalahBenar)
        {
            _playerProgress.progresData.koin += 20;
        }
    }

    public void NextLevel()
    {
        // Soal index selanjutnya
        _indexSoal++;

        //jika index melampaui soal terakhir, ulang dari awal
        if (_indexSoal >= _soalSoal.BanyakLevel)
        {
            // _indexSoal = 0;
            _gameScaneManager.BukaScene(_namaScanePilihMenu);
            return;
        }

        //ambil data pertanyaan
        LevelSoalKuis soal = _soalSoal.AmbilLevelKe(_indexSoal);

        //set informasi soal
        _pertanyaan.SetPertanyaan($"Soal {_indexSoal + 1}", 
            soal.pertanyaan, soal.petunjukJawaban);

        for (int i = 0; i < _pilihanJawaban.Length; i++)
        {
            UI_PoinJawaban poin = _pilihanJawaban[i];
            LevelSoalKuis.OpsiJawaban opsi = soal.opsiJawaban[i];
            poin.SetJawaban(opsi.jawabanTeks, opsi.adalahBenar);
        }
        
    }
}
