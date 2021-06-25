namespace Exam.Controllers
{
    using System.Linq;
    using System.Web;
    using ExamHelperLibrary.Data;
    using ExamHelperLibrary.Data.Models;
    using ExamHelperLibrary.Template;
    using FrameworkServer.Controllers;
    using FrameworkServer.Http;
    using ViewModels;

    public class CardsController : BaseControllerTemplate
    {
        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context) =>
            _context = context;

        [Authorize]
        public HttpResponse All()
        {
            var cardsQuery = _context.Cards
                .Where(x => !_context.UserCards.Any(uc => uc.CardId == x.Id && uc.UserId == User.Id))
                .Select(x => new Card()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Keyword = x.Keyword,
                    Attack = x.Attack,
                    Health = x.Health,
                    ImageUrl = x.ImageUrl,
                })
                .ToList();

            return View(cardsQuery);
        }

        public HttpResponse Add() => 
            View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(CreadCardViewModel model)
        {
            var body = Request.Body;
            var parse = HttpUtility.ParseQueryString(body);
            var obj = new CreadCardViewModel
            {
                Name = parse["name"],
                Image = HttpUtility.UrlDecode(parse["image"]),
                Description = parse["description"],
                Keyword = parse["keyword"],
                Attack = int.Parse(parse["attack"]),
                Health = int.Parse(parse["health"])
            };

            if (!ModelIsVlaid(obj))
            {
                return Error(Errors);
            }

            if (_context.Cards.Any(x => x.Name == obj.Name))
            {
                Errors.Add("This card name already exist.");
            }

            if (Errors.Any())
            {
                return Error(Errors);
            }

            var card = new Card
            {
                Name = obj.Name,
                ImageUrl = obj.Image,
                Description = obj.Description,
                Keyword = obj.Keyword,
                Attack = obj.Attack,
                Health = obj.Health,
            };

            _context.Cards.Add(card);
            _context.SaveChanges();

            return AddToCollection(card.Id.ToString());
        }

        [Authorize]
        public HttpResponse Collection()
        {
            var cards = _context
                .Cards
                .Where(x => x.UserCards.Any(u => u.UserId == User.Id))
                .Select(x => new Card()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Keyword = x.Keyword,
                    Attack = x.Attack,
                    Health = x.Health,
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            return View(cards);
        }

        [Authorize]
        public HttpResponse AddToCollection(string cardId)
        {
            var userCard = GetUserCardFromDatabase(cardId);

            if (userCard != null)
            {
                return Refresh();
            }

            userCard = new UserCard
            {
                CardId = int.Parse(cardId),
                UserId = User.Id
            };

            _context.UserCards.Add(userCard);
            
            if (_context.SaveChanges() > 0)
            {
                return Redirect("/Cards/All");
            }

            return Error("Card is not added on the collection.");
        }
        
        [Authorize]
        public HttpResponse RemoveFromCollection(string cardId)
        {
            var userCard = GetUserCardFromDatabase(cardId);

            if (userCard == null)
            {
                return Error("Card is not exist in your collection.");
            }

            _context.UserCards.Remove(userCard);

            if (_context.SaveChanges() > 0)
            {
                return Redirect("/Cards/All");
            }

            return Error("Card is not removed from the collection.");
        }

        private UserCard GetUserCardFromDatabase (string cardId) =>
            _context
                .UserCards
                .FirstOrDefault(x =>
                    x.UserId == User.Id && x.CardId == int.Parse(cardId));
    }
}