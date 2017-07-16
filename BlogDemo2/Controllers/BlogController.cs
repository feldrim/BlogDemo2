using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BlogDemo2.Core;
using BlogDemo2.Core.Objects;
using BlogDemo2.Models;

namespace BlogDemo2.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ViewResult Posts(int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, p);

            ViewBag.Title = "Latest Posts";
            return View("List", viewModel);
        }

        public ViewResult Category(string category, int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, category, "Category", p);

            if (viewModel.Category == null)
                throw new HttpException(404, "Category not found");

            ViewBag.Title = $@"Latest posts on category ""{viewModel.Category.Name}""";
            return View("List", viewModel);
        }

        public ViewResult Tag(string tag, int p = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, tag, "Tag", p);

            if (viewModel.Tag == null)
                throw new HttpException(404, "Tag not found");

            ViewBag.Title = $@"Latest posts tagged on ""{viewModel.Tag.Name}""";
            return View("List", viewModel);
        }

        public ViewResult Search(string s, int p = 1)
        {
            ViewBag.Title = $@"Lists of posts found
                        for search text ""{s}""";

            var viewModel = new ListViewModel(_blogRepository, s, "Search", p);
            return View("List", viewModel);
        }

        public ViewResult Post(int year, int month, string title)
        {
            var post = _blogRepository.Post(year, month, title);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");

            return View(post);
        }

        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(_blogRepository);
            return PartialView("_Sidebars", widgetViewModel);
        }

        public ActionResult BadAction()
        {
            throw new Exception("You forgot to implement this action!");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                using (var client = new SmtpClient())
                {
                    var adminEmail = ConfigurationManager.AppSettings["AdminEmail"];
                    var from = new MailAddress(adminEmail, "JustBlog Messenger");
                    var to = new MailAddress(adminEmail, "JustBlog Admin");

                    using (var message = new MailMessage(from, to))
                    {
                        message.Body = contact.Body;
                        message.IsBodyHtml = true;
                        message.BodyEncoding = Encoding.UTF8;

                        message.Subject = contact.Subject;
                        message.SubjectEncoding = Encoding.UTF8;

                        message.ReplyTo = new MailAddress(contact.Email);

                        client.Send(message);
                    }
                }

                return View("Thanks");
            }

            return View();
        }
    }
}