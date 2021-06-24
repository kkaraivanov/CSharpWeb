namespace Exam.Controllers
{
    using System.Linq;
    using ExamHelperLibrary.Data;
    using ExamHelperLibrary.Data.Models;
    using ExamHelperLibrary.Services;
    using ExamHelperLibrary.Template;
    using FrameworkServer.Controllers;
    using FrameworkServer.Http;
    using ViewModels;

    public class UsersController : BaseControllerTemplate
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public HttpResponse Login() => 
            View();

        [HttpPost]
        public HttpResponse Login(UserLoginModel model)
        {
            var validator = AttributeValidator.Validate(model);
            var user = _context.Users.FirstOrDefault(x =>
                x.Username == model.Username &&
                x.Password == _passwordHasher.HashPassword(model.Password));

            if (user == null)
            {
                return Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(user.Id))
            {
                validator.Errors.Add("Invalid Username or Password");
            }

            if (validator.Errors.Any())
            {
                return Error(validator.Errors);
            }

            SignIn(user.Id);

            return Redirect("/");
        }

        public HttpResponse Register() => 
            View();

        [HttpPost]
        public HttpResponse Register(UserRegisterViewModel model)
        {
            var validator = AttributeValidator.Validate(model);
            var passwordIsValid = model.Password.Equals(model.ConfirmPassword);
            var isUserExist = _context.Users.Any(x =>
                    x.Username == model.Username &&
                    x.Password == _passwordHasher.HashPassword(model.Password));
            var isEmailAddressExist = _context.Users.Any(x => model.Email.Equals(x.Email));

            if (!passwordIsValid)
            {
                validator.Errors.Add("Password does not match with ConfirmPassword");
            }

            if (isUserExist)
            {
                validator.Errors.Add("User is registered");
            }

            if (isEmailAddressExist)
            {
                validator.Errors.Add("Exist other user with this email address.");
            }

            if (validator.Errors.Any())
            {
                return Error(validator.Errors);
            }

            _context.Users.Add(CreateUser(model));
            _context.SaveChanges();

            return Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            SignOut();

            return Redirect("/");
        }

        private User CreateUser(UserRegisterViewModel model) =>
            new User
            {
                Username = model.Username,
                Email = model.Email,
                Password = _passwordHasher.HashPassword(model.Password),
            };
    }
}