using UnityEngine;
using UnityEngine.UI;

public class livesUI : MonoBehaviour
{

    public Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = PlayerStats.Lives + " Leben";
        
    }
}
