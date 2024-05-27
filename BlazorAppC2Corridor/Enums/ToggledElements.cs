namespace BlazorAppC2Corridor.Enums
{
    [Flags]
    public enum ToggledElements : byte
    {
        None = 0,
        NavigationBarArea = 1,
        LoginAboutArea = 2,
        AnyOtherRandomArea = 4
    }
}
