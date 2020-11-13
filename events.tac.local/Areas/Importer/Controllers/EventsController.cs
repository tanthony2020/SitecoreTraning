using events.tac.local.Areas.Importer.Models;
using Newtonsoft.Json;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace events.tac.local.Areas.Importer.Controllers
{
    public class EventsController : Controller
    {
        // GET: Importer/Events
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, string parentPath)
        {
            IEnumerable<Event> events = null;
            string message = null;
            using (var reader = new StreamReader(file.InputStream))
            {
                var contents = reader.ReadToEnd();
                try
                {
                    events = JsonConvert.DeserializeObject<IEnumerable<Event>>(contents);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            Database database = Sitecore.Configuration.Factory.GetDatabase("master");
            Item parentItem = database.GetItem(parentPath);
            TemplateID templateID = new TemplateID(new ID("{B24333BC-4FD0-43F9-BB4F-A9166EC0968D}"));
            using (new SecurityDisabler())
            {
                foreach (var ev in events)
                {
                    string name = ItemUtil.ProposeValidItemName(ev.Name);
                    Item item = parentItem.Add(name, templateID);
                    item.Editing.BeginEdit();
                    //assign values for all the fields, for example for ContentHeading:
                    item["ContentHeading"] = ev.ContentHeading;
                    item[FieldIDs.Workflow] = "{40EE4287-A777-4B84-8056-4DF0291AFB71}";
                    item[FieldIDs.WorkflowState] = "{3032ED70-3876-4DB8-9D0E-C93968B92C0B}";
                    item.Editing.EndEdit();
                }
            }
            return View();
        }
    }
}