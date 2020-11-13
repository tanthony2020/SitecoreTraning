﻿using System;
using events.tac.local.Business.Navigation;
using events.tac.local.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TAC.Utils.Abstractions;
using TAC.Utils.TestModels;

namespace Events.UnitTests
{
    [TestClass]
    public class NavigationModelBuilderTests
    {
        [TestMethod]
        public void ReturnsAModel()
        {
            IItem item = new TestItem("test");
            NavigationModelBuilder navModelBuilder = new NavigationModelBuilder();
            NavigationMenu model = navModelBuilder.CreateNavigationMenu(item, item);
            Assert.AreNotEqual(model, null);
        }
    }
}
