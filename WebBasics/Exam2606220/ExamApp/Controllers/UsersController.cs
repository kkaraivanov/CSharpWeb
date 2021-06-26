namespace SharedTrip.Controllers
{
    using System.Linq;
    using ExamHelperLibrary.Data;
    using ExamHelperLibrary.Data.Models;
    using ExamHelperLibrary.Services;
    using ExamHelperLibrary.Template;
    using FrameworkServer.Controllers;
    using FrameworkServer.Http;
    using ViewModels.UserModels;

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

        public HttpResponse Register() =>
            View();

        [HttpPost]
        public HttpResponse Login(UserLoginViewModel model)
        {
            if (!ModelIsVlaid(model))
            {
                return Error(Errors.ToList());
            }

            var user = _context.Users.FirstOrDefault(x =>
                x.Username == model.Username &&
                x.Password == _passwordHasher.HashPassword(model.Password));

            if (user == null)
            {
                return Redirect("/Users/Register");
            }

            if (string.IsNullOrEmpty(user.Id))
            {
                Errors.Add("Invalid Username or Password");
            }

            if (Errors.Any())
            {
                return Error(Errors);
            }

            SignIn(user.Id);

            return Redirect("/Trips/All");
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterViewModel model)
        {
            if (!ModelIsVlaid(model))
            {
                return Error(Errors.ToList());
            }

            var passwordIsValid = model.Password.Equals(model.ConfirmPassword);
            var isUserExist = _context.Users.Any(x =>
                x.Username == model.Username &&
                x.Password == _passwordHasher.HashPassword(model.Password));
            var isEmailAddressExist = _context.Users.Any(x => model.Email.Equals(x.Email));

            if (!passwordIsValid)
            {
                Errors.Add("Password does not match with ConfirmPassword");
            }

            if (isUserExist)
            {
                Errors.Add("User is registered");
            }

            if (isEmailAddressExist)
            {
                Errors.Add("Exist other user with this email address.");
            }

            if (Errors.Any())
            {
                return Error(Errors);
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