using BlazorAppC2Corridor.Enums;

namespace ModifyLayoutExample.Services
{
    public class ViewTailoringService
    {

        private ToggledElements toggledElements = ToggledElements.None;
  
        public Action<ToggledElements>? OnChanged { get; set; }

        
        private void NotifyStateChanged() => OnChanged?.Invoke(toggledElements);

        
        public void ToggleElement(ToggledElements elementToToggle)
        {
            if ((toggledElements & elementToToggle) == elementToToggle)
            {
                toggledElements = toggledElements & ~elementToToggle;
            }
            else
            {
                toggledElements = toggledElements | elementToToggle;
            }

            this.NotifyStateChanged();
        }

        public string GetDisplayStatus(ToggledElements elementToToggle)
        {
            var toggled = (toggledElements & elementToToggle) == elementToToggle;
            if (toggled)
            {
                return "d-none";
            }
            return string.Empty;
        }
    }
}
