using Member.Core;
using Member.PSB.Code.Events;
using UnityEngine;

namespace Member.KJH.Code.Player
{
    public class StatSave : MonoBehaviour
    {
        private const string SETMAXHP_PATH = "MAX_HP";
        private const string SETHP_PATH = "HP";
        private const string SETATTACK_PATH = "ATTACK";
        private const string SETSPEED_PATH = "SPEED";

        private void OnEnable()
        {
            Bus<AtkUpgradeEvent>.OnEvent += HandleSetAttack;
            Bus<HpUpgradeEvent>.OnEvent += HandleSetHealth;
            Bus<SpeedUpgradeEvent>.OnEvent += HandleSetSpeed;
        }

        private void OnDestroy()
        {
            Bus<AtkUpgradeEvent>.OnEvent -= HandleSetAttack;
            Bus<HpUpgradeEvent>.OnEvent -= HandleSetHealth;
            Bus<SpeedUpgradeEvent>.OnEvent -= HandleSetSpeed;
        }

        private void HandleSetHealth(HpUpgradeEvent evt)
        {
            PlayerPrefs.SetInt(SETMAXHP_PATH, evt.upgradeValue);
            PlayerPrefs.SetInt(SETHP_PATH, evt.upgradeValue);
        }

        private void HandleSetAttack(AtkUpgradeEvent evt)
        {
            PlayerPrefs.SetInt(SETATTACK_PATH, evt.upgradeValue);
        }

        private void HandleSetSpeed(SpeedUpgradeEvent evt)
        {
            PlayerPrefs.SetInt(SETSPEED_PATH, evt.upgradeValue);
        }
    }
}