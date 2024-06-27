using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class HUD : MonoBehaviour
{
     public static HUD instance;
     private static int i = 0;

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

     [SerializeField] private GameObject livesContainer;
     private Image[] lives;
     [SerializeField] private Image transition;

     private Player player;

     private void Awake()
     {
          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Confined;
          instance = this;

          lives = livesContainer.GetComponentsInChildren<Image>();
     }

     private void Start()
     {
          blurPanel.SetActive(false);
          mainPanelStartPos = mainPanel.transform.position;
          player = FindAnyObjectByType<Player>();
          instance = this;

          transition.gameObject.SetActive(true);
          transition.DOFade(0, 1);
     }

     public void UpdateHp(int currentHP)
     {
          for (int i = 0; i < lives.Length; i++)
          {
               if (currentHP <= 0)
               {
                    lives[i].DOFade(0.2f, 1f);
                    lives[i].transform.DOPunchScale(Vector3.one * 1.2f, 1f);
                    lives[i].transform.DOScale(Vector3.one * 1.2f, 1.0f);
               }
               else
               {
                    lives[i].DOFade(1.0f, 1f);
                    lives[i].transform.DOScale(Vector3.one, 1.0f);
                    currentHP -= 1;
               }
          }
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
          
          if (i == 0 && LevelPicker.CurrentLevel == "5")
          {
               WorldChoose.chosenWorldName = "BlackHole";
               WorldLoader.UpdateSave(WorldChoose.chosenWorldName, "w", false);
               LevelPicker.CurrentLevel = "0";
               i++;
          }
          else if (i == 1 && LevelPicker.CurrentLevel == "8")
          {
               WorldChoose.chosenWorldName = "Sun";
               WorldLoader.UpdateSave(WorldChoose.chosenWorldName, "w", false);
               LevelPicker.CurrentLevel = "0";
               i++;
          }
          
          WorldLoader.UpdateSave(WorldChoose.chosenWorldName, (Convert.ToInt16(LevelPicker.CurrentLevel) + 1).ToString(), false);
          
          ShowHud();
          
          AudioManager.instance.Play("win");

     }

     public void ShowLoseScreen()
     {
          endImage.sprite = loseImage;
          endText.text = loseText;
          tryAgainButton.gameObject.SetActive(true);
          nextLevelButton.gameObject.SetActive(false);
          
          ShowHud();
          
          AudioManager.instance.Play("lose");
     }

     private void ShowHud()
     {
          mainPanel.gameObject.SetActive(true);
          
          blurPanel.SetActive(true);
          BlurManager.SetBlur(true);

          Giroscope.GravityMultiplier = 0;
          Accelerometr.CanDash = false;
          player.SetConstraintsFalse();
          
          //EventSystem.current.SetSelectedGameObject(null);
          
          mainPanel.DOJump(mainPanelDestination.position, 4, 4, 1.5f);
     }

     public void LoadMainMenu()
     {
          AudioManager.instance.Play("click");
          PlayerMainBone.AllowCollision = true;
          Giroscope.GravityMultiplier = 1;
          Accelerometr.CanDash = true;
          player.Immune1 = true;
          player.SetConstraintsFalse();
          
          BlurManager.renderTexture.Release();

          transition.DOFade(1, 1).onComplete = () => Core.LoadScene("MainMenu");
     }

     public void TryAgain()
     {
          AudioManager.instance.Play("click");
          transition.DOFade(1, 1).onComplete = () =>
          {
               HideScreen();
               //Core.LoadCurrentSceneAgain();
               Accelerometr.onAccelerometrActivate.RemoveAllListeners();
               
               Destroy(player.gameObject);
               
               player = Instantiate(Settings.player, Settings.playerStartPos, Quaternion.identity).GetComponent<Player>();
               Accelerometr.onAccelerometrActivate.AddListener(player.PushBones);
               
               Settings.cameraFollow.what = player.GetComponentInChildren<PlayerMainBone>().transform;
               Settings.cameraFollow.transform.position = CameraFollow.CameraStartPos;

               CameraFollow.FollowY = InitialFollow.FollowY;
               CameraFollow.FollowX = InitialFollow.FollowX;

               transition.DOFade(0, 1).onComplete = () =>
               {
                    PlayerMainBone.AllowCollision = true;
                    Giroscope.GravityMultiplier = 1;
                    Accelerometr.CanDash = true;
                    player.SetConstraintsTrue();
                    player.Immune1 = false;
               };
          };
          
     }

     public void LoadNextLevel()
     {
          AudioManager.instance.Play("click");
          BlurManager.renderTexture.Release();

          PlayerMainBone.AllowCollision = true;
          Giroscope.GravityMultiplier = 1;
          Accelerometr.CanDash = true;
          player.SetConstraintsFalse();
          
          LevelPicker.CurrentLevel = (Convert.ToInt16(LevelPicker.CurrentLevel) + 1).ToString();

          string toLoad = WorldChoose.chosenWorldName + "_" + LevelPicker.CurrentLevel;
          
          transition.DOFade(1, 1).onComplete = () => Core.LoadScene(toLoad);
     }
} 
