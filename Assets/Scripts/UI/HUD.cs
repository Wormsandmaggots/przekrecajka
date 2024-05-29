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
          player = FindAnyObjectByType<Player>();
          instance = this;
     }

     public void ShowWinScreen()
     {
          endImage.sprite = winImage;
          endText.text = winText;
          tryAgainButton.gameObject.SetActive(false);
          nextLevelButton.gameObject.SetActive(true);
          
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
          Core.LoadCurrentSceneAgain();
     }

     public void LoadNextLevel()
     {
          Core.LoadScene("");
     }
} 
