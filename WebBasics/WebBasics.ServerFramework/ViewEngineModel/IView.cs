namespace WebBasics.ServerFramework.ViewEngineModel
{
    public interface IView
    {
        string Execute(object viewModel, string user);
    }
}