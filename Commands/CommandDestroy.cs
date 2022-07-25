using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Framework.Utilities;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

namespace ExtraConcentratedJuice.BreakAndEnter
{
    public class CommandDestroy : IRocketCommand
    {
        #region Properties
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "destroy";

        public string Help => "Destroys the barricade or structure that you are looking at.";

        public string Syntax => "/destroy";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "breakandenter.destroy" };
        #endregion

        public void Execute(IRocketPlayer caller, string[] args)
        {
            Player player = ((UnturnedPlayer)caller).Player;
            PlayerLook look = player.look;

            if (PhysicsUtility.raycast(new Ray(look.aim.position, look.aim.forward), out RaycastHit hit, Mathf.Infinity, RayMasks.BARRICADE_INTERACT | RayMasks.STRUCTURE))
            {
                Interactable2SalvageBarricade barri = hit.transform.GetComponent<Interactable2SalvageBarricade>();
                Interactable2SalvageStructure struc = hit.transform.GetComponent<Interactable2SalvageStructure>();

                if (barri != null)
                {
                    BarricadeDrop barr = BarricadeManager.FindBarricadeByRootTransform(barri.root);
                    var br = BarricadeManager.tryGetRegion(barri.root, out var x, out var y, out var plant, out _);

                    if (!br)
                    {
                        UnturnedChat.Say(caller, Util.Translate("invalid_destroy"));
                        return;
                    }

                    BarricadeManager.destroyBarricade(barr, x, y, plant);

                    UnturnedChat.Say(caller, Util.Translate("barricade_removed"));
                }
                else if (struc != null)
                {
                    StructureDrop SD = StructureManager.FindStructureByRootTransform(hit.transform);
                    StructureManager.tryGetRegion(hit.transform, out var x, out var y, out StructureRegion sr);
                    if (sr == null)
                    {
                        UnturnedChat.Say(caller, Util.Translate("invalid_destroy"));
                        return;
                    }

                    StructureManager.destroyStructure(SD, x, y, Vector3.up);

                    UnturnedChat.Say(caller, Util.Translate("structure_removed"));
                }
                else
                {
                    UnturnedChat.Say(caller, Util.Translate("invalid_destroy"));
                }
            }
            else
            {
                UnturnedChat.Say(caller, Util.Translate("no_object"));
            }
        }
    }
}