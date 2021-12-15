using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PuzzleGroupRow : MonoBehaviour
{
    public int row;
    
    public Image _iconImage;
    public TextMeshProUGUI _titleText;
    public TextMeshProUGUI _descriptionText;
    
    public delegate void Notify(PuzzleGroupRow group);
    public event Notify OnRowSelectEvent;
    
    public void SetImageFromURL(string url)
    {
        // Debug.Log("SET IMAGE: "+url);
        StartCoroutine(getIconTexture(url));

        _titleText.text = "set the title text";
        _descriptionText.text = "set the description text";
    }

    private IEnumerator getIconTexture(string url)
    {
        var request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log("request error: " + request.error);
        }
        else
        {
            var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            // Debug.Log("request texture: " + texture);
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
            // Debug.Log("newSprite: " + newSprite);
            _iconImage.sprite = newSprite;
        }
    }

    public void DidSelectRow()
    {
        // Debug.Log("RICHIE - DidSelectRow");
        OnRowSelectEvent?.Invoke(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetImageFromURL("https://images.squarespace-cdn.com/content/v1/5616ac17e4b018d366f57f53/1617124584308-358K4SM1P0Q53DZ6B5RD/LOGO+%282%29.png?format=750w");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
