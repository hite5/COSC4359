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
        [StringInList("t", "mouse 0", "mouse 1", "tab", "space", "up", "down", "right", "left",
        "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "escape",
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t",
        "u", "v", "w", "x", "y", "z", "numlock", "caps lock", "scroll lock", "right shift", "left shift", "right ctrl",
        "left ctrl", "right alt", "left alt")]
        public string DualWieldToggle;

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
        UsePrimaryPressed = Input.GetKey(PlayerInputs.UsePrimary.ToLower()) ? true : false;
        ADSOrSecondaryPressed = Input.GetKey(PlayerInputs.ADSOrSecondary.ToLower()) ? true : false;
        SprintButtonPressed = Input.GetKey(PlayerInputs.Sprint.ToLower()) ? true : false;
        Weapon1ButtonPressed = Input.GetKey(PlayerInputs.Weapon1.ToLower()) ? true : false;
        Weapon2ButtonPressed = Input.GetKey(PlayerInputs.Weapon2.ToLower()) ? true : false;
        Weapon3ButtonPressed = Input.GetKey(PlayerInputs.Weapon3.ToLower()) ? true : false;
        MeleeButtonPressed = Input.GetKey(PlayerInputs.MeleeWeapon.ToLower()) ? true : false;
        GrenadeThrowButtonPressed = Input.GetKey(PlayerInputs.ThrowGrenade.ToLower()) ? true : false;
        GrenadeSwitchButtonPressed = Input.GetKey(PlayerInputs.SwitchGrenade.ToLower()) ? true : false;
        ReloadButtonPressed = Input.GetKey(PlayerInputs.Reload.ToLower()) ? true : false;
        EscapeButtonPressed = Input.GetKey(PlayerInputs.Pause.ToLower()) ? true : false;
        InteractButtonPressed = Input.GetKey(PlayerInputs.Interact.ToLower()) ? true : false;
        DualWieldButtonPressed = Input.GetKey(PlayerInputs.DualWieldToggle.ToLower()) ? true : false;
        ItemButtonPressed = Input.GetKey(PlayerInputs.UseItem.ToLower()) ? true : false;
        VaccineButtonPressed = Input.GetKey(PlayerInputs.UseVaccine.ToLower()) ? true : false;
    }
}
