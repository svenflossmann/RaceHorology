﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSVAlpin2Lib;
using System.Collections.ObjectModel;

namespace DSVAlpin2LibTest
{
  /// <summary>
  /// Summary description for AppDataModelViewTests
  /// </summary>
  [TestClass]
  public class AppDataModelViewTests
  {
    public AppDataModelViewTests()
    {
    }

    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    //
    // You can use the following additional attributes as you write your tests:
    //
    // Use ClassInitialize to run code before running the first test in the class
    // [ClassInitialize()]
    // public static void MyClassInitialize(TestContext testContext) { }
    //
    // Use ClassCleanup to run code after all tests in a class have run
    // [ClassCleanup()]
    // public static void MyClassCleanup() { }
    //
    // Use TestInitialize to run code before running each test 
    // [TestInitialize()]
    // public void MyTestInitialize() { }
    //
    // Use TestCleanup to run code after each test has run
    // [TestCleanup()]
    // public void MyTestCleanup() { }
    //
    #endregion

    [TestMethod]
    public void StartListViewProviderTest()
    {

      ObservableCollection<RaceParticipant> participants = new ObservableCollection<RaceParticipant> ();
      FillTestRaceParticipants(participants);

      FirstRunStartListViewProvider provider = new FirstRunStartListViewProvider();
      provider.Init(participants);

      // Test initial order
      Assert.AreEqual("Name 2", provider.GetViewList()[0].Name);
      Assert.AreEqual("Name 1", provider.GetViewList()[1].Name);
      Assert.AreEqual("Name 4", provider.GetViewList()[2].Name);

      // Test Update when inserting
      {
        Participant p = new Participant { Name = "Name 3", Firstname = "3" };
        RaceParticipant r = new RaceParticipant(p, 3, 0.0);
        participants.Add(r);
      }
      Assert.AreEqual("Name 2", provider.GetViewList()[0].Name);
      Assert.AreEqual("Name 1", provider.GetViewList()[1].Name);
      Assert.AreEqual("Name 3", provider.GetViewList()[2].Name);
      Assert.AreEqual("Name 4", provider.GetViewList()[3].Name);

      // Test Update when deleting
      participants.RemoveAt(0); // Name 1
      Assert.AreEqual("Name 2", provider.GetViewList()[0].Name);
      Assert.AreEqual("Name 3", provider.GetViewList()[1].Name);
      Assert.AreEqual("Name 4", provider.GetViewList()[2].Name);

      // Test Update when startnumber changes
      participants[1].StartNumber = 2; // Name 4 => StNr 2
      Assert.AreEqual("Name 2", provider.GetViewList()[0].Name);
      Assert.AreEqual("Name 4", provider.GetViewList()[1].Name);
      Assert.AreEqual("Name 3", provider.GetViewList()[2].Name);
    }



    private static void FillTestRaceParticipants(ObservableCollection<RaceParticipant> participants)
    {
      // Constraint: Startnumber = Firstname
      {
        Participant p1 = new Participant { Name = "Name 1", Firstname="2" };
        RaceParticipant r1 = new RaceParticipant(p1, 2, 0.0);
        participants.Add(r1);
      }

      {
        Participant p1 = new Participant { Name = "Name 2", Firstname = "1" };
        RaceParticipant r1 = new RaceParticipant(p1, 1, 0.0);
        participants.Add(r1);
      }

      {
        Participant p1 = new Participant { Name = "Name 4", Firstname = "4" };
        RaceParticipant r1 = new RaceParticipant(p1, 4, 0.0);
        participants.Add(r1);
      }

    }
  }
}