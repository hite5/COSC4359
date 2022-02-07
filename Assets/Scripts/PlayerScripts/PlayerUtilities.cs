using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerUtilities
{
    private Player player;

    [System.Serializable]
    public struct InputButton
    {
        [StringInList("mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string UsePrimary;
        [StringInList("mouse 1", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string ADSOrSecondary;
        [StringInList("left shift", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string Sprint;
        [StringInList("space", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string Dash;
        [StringInList("e", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string UseItem;
        [StringInList("q", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string UseVaccine;
        [StringInList("r", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string Reload;
        [StringInList("1", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string Weapon1;
        [StringInList("2", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string Weapon2;
        [StringInList("3", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string Weapon3;
        [StringInList("4", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string MeleeWeapon;
        [StringInList("g", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string ThrowGrenade;
        [StringInList("5", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string SwitchGrenade;
        [StringInList("escape", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string Pause;
        [StringInList("f", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string Interact;
        /*
        [StringInList("t", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string DualWieldToggle;
        [StringInList("y", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string PickUpSecondary;
        */
        [StringInList("mouse 2", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string RallyGlobin;
    }

    public bool UsePrimaryPressed { get; set; }
    public bool ADSOrSecondaryPressed { get; set; }
    public bool SprintButtonPressed { get; set; }
    public bool DashButtonPressed { get; set; }
    public bool Weapon1ButtonPressed { get; set; }
    public bool Weapon2ButtonPressed { get; set; }
    public bool Weapon3ButtonPressed { get; set; }
    public bool MeleeButtonPressed { get; set; }
    public bool GrenadeThrowButtonPressed { get; set; }
    public bool GrenadeSwitchButtonPressed { get; set; }
    public bool ReloadButtonPressed { get; set; }
    public bool EscapeButtonPressed { get; set; }
    public bool InteractButtonPressed { get; set; }
    public bool DualWieldButtonPressed { get; set; }
    public bool ItemButtonPressed { get; set; }
    public bool VaccineButtonPressed { get; set; }
    public bool PickUpSecondaryButtonPressed { get; set; }
    public bool GlobinRallyButtonPressed { get; set; }

    public InputButton PlayerInputs;

    public PlayerUtilities(Player player)
    {
        this.player = player;
        PlayerInputs = player.Utilities.PlayerInputs;
    }

    public void HandleInput()
    {
        if (GlobalPlayerVariables.EnablePlayerControl)
            player.Stats.Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        else
            player.Stats.Direction = Vector2.zero;
        player.Stats.Position = player.Components.PlayerRidgitBody.position;
        player.Stats.Angle = Mathf.Atan2(player.References.MousePosToPlayer.y, player.References.MousePosToPlayer.x) * Mathf.Rad2Deg;
        UsePrimaryPressed = Input.GetKey(PlayerInputs.UsePrimary.ToLower());
        ADSOrSecondaryPressed = Input.GetKey(PlayerInputs.ADSOrSecondary.ToLower());
        SprintButtonPressed = Input.GetKey(PlayerInputs.Sprint.ToLower());
        DashButtonPressed = Input.GetKeyDown(PlayerInputs.Dash.ToLower());
        Weapon1ButtonPressed = Input.GetKeyDown(PlayerInputs.Weapon1.ToLower());
        Weapon2ButtonPressed = Input.GetKeyDown(PlayerInputs.Weapon2.ToLower());
        Weapon3ButtonPressed = Input.GetKeyDown(PlayerInputs.Weapon3.ToLower());
        MeleeButtonPressed = Input.GetKeyDown(PlayerInputs.MeleeWeapon.ToLower());
        GrenadeThrowButtonPressed = Input.GetKeyDown(PlayerInputs.ThrowGrenade.ToLower());
        GrenadeSwitchButtonPressed = Input.GetKeyDown(PlayerInputs.SwitchGrenade.ToLower());
        ReloadButtonPressed = Input.GetKeyDown(PlayerInputs.Reload.ToLower());
        EscapeButtonPressed = Input.GetKeyDown(PlayerInputs.Pause.ToLower());
        InteractButtonPressed = Input.GetKeyDown(PlayerInputs.Interact.ToLower());
        //DualWieldButtonPressed = Input.GetKeyDown(PlayerInputs.DualWieldToggle.ToLower());
        ItemButtonPressed = Input.GetKeyDown(PlayerInputs.UseItem.ToLower());
        VaccineButtonPressed = Input.GetKeyDown(PlayerInputs.UseVaccine.ToLower());
        //PickUpSecondaryButtonPressed = Input.GetKeyDown(PlayerInputs.PickUpSecondary.ToLower());
        GlobinRallyButtonPressed = Input.GetKeyDown(PlayerInputs.RallyGlobin.ToLower() == "" ? "mouse 2" : PlayerInputs.RallyGlobin.ToLower());
    }
}
