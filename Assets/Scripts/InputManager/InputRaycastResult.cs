using UnityEngine;

public partial class InputManager
{
    public class InputRaycastResult
    {
        public readonly RaycastHit Hit;
        private readonly IClickable3D[] Clickables;

        public void ClickAll()
        {
            foreach (var clickable in Clickables)
            {
                clickable.Click(this);
            }
        }

        public void HoverAll()
        {
            foreach (var clickable in Clickables)
            {
                clickable.Hover(this);
            }
        }

        public InputRaycastResult(RaycastHit hit, IClickable3D[] clickables)
        {
            this.Hit = hit;
            this.Clickables = clickables;
        }
    }
}
