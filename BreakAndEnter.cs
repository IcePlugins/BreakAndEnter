using Rocket.Core.Plugins;
using System.Collections;
using Rocket.API.Collections;
using SDG.Unturned;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace ExtraConcentratedJuice.BreakAndEnter
{
    public class BreakAndEnter : RocketPlugin<BreakAndEnterConfig>
    {
        public static BreakAndEnter instance;
        private float _autoCloseDoorsDelay;

        protected override void Load()
        {
            instance = this;
            _autoCloseDoorsDelay = instance.Configuration.Instance.AutoCloseDoorsDelay / 1000f;
            
            // Hello by name is Sven and your name sucks because it's just three random words combined
            // I had to paraphrase because I couldn't find the picture
            // Now merge my PR already
            //Hello my name is Random
            //FIX YOUR PLUGINS aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
            //but fr tho thanks for your work
            Logger.Log("BreakAndEnter by ExtraConcatenatedJuice loaded");
            Logger.Log("For help please visit https://iceplugins.xyz/BreakAndEnter/");
        }

        public void AutoCloseDoor(InteractableDoor door) => StartCoroutine(_AutoCloseDoor(door));
        
        private IEnumerator _AutoCloseDoor(InteractableDoor door)
        {
            yield return new WaitForSeconds(_autoCloseDoorsDelay);
            Util.ToggleDoor(door, false);
        }

        public override TranslationList DefaultTranslations =>
            new TranslationList
            {
                { "no_object", "No object was found in your line of sight." },
                { "structure_removed", "Structure removed successfully." },
                { "barricade_removed", "Barricade removed successfully." },
                { "invalid_destroy", "The object that you are looking at is not a barricade nor a structure." },
                { "invalid_door", "The object that you are looking at is not a door." },
                { "door_toggle", "Door {0}." },
                { "storage_open", "Opened storage." },
                { "invalid_storage", "The object that you are looking at is not a storage unit." }
            };
    }
}
