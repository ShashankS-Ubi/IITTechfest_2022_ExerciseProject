using System.Linq;
using UnityEngine;

public class CalamityManager : MonoBehaviour
{
    [SerializeField]
    private CalamityDataScriptableObject calamityData;

    [SerializeField]
    private int _minCalamityInterval = 10;

    [SerializeField]
    private int _calamityIntervalDelta = 10;

    private float _time = 0.0f;
    private int _currCalamityInterval;

    public void Init()
    {
        _time = 0.0f;
        SetRandomCalamityInterval();
    }

    private void Update()
    {
        // Call StartCalamity() to run periodically
    }

    public void StartCalamity()
    {
        // Implement a way to start calamity 
    }

    public void SetRandomCalamityInterval()
    {
        _currCalamityInterval = UnityEngine.Random.Range(_minCalamityInterval, _minCalamityInterval + _calamityIntervalDelta);
        Debug.Log(string.Format($"Next calamity in { _currCalamityInterval} seconds."));
    }

    void CalamityAction(CalamityType type)
    {
        Debug.Log(string.Format($"Current Calamity : {type.ToString()}"));
        switch (type)
        {
            case CalamityType.THUNDERSTORM:
                {
                    CalamityData data = calamityData.CalamityDataList.FirstOrDefault(i => i.CalamityType == CalamityType.THUNDERSTORM);
                    var tiles = GameDirector.Instance.GameTileManager.GetAllTiles();
                    DamageBuildings(tiles, data.DamageCofficient);
                    GameDirector.Instance.VfxManager.PlayVFX(VfxType.THUNDERSTORM);
                    break;
                }
        }
    }

    void DamageBuildings(Tile[] tiles, int damage)
    {
        GameDirector.Instance.GameTileManager.CalamityDamage(tiles, damage);
        Debug.Log(string.Format($"Damage Building By: {damage}"));
    }
}
