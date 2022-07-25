using SDG.Unturned;

namespace ExtraConcentratedJuice.BreakAndEnter
{
    public static class Util
    {
        public static string Translate(string TranslationKey, params object[] Placeholders) =>
            BreakAndEnter.instance.Translations.Instance.Translate(TranslationKey, Placeholders);

        public static void ToggleDoor(InteractableDoor door, bool open)
        {
            BarricadeManager.ServerSetDoorOpen(door, open);
        }
    }
}
