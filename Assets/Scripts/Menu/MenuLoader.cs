using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuLoader : MonoBehaviour
     , IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    private GameObject gO;
    private Button button;
    public MenuStatus menuType;
    public GameObject gameMainMenu;
    public GameObject gameLevels;
    private int currentScene;


    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
        this.gO = image.gameObject;
    }

    public void onClick()
    {
        Debug.Log(menuType + " Button Cliked ");
        switch (menuType)
        {
            case MenuStatus.Start:
                {
                    FindObjectOfType<ButtonAudioController>().Play(SoundType.StartButton);
                    SceneManager.LoadScene(currentScene + 1);
                }
                break;
            case MenuStatus.Levels:
                {
                    gameMainMenu.SetActive(false);
                    gameLevels.SetActive(true);
                    image.enabled = false;
                    this.gO.SetActive(false);
                    FindObjectOfType<ButtonAudioController>().Play(SoundType.ButtonClick);
                }
                break;
            case MenuStatus.Exit:
                Application.Quit();
                break;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        image.enabled = false;
        this.gO.SetActive(false);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        image.enabled = true;
        this.gO.SetActive(true);
        FindObjectOfType<ButtonAudioController>().Play(SoundType.ButtonHover);

    }
}
