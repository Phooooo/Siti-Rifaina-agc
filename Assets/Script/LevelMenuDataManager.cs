using TMPro;
using UnityEngine;

public class LevelMenuDataManager : MonoBehaviour
{
    [SerializeField]
    private PlayerProgress _playerProgress = null;

    [SerializeField]
    private TextMeshProUGUI _tempatKoin = null;

    void Start()
    {
        if (!_playerProgress.MuatProgres())
        {
            _playerProgress.SimpanProgres();
        }
        
        _tempatKoin.text = $"{_playerProgress.progresData.koin}";
    }

}
