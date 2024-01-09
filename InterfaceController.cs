using FMOD;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : IUIController
{
    
    private KeyboardControls keyboardControl;
    private Image playerHealthBar;
    private int resources;
    private int ammo = -1;

    private TextMeshProUGUI _resourceText;
    private TextMeshProUGUI _ammoText;
    private Animator animator;

    private readonly string scroller_weapon = "Weapon_Scroller_Anim";


				public InterfaceController ()
    {
        
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="keyboardControl">KeyabordControls</param>
    /// <param name="playerHealthBar">Image</param>
    /// <param name="_resourceText">TextMeshProUGUI</param>
    /// <param name="_ammoText">TextMeshProUGUI</param>
    /// <param name="animator">Animator</param>
    public InterfaceController (KeyboardControls keyboardControl, Image playerHealthBar, TextMeshProUGUI _resourceText, TextMeshProUGUI _ammoText, Animator animator)
    {
        this.keyboardControl = keyboardControl;
        this.playerHealthBar = playerHealthBar;
        this._resourceText = _resourceText;
        this._ammoText = _ammoText;
        this.animator = animator;

    }

    public override void Init()
    {
        if (ammo < 0) _ammoText.text = "∞";
        //SetAmmo(ammo);
				}

    public override int GetAmmo()
    {
        return ammo;
    }

    public override int GetResources()
    {
        return resources;
    }

    public override void SetAmmo(int ammo)
    {
        this.ammo = ammo;
        if(ammo < 0)
        {
            _ammoText.text = "∞";
            return;
								}
        _ammoText.text = ammo.ToString();
    }

   
    public override void SetResources(int resources)
    {
        this.resources = resources;
        _resourceText.text = resources.ToString();
    }

    public override void Animator_Play()
    {
        animator.Play(scroller_weapon);
    }

    public override void DoUpdate()
    {
        if(keyboardControl != null)
        {
												keyboardControl.DetectKey(out KeyboardControls.All_keys key);
												if (key == KeyboardControls.All_keys.PISTOL)
												{
																key = KeyboardControls.All_keys.DEFAULT;
																if (animator != null)
																{
                    Animator_Play();

																}

												}
												else if (key == KeyboardControls.All_keys.SHOTGUN)
												{
																key = KeyboardControls.All_keys.DEFAULT;
																if (animator != null)
																{
																				Animator_Play();

																}

												}
												else if (key == KeyboardControls.All_keys.RIFLE)
												{
																key = KeyboardControls.All_keys.DEFAULT;
																if (animator != null)
																{
																				Animator_Play();

																}

												}
								}
    }
}
