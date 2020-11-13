using events.tac.local.Models;
using Sitecore.Data.Items;
using Sitecore.Links;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TAC.Utils.Abstractions;
using TAC.Utils.TestModels;

namespace events.tac.local.Business.Navigation
{
    public class NavigationModelBuilder
    {
        public NavigationMenu CreateNavigationMenu(IItem root, IItem current)
        {
            NavigationMenu menu = new NavigationMenu()
            {
                Title = root.DisplayName,
                URL = root.GetUrl(),
                //URL = LinkManager.GetItemUrl(root),
                Children = root.IsAncestorOf(current) ? root.GetChildren().Select(i => CreateNavigationMenu(i, current)) : null
            };

            return menu;
        }
    }
}