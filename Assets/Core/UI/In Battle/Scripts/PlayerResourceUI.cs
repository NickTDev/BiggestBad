// Merle Roji

using UnityEngine;
using T02.Characters;
using T02.Characters.InBattle;

namespace T02.UI
{
    public class PlayerResourceUI : CharacterResourceUI
    {
        [SerializeField] private PlayerPartyPosition _partyPosition;

        protected override void Init()
        {
            base.Init();
            if (_turnManager == null) return;
        }

        protected override void TickUI()
        {
            base.TickUI();
            if (_turnManager == null) return;

            if (_turnManager.PlayerParty.Count > 0)
            {
                if ((int)_partyPosition < _turnManager.PlayerParty.Count)
                {
                    CharacterBattleController player = _turnManager.PlayerParty[(int)_partyPosition];

                    if (player != null)
                    {
                        _nameText.text = player.Stats.Name;

                        _hpNumberText.text = player.Stats.CurrentHP + "/" + player.Stats.MaxHP;
                        _hpSlider.maxValue = player.Stats.MaxHP;
                        _hpSlider.value = player.Stats.CurrentHP;

                        _epNumberText.text = player.Stats.CurrentEnergy + "/" + player.Stats.MaxEnergy;
                        _epSlider.maxValue = player.Stats.MaxEnergy;
                        _epSlider.value = player.Stats.CurrentEnergy;

                        _moveNumberText.text = player.Stats.CurrentMovement + "/" + player.Stats.BaseSpeed;
                        _moveSlider.maxValue = player.Stats.BaseSpeed;
                        _moveSlider.value = player.Stats.CurrentMovement;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}