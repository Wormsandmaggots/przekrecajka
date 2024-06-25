using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HUD : MonoBehaviour
{
     public static HUD instance;

     [SerializeField] private Transform mainPanel;
     [SerializeField] private Transform mainPanelDestination;
     [SerializeField] private GameObject blurPanel;
     private Vector3 mainPanelStartPos;
     
     [Header("Image")]
     [SerializeField] private Image endImage;
     [SerializeField] private Sprite winImage;
     [SerializeField] private Sprite loseImage;
     
     [Header("Text")]
     [SerializeField] private TextMeshProUGUI endText;
     [SerializeField] private string[] winText;
     [SerializeField] private string loseText;
     
     [Header("Buttons")]
     [SerializeField] private Button nextLevelButton;
     [SerializeField] private Button tryAgainButton;

     private Player player;

     private void Awake()
     {
          instance = this;
     }

     private void Start()
     {
          blurPanel.SetActive(false);
          mainPanelStartPos = mainPanel.transform.position;
          player = FindAnyObjectByType<Player>();
          instance = this;
     }

     public void HideScreen()
     {
          blurPanel.SetActive(false);
          BlurManager.SetBlur(false);
          
          mainPanel.gameObject.SetActive(false);
          mainPanel.DOMove(mainPanelStartPos, 1f);
     }

     public void ShowWinScreen()
     {
          endImage.sprite = winImage;
          endText.text = winText[Random.Range(0, winText.Length)];
          tryAgainButton.gameObject.SetActive(false);
          nextLevelButton.gameObject.SetActive(true);
          PlayerMainBone.AllowCollision = false;
          
          WorldLoader.UpdateSave(WorldChoose.chosenWorldName, LevelPicker.CurrentLevel, true);
          
          ShowHud();
     }

     public void ShowLoseScreen()
     {
          endImage.sprite = loseImage;
          endText.text = loseText;
          tryAgainButton.gameObject.SetActive(true);
          nextLevelButton.gameObject.SetActive(false);
          
          ShowHud();
     }

     private void ShowHud()
     {
          mainPanel.gameObject.SetActive(true);
          
          blurPanel.SetActive(true);
          BlurManager.SetBlur(true);

          Giroscope.GravityMultiplier = 0;
          Accelerometr.CanDash = false;
          
          mainPanel.DOJump(mainPanelDestination.position, 4, 4, 1.5f);
     }

     public void LoadMainMenu()
     {
          PlayerMainBone.AllowCollision = true;
          Giroscope.GravityMultiplier = 1;
          Accelerometr.CanDash = true;
          player.SetConstraintsFalse();
          
          BlurManager.renderTexture.Release();

          Core.LoadScene("MainMenu");
     }

     public void TryAgain()
     {
          PlayerMainBone.AllowCollision = true;
          Giroscope.GravityMultiplier = 1;
          Accelerometr.CanDash = true;
          player.SetConstraintsFalse();
          HideScreen();
          //Core.LoadCurrentSceneAgain();
          player = Instantiate(Settings.player, Settings.playerStartPos, Quaternion.identity).GetComponent<Player>();
          Settings.cameraFollow.what = player.GetComponentInChildren<PlayerMainBone>().transform;

          CameraFollow.FollowY = InitialFollow.FollowY;
          CameraFollow.FollowX = InitialFollow.FollowX;
     }

     public void LoadNextLevel()
     {
          BlurManager.renderTexture.Release();

          PlayerMainBone.AllowCollision = true;
          Giroscope.GravityMultiplier = 1;
          Accelerometr.CanDash = true;
          player.SetConstraintsFalse();
          
          LevelPicker.CurrentLevel = (Convert.ToInt16(LevelPicker.CurrentLevel) + 1).ToString();

          string toLoad = WorldChoose.chosenWorldName + "_" + LevelPicker.CurrentLevel;
          
          Debug.Log(toLoad);
          Core.LoadScene(toLoad);
     }
} 
