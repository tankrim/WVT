namespace WVTApp
{
    public static class ControlExtensions
    {
        public static Task InvokeAsync(this Control control, Action action)
        {
            return Task.Factory.FromAsync(control.BeginInvoke(action), control.EndInvoke);
        }
    }
}