namespace ExamHelperLibrary.Template
{
    using System.Collections.Generic;
    using System.Linq;
    using FrameworkServer.Controllers;
    using Services;

    public class BaseControllerTemplate : Controller
    {
        private IValidator _validator;

        protected bool ModelIsVlaid(object obj) =>
            _validator
                .ModelIsValid(obj);

        protected List<string> Errors => 
            _validator
                .Errors
                .ToList();

        protected void AddError(string error) => 
            _validator
                .AddError(error);

        protected BaseControllerTemplate() =>
            _validator = new Validator();
    }
}