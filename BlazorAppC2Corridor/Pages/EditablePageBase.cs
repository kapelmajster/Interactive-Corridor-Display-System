using Microsoft.AspNetCore.Components;
using BlazorAppC2Corridor.Enums;

namespace BlazorAppC2Corridor.Pages
{
    public class EditablePageBase : PageBase
    {
        [Parameter]
        public int Id { get; set; }
        public Mode Mode => Id == 0 ? Mode.Create : Mode.Edit;
    }
}
