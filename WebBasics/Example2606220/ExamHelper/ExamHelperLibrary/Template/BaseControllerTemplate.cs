namespace ExamHelperLibrary.Template
{
    using System.Collections.Generic;
    using System.Linq;
    using FrameworkServer.Controllers;
    using Services;

    public class BaseControllerTemplate : Controller
    {
        private IValidator _attributeValidator;

        protected bool ModelIsVlaid(object obj) =>
            _attributeValidator
                .ModelIsValid(obj);

        protected List<string> Errors => 
            _attributeValidator
                .Errors
                .ToList();

        protected void AddError(string error) => 
            _attributeValidator
                .AddError(error);

        protected BaseControllerTemplate() =>
            _attributeValidator = new Validator();
    }
}