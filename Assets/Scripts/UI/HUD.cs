using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
     [SerializeField] private string winText;
     [SerializeField] private string loseText;
     
     [Header("Buttons")]
     [SerializeField] private Transform buttons;
     [SerializeField] private Button nextLevelButton;
     [SerializeField] private Button tryAgainButton;

     private Player player;

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
          endText.text = winText;
          tryAgainButton.gameObject.SetActive(false);
          nextLevelButton.gameObject.SetActive(true);
          
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
          Core.LoadScene("MainMenu");
     }

     public void TryAgain()
     {
          Giroscope.GravityMultiplier = 1;
          Accelerometr.CanDash = true;
          player.SetConstraintsFalse();
          HideScreen();
          //Core.LoadCurrentSceneAgain();
          player = Instantiate(Settings.player, Settings.playerStartPos, Quaternion.identity).GetComponent<Player>();
          Settings.cameraFollow.what = player.GetComponentInChildren<PlayerMainBone>().transform;
     }

     public void LoadNextLevel()
     {
          Core.LoadScene("");
     }
} 
