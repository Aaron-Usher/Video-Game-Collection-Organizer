using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoGameDataObjects;
using VideoGameLogicLayer;

namespace VideoGameMVCLayer.Controllers
{
    [Authorize (Roles ="Administrator")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(string sortOrder = "name", string inQuery = "", int? pageSize = 5, int page = 1, bool adminApprove = false)
        {
            ViewBag.searchQuery = string.IsNullOrEmpty(inQuery) ? ViewBag.searchQuery : inQuery;
            inQuery = ViewBag.searchQuery ?? "";
            page = page > 0 ? page : 1;
            //The default is 5 solely to show that pagination exists; I would probably set it to 25 in a real application, or, more even more likely,
            //actualy figure out how to let the user choose their own.
            if (pageSize == null)
            {
                pageSize = ViewBag.PageSize != null ? ViewBag.PageSize : 5;
            }
            else
            {
                pageSize = pageSize > 0 ? pageSize : 5;
            }

            ViewBag.PageSize = pageSize;

            //If I don't explictly set this, the default sort doesn't work right.
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "name";
            }

            //These make sorts swap back and forth by clicking on them more than once.
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.OwnedSortParam = sortOrder == "owned" ? "owned_desc" : "owned";
            ViewBag.ConsoleSortParam = sortOrder == "console" ? "console_desc" : "console";
            ViewBag.DeveloperSortParam = sortOrder == "developer" ? "developer_desc" : "developer";
            ViewBag.PublisherSortParam = sortOrder == "publisher" ? "publisher_desc" : "publisher";


            ViewBag.CurrentSort = sortOrder;

            //This implements a search function, basically, by just calling "contains" with the passed in query
            //on every single part of the object. This makes the massive list much more useable than it has any
            //right to be.
            var games = GameManager.RetrieveGames(active:false).Where(g =>
                g.Console.Contains(inQuery) ||
                g.Developer.Contains(inQuery) ||
                g.Name.Contains(inQuery) ||
                g.Publisher.Contains(inQuery));

            if (adminApprove)
            {
                games = GameManager.RetrieveGames(false);
            }

            switch (sortOrder)
            {
                case "name":
                    games = games.OrderBy(x => x.Name);
                    break;
                case "name_desc":
                    games = games.OrderByDescending(x => x.Name);
                    break;
                case "console":
                    games = games.OrderBy(x => x.Console);
                    break;
                case "console_desc":
                    games = games.OrderByDescending(x => x.Console);
                    break;
                case "developer":
                    games = games.OrderBy(x => x.Developer);
                    break;
                case "developer_desc":
                    games = games.OrderByDescending(x => x.Developer);
                    break;
                case "publisher":
                    games = games.OrderBy(x => x.Publisher);
                    break;
                case "publisher_desc":
                    games = games.OrderByDescending(x => x.Publisher);
                    break;
                case "owned":
                    games = games.OrderBy(x => x.IsOwned);
                    break;
                case "owned_desc":
                    games = games.OrderByDescending(x => x.IsOwned);
                    break;
                default:
                    break;
            }
            return View(games.ToPagedList(page, pageSize.Value));
        }
        // GET: Games/Details/5
        public ActionResult Approve(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = GameManager.RetrieveGame(id.Value);

            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        [HttpPost, ActionName("Approve")]
        public ActionResult ApproveConfirmed(int id)
        {
            try
            {
                Game game = GameManager.RetrieveGame(id);

                if (game == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IsOwned = game.IsOwned = GameManager.CheckUserGame(game.Id, User.Identity.Name);
                GameManager.ApproveGame(id);
            }
            catch (Exception)
            {

                
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Deny(int id)
        {
            try
            {
                Game game = GameManager.RetrieveGame(id);

                if (game == null)
                {
                    return HttpNotFound();
                }
                GameManager.DeleteGame(id);
            }
            catch (Exception)
            {

                
            }
            return RedirectToAction("Index");
        }
    }
}