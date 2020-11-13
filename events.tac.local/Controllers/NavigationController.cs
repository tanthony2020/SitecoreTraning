using events.tac.local.Business.Navigation;
using events.tac.local.Models;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TAC.Utils.SitecoreModels;

namespace events.tac.local.Controllers
{
    public class NavigationController : Controller
    {
        private readonly NavigationModelBuilder _builder;
        private readonly RenderingContext _context;
        // GET: Navigation
        public NavigationController(NavigationModelBuilder modelBuilder, RenderingContext context)
        {
            _builder = modelBuilder;
            _context = context;
        }
        public ActionResult Index()
        {
            Item currentItem = _context.ContextItem;//RenderingContext.Current.ContextItem;
            Item section = currentItem.Axes.GetAncestors().FirstOrDefault(i=>i.TemplateName=="Event Section");
            var model = _builder.CreateNavigationMenu(new SitecoreItem(section), new SitecoreItem(currentItem));
            return View(model);
        }
        
    }
}