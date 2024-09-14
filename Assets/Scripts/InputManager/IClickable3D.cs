public partial class InputManager
{
    public interface IClickable3D
    {
        public void Click(InputRaycastResult clickInfo);
        public void Hover(InputRaycastResult hoverInfo);
    }
}
