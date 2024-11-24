using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtIUController : MonoBehaviour
{
    public GameObject playerOrEnemy; 
    public GameObject heartContainer;
    public GameObject menuContainer;
  

    private Image fillImage;
    private Healt healthComponent;
    private Menu menu;
    

    void Start()
    {
        fillImage = heartContainer.GetComponent<Image>();
        healthComponent = playerOrEnemy.GetComponent<Healt>();
        menu = menuContainer.GetComponent<Menu>();
        
    }

    void Update()
    {
         float fillValue = healthComponent.GetHealthPercent();
         fillImage.fillAmount = fillValue ;
        if (fillValue == 0.0f)
        {
            menu.FinJuego();
        }
    }
}
