using UnityEngine;

public class ColorChangeOnDamage : MonoBehaviour
{
    public Color damagedColor = Color.red;
    public SpriteRenderer gfx;

    public float recoverSpeed = 2f;

    private float damagedPercent = 0;
    private Color baseColor;

    void Start()
    {
        this.baseColor = this.gfx.color;

        var h = GetComponent<Healt>();
        h.OnDamage += this.OnDamageReceived;
    }

    void Update()
    {
        if (this.damagedPercent >= 0f)
        {
            this.gfx.color = Color.Lerp(this.baseColor, this.damagedColor, this.damagedPercent);
            this.damagedPercent -= Time.deltaTime * this.recoverSpeed;
        }
    }

    void OnDamageReceived(float amount, Vector3 instigatorLocation)
    {
        this.damagedPercent = 1;
    }
}
