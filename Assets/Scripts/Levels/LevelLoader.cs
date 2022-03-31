using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
    , IPointerEnterHandler, IPointerExitHandler
{

    private Button button;
    public string LevelName;
    public Image image;
    private GameObject gO;
    public Sprite sprite;
    public Sprite closeHltSprite;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);

        this.gO = image.gameObject;
    }

    // Update is called once per frame
    public void onClick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);

        switch (levelStatus) {

            case LevelStatus.Locked:
                Debug.Log(LevelName + " is Locked");
                break;
            case LevelStatus.UnLocked:
                {
                    Debug.Log(LevelName + " is Unlocked");
                    SceneManager.LoadScene(LevelName);
                }
                break;
            case LevelStatus.Completed:
                SceneManager.LoadScene(LevelName);
                break;
        }
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {
        image.enabled = false;
        this.gO.SetActive(false);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        image.enabled = true;
        this.gO.SetActive(true);
    }

    private void Start()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(LevelName);

        if(levelStatus == LevelStatus.Locked)
        {
            button.GetComponent<Image>().sprite = sprite;
        }
        else 
      // if (levelStatus  == "")
        {
            Debug.Log(LevelName + " is Unlocked");

        }
    }

    private void OnMouseOver()
    {
        Debug.Log(gameObject.GetComponent<Button>());

    }

}
