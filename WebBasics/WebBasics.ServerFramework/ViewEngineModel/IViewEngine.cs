namespace WebBasics.ServerFramework.ViewEngineModel
{
    using Extensions;

    public interface IViewEngine
    {
        string GetHtml(string template, object model, string user);
    }

    public class ViewEngine : IViewEngine
    {
        public string GetHtml(string template, object model, string user)
        {
            string codeForTemplate = template.GenerateTemplate(model);
            IView vewObject = codeForTemplate.ExecuteView(model);
            string html = vewObject.Execute(model, user);

            return html;
        } 
    }
}