using UnityEngine;
using System.Collections;
public enum GamePlayStates
{
    NONE = 0,
    INGAME = 1,
    PAUSE = 2
}

public class GameDirector : MonoBehaviour
{
    [HideInInspector]
    public InventoryManager InventoryManager = null;
    [HideInInspector]
    public TileManager GameTileManager = null;
    [HideInInspector]
    public BuildingUpgradeDataManager UpgradeDataManager = null;
    [HideInInspector]
    public GameplayUIManager GameplayUIManager = null;
    [HideInInspector]
    public TileSelectionManager TileSelectionManager = null;
    [HideInInspector]
    public VFXManager VfxManager = null;
    [HideInInspector]
    public AudioManager AudioManager;
    [HideInInspector]
    public CalamityManager CalamityManager;

    public static GameDirector Instance = null;

    private void Awake()
    {
        ///How do you make this class a Singleton?
        

        ///Initializing
        ///Get the components of and Initialize these:
        /// - InventoryManager
        /// - UpgradeDataManager
        /// - GameTileManager
        /// - GameplayUIManager
        /// - CalamityManager



        
        InventoryManager.ResourcesStatsChanged += OnResourcesStatsChanged;

        TileSelectionManager = GetComponent<TileSelectionManager>();
        TileSelectorRaycaster.TileSelected += OnTileSelected;

        GameplayUIManager.BuildingCreationRequested += OnBuildingCreationRequest;

        VfxManager = GetComponent<VFXManager>();

        AudioManager = GetComponent<AudioManager>();
        AudioManager.Init();
    }

    private void Start()
    {
        ///This just starts the background score as soon as the game starts.
        /// Where's the Background Score?!
    }

    private void OnResourcesStatsChanged()
    {
        GameplayUIManager.RefreshTopBarUI();
    }

    private void OnTileSelected(Tile tile)
    {
        TileSelectionManager.SelectTile(tile);
    }

    private void OnDestroy()
    {
        GameplayUIManager.BuildingCreationRequested -= OnBuildingCreationRequest;
    }

    private void OnBuildingCreationRequest(BuildingMenuData buildingMenuData, Tile tile)
    {
        GameTileManager.CreateBuilding(buildingMenuData, tile);
    }
}