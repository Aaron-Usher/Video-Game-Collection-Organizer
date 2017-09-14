using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VideoGameDataObjects;
using VideoGameLogicLayer;
using VideoGameMVCLayer.Models;

namespace VideoGameMVCLayer.Controllers
{
    //Default MVC tables kind of suck. As such, I looked into how to easily sort and paginate tables, and found the following:
    //http://www.itworld.com/article/2956575/development/how-to-sort-search-and-paginate-tables-in-asp-net-mvc-5.html
    

    //Only a logged in user should be using this.
    [Authorize]
    public class GamesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// When you click the edit button, the game as it was gets stored here, to be referenced later for concurrency checks.
        /// </summary>
        public static Game _gameInEdit;

        [AllowAnonymous]
        // GET: Games
        public ActionResult Index(string sortOrder = "name", string inQuery = "", int? pageSize = 5, int page = 1, bool adminApprove = false)
        {
            ViewBag.searchQuery = string.IsNullOrEmpty(inQuery) ? ViewBag.searchQuery : inQuery;
            inQuery = ViewBag.searchQuery ?? "";
            page = page > 0 ? page : 1;
            

            //The default is 5 solely to show that pagination exists; I would probably set it to 25 in a real application, or, more even more likely,
            //actualy figure out how to let the user choose their own.
            if (pageSize == null)
            {
                pageSize = ViewBag.PageSize ?? 5;
            }
            else
            {
                pageSize = pageSize > 0 ? pageSize : 5;
            }
            
            ViewBag.PageSize = pageSize;


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

            IEnumerable<Game> games = new List<Game>();

            try
            {
               games = GameManager.RetrieveGamesWithOwnershipStatus(User.Identity.Name).Where(g =>
               g.Console.Contains(inQuery) ||
               g.Developer.Contains(inQuery) ||
               g.Name.Contains(inQuery) ||
               g.Publisher.Contains(inQuery));

                if (adminApprove)
                {
                    games = GameManager.RetrieveGames(false);
                }
            }
            catch (Exception)
            {
                throw;
                
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
        public ActionResult Details(int? id)
        {
            Game game = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                game = GameManager.RetrieveGame(id.Value);
                ViewBag.IsOwned = game.IsOwned = GameManager.CheckUserGame(game.Id, User.Identity.Name);

            }
            catch (Exception)
            {

            
            }
            if (game == null)
            {
                return HttpNotFound();
            }


            
            
            return View(game);
        }

        [HttpPost, ActionName("Details")]
        public ActionResult DetailsConfirmed(int id)
        {

            Game game = null;
            try
            {
                game = GameManager.RetrieveGame(id);
                ViewBag.IsOwned = game.IsOwned = GameManager.CheckUserGame(game.Id, User.Identity.Name);
                GameManager.ToggleUserGame(id, User.Identity.Name);
            }
            catch (Exception)
            {

               
            }
            

            if (game == null)
            {
                return HttpNotFound();
            }
           
            return RedirectToAction("Index");
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.Consoles = ConsoleManager.RetrieveConsoles();
                ViewBag.Developers = DeveloperManager.RetrieveDevelopers();
                ViewBag.Publishers = PublisherManager.RetrievePublishers();
            }
            catch (Exception)
            {
                
            }
            
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Console, Developer, Publisher")] Game game)
        {
            if (ModelState.IsValid)
            {
                bool admin = false;
                if (User.IsInRole("Administrator"))
                {
                    admin = true;
                }
                try
                {
                    GameManager.CreateGame(game, User.Identity.Name, admin);
                }
                catch (Exception)
                {
                    
                }

                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            Game game = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                game = GameManager.RetrieveGame(id.Value);
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
            _gameInEdit = game;

            try
            {
                ViewBag.Consoles = ConsoleManager.RetrieveConsoles();
                ViewBag.Developers = DeveloperManager.RetrieveDevelopers();
                ViewBag.Publishers = PublisherManager.RetrievePublishers();
            }
            catch (Exception)
            {
                
               
            }
            
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Console,Developer,Publisher,User")] Game game)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    GameManager.UpdateGame(_gameInEdit, game);
                }
                catch (Exception)
                {

                    
                }   
                  
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            Game game = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                game = GameManager.RetrieveGame(id.Value);
            }
            catch (Exception)
            {

                throw;
            }
            
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                GameManager.DeleteGame(id);
            }
            catch (Exception)
            {

            }
            
            return RedirectToAction("Index");
        }
    }
}
