
public enum EFebucciBehavior
{
    WIGGLE, SHAKE, RAINB, FADE, WAVE, ROT, SWING, INCR, SLIDE, BOUNCE, SIZE, NONE
}

public enum EFebucciAppearance
{
    OFFSET, SIZE, FADE, VERTEXP, HORIEXP, DIAGEXP, ROT, NONE
}

public class FebucciHelper
{
    public static string AddBehaviourEffects(string str, params EFebucciBehavior[] effects)
    {
        string finalString = str;
        foreach(EFebucciBehavior e in effects)
        {
            if(e == EFebucciBehavior.NONE)
            {
                continue;
            }

            string effectStr = e.ToString().ToLower();
            finalString = string.Format("<{0}>{1}</{0}>", effectStr, finalString);
        }

        return finalString;
    }

    public static string AddAppearanceEffects(string str, params EFebucciAppearance[] effects)
    {
        string finalString = str;
        foreach (EFebucciAppearance e in effects)
        {
            if (e == EFebucciAppearance.NONE)
            {
                continue;
            }

            string effectStr = e.ToString().ToLower();
            finalString = string.Format("{{{0}}}{1}{{/{0}}}", effectStr, finalString);
        }

        return finalString;
    }

    public static string AddAppearanceAndBehaviorEffect(string str, EFebucciAppearance[] appearance, EFebucciBehavior[] behavior)
    {
        return AddBehaviourEffects(AddAppearanceEffects(str, appearance), behavior);
    }
}

